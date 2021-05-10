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
    public class WeeklyKOrderController : ControllerBase
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
            try
            {
                k_Orders = await _context.KOrder.Where(x => (x.MarketingDate == DateTime.Today || x.MarketingDate == DateTime.Today.AddDays(1))
                && (x.JobStatus == K_OrderPhase.StandBy || x.JobStatus == K_OrderPhase.AttachedPassports || x.JobStatus == K_OrderPhase.InProgress 
                || x.JobStatus == K_OrderPhase.Finish))
              .Include(x => x.Ocrd)
              .Include(x => x.K_OrderPassports)
              .ThenInclude(x => x.K_Passport)
              .Include(x => x.k_OrderRemarks)
              .ToListAsync();
            }
          catch(Exception e) { Console.WriteLine(e.Message); }

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

        //Thai Korder Ready
        [HttpGet("GetKOrder/Thai/{id}")]
        public async Task<KOrder> GetKOrderThai(int id)
        {
            var korder = new KOrder();
            try
            {
                korder = await _context.KOrder.Where(x => x.JobId == id).FirstOrDefaultAsync();                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return korder;
        }


        [HttpGet("GetChangedJobsAfterDelivery")]
        public async Task<List<KOrder>> GetChangedJobsAfterDelivery()
        {
            var k_Orders = new List<KOrder>();
            k_Orders = await _context.KOrder.Where(x => x.IsWasChangedAfterDeliveryReport == true)
              .Include(x => x.Ocrd)
              .Include(x => x.K_OrderPassports)
              .ThenInclude(x => x.K_Passport)
              .Include(x => x.k_OrderRemarks)
              .ToListAsync();
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
            kOrder.UserName = screenName;

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

        [HttpPost("NewKOrderPassport")]
        public async Task<int> NewKOrderPassport(KOrderPassports kOrderPassports)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            kOrderPassports.UserName = screenName;
            kOrderPassports.PassportsToOrdersId = 0;

            _context.KOrderPassports.Add(kOrderPassports);

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

        [HttpPost("NewKOrderRemark")]
        public async Task<int> NewKOrderRemark(KOrderRemark kOrderRemark)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            kOrderRemark.UserName = screenName;
            kOrderRemark.K_OrderRemarkStamp = DateTime.Now;

            _context.KOrderRemarks.Add(kOrderRemark);
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

        #region DELETE

            // DELETE: api/WeeklyKOrder/5
            [HttpDelete("KOrderPassportsDelete/{id}")]
        public async Task<IActionResult> KOrderPassportsDelete(int id)
        {
            var kordpass = await _context.KOrderPassports.Where(x=>x.JobId == id).ToListAsync();
            if (kordpass == null)
            {
                return NotFound();
            }
            foreach(var korpas in kordpass)
            {
                _context.KOrderPassports.Remove(korpas);
                await _context.SaveChangesAsync();
            }
           
            return NoContent();
        }


        #endregion

        private bool KOrderExists(int id)
        {
            return _context.KOrder.Any(e => e.JobId == id);
        }

    }
}
