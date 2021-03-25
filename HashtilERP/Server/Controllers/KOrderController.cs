using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HashtilERP.Data;
using Microsoft.AspNetCore.Identity;
using HashtilERP.Server.Models;
namespace HashtilERP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KOrderController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HashtilERPContext _context;
        public KOrderController(HashtilERPContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //Preperetion Report(MIRI)
        [HttpGet("PrepReport")]
        public async Task<ActionResult<IEnumerable<K_Order>>> GetCurrentMetzay()
        {
            var prepReport = new List<K_Order>();
            List<Passprod> listPassprod;
            List<Passport> listPassport;
            var beginEnddate = new List<DateTime>();
            beginEnddate = KOrderAlgorithem.GetPrepReportWeekRange(DateTime.Today);
            var beginDate = beginEnddate[0];
            var endDate = beginEnddate[1];
            try
            {
                var t = await _context.Passport.Where(x => x.UDateEnd >= beginDate && x.UDateEnd <= endDate).OrderBy(x=>x.UDateEnd)
                    .Include(x=>x.Oitm)
                    .ToListAsync();
                listPassport = await _context.Passport.Where(x => x.UDateEnd >= beginDate && x.UDateEnd <= endDate).Include(x => x.Passprods)
                    .Include(x=>x.Oitm)                  
                    .ToListAsync();
                listPassprod = null;
                foreach (var order in listPassprod)
                {
                    try
                    {


                        var passPort = listPassport.Where(x => x.DocEntry == order.DocEntry).Single();
                        var newOrder = new K_Order();
                        newOrder.MarketingDate = (DateTime)order.UDateSupp;
                        newOrder.SaleNum = order.USaleNum;
                        newOrder.Gidul = passPort.Oitm.UHebGidul ?? "gg";
                        newOrder.Zan = passPort.UZanZl ?? passPort.Oitm.UHebZan;
                        if (order.UQuantity < 5555554)
                        {
                            newOrder.JobPlantsNum = Convert.ToInt32(order.UQuantity * 1000);
                        }
                        else
                        {
                            //newOrder.JobPlantsNum = Convert.ToInt32(passPort.Select(x => x.UQuanProd * 1000));
                        }
                        newOrder.CxName = order.UCustName;
                        prepReport.Add(newOrder);
                    }
                    catch(Exception)
                    { continue; }
                }

                return prepReport;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
