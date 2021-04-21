using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HashtilERP.Data;
using Microsoft.AspNetCore.Identity;
using HashtilERP.Server.Models;
using HashtilERP.DBTestVol1;
using HashtilERP.Shared.Models;

namespace HashtilERP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KOrderController : ControllerBase
    {
        private readonly OrdersContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public KOrderController(OrdersContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        #region GET REGION
        //Preperetion Report(MIRI)
        [HttpGet("PrepReport")]
        public async Task<int> GetCurrentJobs()
        {
            var prepReport = new List<KOrder>();
            var beginEnddate = new List<DateTime>();
            beginEnddate = KOrderAlgorithem.GetPrepReportWeekRange(DateTime.Today);
            DateTime? beginDate = beginEnddate[0];
            DateTime? endDate = beginEnddate[1];
            try
            {
                //get all Passports(!!) with Sun - Sat Jobs(Filter 1)
                var passportsToOrders = await _context.Passport.Where(x => x.UDateEnd >= beginDate && x.UDateEnd <= endDate && x.UTraySow>0)
                        .Include(x => x.Oitm)
                        .Include(x => x.Passprods)
                        .OrderBy(x=>x.UDateEnd)
                        .ToListAsync();

                foreach(var passport in passportsToOrders)
                {
                    //Filter only sowed Passports(Filter 2)
                    if (passport.UTraySow == 0) { continue; }

                    foreach (var passprod in passport.Passprods)
                    {
                        //Continue filter for Cx's only(Filter 3)
                        if (passprod.UQuantity == 0) { continue; }

                        var korder = new KOrder
                        {
                            DocEntry = passprod.DocEntry,
                            PrepReportEnteringDate = DateTime.Today,
                            MarketingDate = passprod.UDateSupp,
                            SaleNum = passprod.USaleNum,
                            CxName = passprod.UCustName,
                            Gidul = passport.Oitm.UHebGidul,
                            Zan = passport.UZanZl ?? passport.Oitm.UHebZan ?? passport.Oitm.UHebGidul,
                            ItemCode = passport.UItemCode,
                            PassprodComments = passprod.UComments ?? "",
                            FixedCoordinationRemark = K_OrderStatus.NeedToSchedule.Trim(),

                        };


                        //if Cx's Seeds && if 1 trey was split for different types of seeds!
                        if (passprod.UQuantity > 55555 && passport.UTraySow == 1)
                        {
                            korder.JobPlantsNum = Convert.ToInt32(passport.UQuanProd * 1000) / passport.Passprods.Count;
                        }

                        else
                        {
                            korder.JobPlantsNum = Convert.ToInt32(passprod.UQuantity * 1000);
                        }

                        //adding cx CardCode to KOrder(because USaleNum string and DocNum is int...)
                        var ordr = await _context.Ordr.Where(x => x.DocNum.ToString() == passprod.USaleNum).FirstOrDefaultAsync();

                        korder.CardCode = ordr.CardCode;

                        prepReport.Add(korder);
                    }                      
                   
                }

                //retrieve KOrder list from db 
                var listOfKOrder = await _context.KOrder.OrderBy(x=>x.MarketingDate).ToListAsync();

                if (listOfKOrder.Count > 0)
                {
                    //remove duplicates
                    foreach (var korder in listOfKOrder)
                    {
                        prepReport.RemoveAll(x => x.DocEntry == korder.DocEntry && x.SaleNum == korder.SaleNum);
                    }
                }            

                var sdfg = prepReport;

                if(prepReport.Count > 0)
                {
                    foreach(var korder in prepReport)
                    {
                        _context.Add(korder);
                        await _context.SaveChangesAsync();
                    }
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        //Get SAP orders for 1 week(Sun - Sat)
        [HttpGet("GetKOrders")]
        public async Task<List<KOrder>> GetKOrders()
        {
            var beginEnddate = new List<DateTime>();
            beginEnddate = KOrderAlgorithem.GetPrepReportWeekRange(DateTime.Today);
            DateTime? beginDate = beginEnddate[0];
            DateTime? endDate = beginEnddate[1];
            var kOrders = await _context.KOrder.Where(x=>x.FixedCoordinationRemark != K_OrderStatus.WasCanceled  || x.MarketingDate >= beginDate && x.MarketingDate <= endDate)
                .Include(X=>X.Ocrd).OrderBy(x=>x.MarketingDate)
                .ToListAsync();

            var korderFinal = kOrders.Where(x => x.FixedCoordinationRemark != K_OrderStatus.SchedualeWasOk).ToList();

            return korderFinal;
        }
        [HttpGet("GetKOrdersByDateRange/{dateTime1}/{dateTime2}")]
        public async Task<List<KOrder>> GetKOrdersDateRange(string dateTime1, string dateTime2)
        {

            DateTime beginDate = Convert.ToDateTime(dateTime1);
            DateTime endDate = Convert.ToDateTime(dateTime2);
            var sapOrders = await _context.KOrder.Where(x => x.MarketingDate >= beginDate && x.MarketingDate <= endDate)
                .Include(X => X.Ocrd).OrderBy(x => x.MarketingDate)
                .ToListAsync();
            return sapOrders;
        }
        #endregion

        #region PUT REGION

        [HttpPut("UpdatePrepReport/{id}")]
        public async Task<IActionResult> UpdateSapPrepReport(int id, KOrder k_Order)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            k_Order.UserName = screenName;

            if (id != k_Order.JobId)
            {
                return BadRequest();
            }


            _context.Entry(k_Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Ok();
        }

        #endregion
    }


}
