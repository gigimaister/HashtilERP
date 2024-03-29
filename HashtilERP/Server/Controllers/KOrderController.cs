﻿using System;
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
                            Gidul = passport.Oitm.UHebGidul ?? passport.Oitm.ItemName.Split(new char[] { ' ' })[0],
                            Zan = passport.UZanZl ?? passport.Oitm.UHebZan ?? passport.Oitm.UHebGidul,
                            ItemCode = passport.UItemCode,
                            PassprodComments = passprod.UComments ?? "",
                            FixedCoordinationRemark = K_OrderStatus.NeedToSchedule.Trim(),
                            JobStatus = K_OrderPhase.StandBy,
                            UserName = "בוט תיאום הוצאות"

                        };
                        if(passprod.UCustName.Contains("כלובים קטנים"))
                        {
                            korder.IsCageSmall = true;
                        }

                        //if Cx's Seeds && if 1 trey was split for different types of seeds!
                        if (passprod.UQuantity > 55555 && passport.UTraySow == 1)
                        {
                            korder.JobPlantsNum = Convert.ToInt32(passport.UQuanProd * 1000) / passport.Passprods.Count;
                        }

                        //if Cx's Seeds and more than 1 Tray
                        else if (passprod.UQuantity > 55555 && passport.UTraySow > 1)
                        {
                            korder.JobPlantsNum = Convert.ToInt32(passport.UQuanProd * 1000);
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
                return 0;
            }
            return 0;
        }

        //PrepReport Archive
        [HttpGet("GetKOrdersArchive")]
        public async Task<List<KOrder>> GetKOrdersArchive()
        {
          
            var kOrders = await _context.KOrder.Where(x =>(x.MarketingDate <= DateTime.Today)
            ||(x.FixedCoordinationRemark == K_OrderStatus.WasCanceled || x.FixedCoordinationRemark == K_OrderStatus.SchedualeWasOk)
            )           
            .Include(X => X.Ocrd)
            .Include(x => x.k_OrderAuditTables)
            .OrderByDescending(x => x.MarketingDate)
            .ToListAsync();


            return kOrders;
        }

        //Get SAP orders for 1 week(Sun - Sat)
        [HttpGet("GetKOrders")]
        public async Task<List<KOrder>> GetKOrders()
        {
            var beginEnddate = new List<DateTime>();
            beginEnddate = KOrderAlgorithem.GetPrepReportWeekRange(DateTime.Today);
            DateTime? beginDate = beginEnddate[0];
            DateTime? endDate = beginEnddate[1];
            var kOrders = await _context.KOrder.Where(x=>
            (x.MarketingDate >= beginDate && x.MarketingDate <= endDate && x.PrepReportEnteringDate != null)
            ||(x.PrepReportEnteringDate != null && 
            (x.FixedCoordinationRemark == K_OrderStatus.NeedToSchedule 
            || x.FixedCoordinationRemark == K_OrderStatus.NotAnswering
            || x.FixedCoordinationRemark == K_OrderStatus.WantsLess
            || x.FixedCoordinationRemark == K_OrderStatus.WantsMore
            || x.FixedCoordinationRemark == K_OrderStatus.WillLetUsKnow
            || x.FixedCoordinationRemark == K_OrderStatus.WillTalkToOren)
            ))
            .Include(X => X.Ocrd)
            .Include(x => x.k_OrderAuditTables)
            .OrderBy(x=>x.MarketingDate)
            .ToListAsync();


            return kOrders;
        }

        [HttpGet("GetKOrdersByDateRange/{dateTime1?}/{dateTime2?}")]
        public async Task<List<KOrder>> GetKOrdersDateRange(string dateTime1, string dateTime2)
        {
            
            DateTime beginDate = Convert.ToDateTime(dateTime1);
            DateTime endDate = Convert.ToDateTime(dateTime2);
            var sapOrders = await _context.KOrder.Where(x => x.MarketingDate >= beginDate && x.MarketingDate <= endDate && x.PrepReportEnteringDate != null)
                .Include(X => X.Ocrd)
                .Include(x => x.k_OrderAuditTables)
                .OrderBy(x => x.MarketingDate)
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
            if(k_Order.FixedCoordinationRemark == K_OrderStatus.SchedualeWasOk && k_Order.KOrderEnteringDate == null)
            {
                k_Order.KOrderEnteringDate = DateTime.Today;
                k_Order.MarketingDate = new DateTime(Math.Max(Convert.ToDateTime(k_Order.MarketingDate).Ticks, DateTime.Today.Ticks));
            }

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
