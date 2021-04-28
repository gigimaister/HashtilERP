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
    public class WeeklyKOrderController : Controller
    {
        private readonly OrdersContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public WeeklyKOrderController(OrdersContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region GET REGION
        //Preperetion Report(MIRI)
        [HttpGet("GetKOrdersForTodayTomorrow")]
        public async Task<List<KOrder>> GetKOrdersForTodayTomorrow()
        {
            var k_Orders = new List<KOrder>();

            k_Orders = await _context.KOrder.Where(x =>(x.FixedCoordinationRemark == K_OrderStatus.SchedualeWasOk && x.MarketingDate == DateTime.Today )|| (x.FixedCoordinationRemark == K_OrderStatus.SchedualeWasOk && x.MarketingDate == DateTime.Today.AddDays(1))
            ||(x.PrepReportEnteringDate== null && x.MarketingDate == DateTime.Today.AddDays(1)) || (x.PrepReportEnteringDate == null && x.MarketingDate == DateTime.Today) )
                .Include(x=>x.Ocrd)
                .ToListAsync();

            return k_Orders;
        }

        //Preperetion Report(MIRI)
        [HttpGet("GetSapCxList")]
        public async Task<List<KOrder>> GetSapCxList()
        {
            var k_Orders = new List<KOrder>();

            var ocrd = await _context.Ocrd.Where(x => x.CardType == "C" && x.UBsyType == "2" && x.FrozenFor == "N").ToListAsync();
            if(ocrd.Count() > 0)
            {
                foreach(var ocr in ocrd)
                {
                    var order = new KOrder();
                    order.CardCode = ocr.CardCode;
                    order.CxName = ocr.CardName;
                    k_Orders.Add(order);
                }
            }

            return k_Orders;
        }



        #endregion

        #region PUT REGION

        [HttpPut("UpdateTodayTomorrowOrder/{id}")]
        public async Task<IActionResult> UpdateTodayTomorrowOrder(int id, KOrder k_Order)
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

        #region POST
        [HttpPost("NewKOrderInsert")]
        public async Task<int> PostNewKOrder(KOrder kOrder)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;

            _context.KOrder.Add(kOrder);

            try
            {              
                await _context.SaveChangesAsync();
            }
            //if no KOrder in KOrder Table
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            


           
            return 0;
        }
        #endregion

        private bool KOrderExists(int id)
        {
            return _context.KOrder.Any(e => e.JobId == id);
        }

    }
}
