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
//using HashtilERP.Shared.Models;

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
        //weekly orders
        [HttpGet("GetWeeklyKOrders")]
        public async Task<List<KOrder>> GetWeeklyKOrders()
        {
            var k_Orders = new List<KOrder>();
            try
            {
               
                    k_Orders = await _context.KOrder.Where(x =>
                    (x.MarketingDate >= DateTime.Today 
                    && (x.PrepReportEnteringDate == null))
                    || (x.MarketingDate >= DateTime.Today
                    && (x.PrepReportEnteringDate != null && x.FixedCoordinationRemark == K_OrderStatus.SchedualeWasOk))
                    )
                  .Include(x => x.Ocrd)
                  .Include(x => x.K_OrderPassports)
                  .ThenInclude(x => x.K_Passport)
                  .Include(x => x.k_OrderRemarks)
                  .Include(x => x.k_OrderAuditTables)
                  .ToListAsync();
                

            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            return k_Orders;
        }

        //archive orders
        [HttpGet("GetArchiveKOrders")]
        public async Task<List<KOrder>> GetArchiveKOrders()
        {
            var k_Orders = new List<KOrder>();
            try
            {
                //IF we in thrsday or friday we want Sunday Jobs as well

                //get M.Date for tomorrow or M.Date today && entered today or M.Date today but not finish or canceled
                k_Orders = await _context.KOrder.Where(x => x.MarketingDate <= DateTime.Today)
              .Include(x => x.Ocrd)
              .Include(x => x.K_OrderPassports)
              .ThenInclude(x => x.K_Passport)
              .Include(x => x.k_OrderRemarks)
              .Include(x => x.k_OrderAuditTables)
              .OrderByDescending(x=>x.MarketingDate)
              .Take(1000)
              .ToListAsync();


            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            return k_Orders;
        }

        //Today Orders
        [HttpGet("GetKOrdersForTodayTomorrow")]
        public async Task<ActionResult<IEnumerable<KOrder>>> GetKOrdersForTodayTomorrow()
        {
            var k_Orders = new List<KOrder>();
            try
            {
                //get M.Date for tomorrow or M.Date today && entered today or M.Date today but not finish or canceled
                k_Orders = await _context.KOrder.Where(x => (x.MarketingDate == DateTime.Today) && (x.KOrderEnteringDate == DateTime.Today)
                || (x.MarketingDate <= DateTime.Today && x.PrepReportEnteringDate != null && x.FixedCoordinationRemark == K_OrderStatus.SchedualeWasOk && x.JobStatus != K_OrderPhase.Finish && x.JobStatus != K_OrderPhase.Canceled)
                || (x.MarketingDate <= DateTime.Today && x.PrepReportEnteringDate == null && x.JobStatus != K_OrderPhase.Finish && x.JobStatus != K_OrderPhase.Canceled)
                || (x.MarketingDate == DateTime.Today.AddDays(1) && (x.FixedCoordinationRemark == K_OrderStatus.SchedualeWasOk || x.PrepReportEnteringDate == null))
                )
              .Include(x => x.Ocrd)
              .Include(x => x.K_OrderPassports)
              .ThenInclude(x => x.K_Passport)
              .Include(x => x.k_OrderRemarks)
              .Include(x => x.k_OrderAuditTables)
              .OrderBy(x => x.CxName)
              .ToListAsync();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            return k_Orders;
        }

        //Preperetion Report(MIRI)
        [HttpGet("GetSapCxList")]
        public async Task<List<KOrder>> GetSapCxList()
        {
            var k_Orders = new List<KOrder>();
            try
            {
                var ocrd = await _context.Ocrd.Where(x => x.UIsKorder == "1").ToListAsync();
                if (ocrd.Count() > 0)
                {
                    foreach (var ocr in ocrd)
                    {
                        var order = new KOrder();
                        order.CardCode = ocr.CardCode;
                        order.CxName = ocr.CardName;
                        k_Orders.Add(order);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return k_Orders;
        }
        //Preperetion Report(MIRI)
        [HttpGet("GetSapCxListAsString")]
        public async Task<List<string>> GetSapCxListAsString()
        {
            var k_Orders = new List<string>();
            try
            {
                var ocrd = await _context.Ocrd.Where(x => x.UIsKorder == "1").ToListAsync();
                if (ocrd.Count() > 0)
                {
                    foreach (var ocr in ocrd)
                    {
                        var order = ocr.CardName;                        
                        k_Orders.Add(order);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                korder = await _context.KOrder.Where(x => x.JobId == id)
                    .Include(x => x.K_OrderPassports)
                    .ThenInclude(x => x.K_Passport)
                    .FirstOrDefaultAsync();                
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
              .Include(x=>x.k_OrderAuditTables)
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

        [HttpPut("UpdateKOrderPassport")]
        public async Task<IActionResult> UpdateKOrderPassport(KOrderPassports kOrderPassports)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            kOrderPassports.UserName = screenName;

            _context.Entry(kOrderPassports).State = EntityState.Modified;

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
            kOrder.KOrderEnteringDate = DateTime.Today;

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

        // DELETE: api/WeeklyKOrder/KOrderPassportsDelete/5
        [HttpDelete("KOrderPassportsDelete/{id}")]
        public async Task<IActionResult> KOrderPassportsDelete(int? id)
        {

            try 
            { 
                var kordpass = await _context.KOrderPassports.Where(x => x.JobId == id).ToListAsync();
                if (kordpass == null)
                {
                    return NotFound();
                }
                foreach (var korpas in kordpass)
                {
                    _context.KOrderPassports.Remove(korpas);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message); 
            }
            
            
           
            return NoContent();
        }

        // DELETE: api/WeeklyKOrder/KOrderPassportsDelete/5
        [HttpDelete("KOrderPassportDelete/{id}")]
        public async Task<IActionResult> KOrderPassportDelete(int id)
        {
            var kordpass = await _context.KOrderPassports.Where(x => x.PassportsToOrdersId == id).FirstOrDefaultAsync();
            if (kordpass == null)
            {
                return NotFound();
            }

            _context.KOrderPassports.Remove(kordpass);
            await _context.SaveChangesAsync();
            

            return Ok();
        }

        #endregion

        private bool KOrderExists(int id)
        {
            return _context.KOrder.Any(e => e.JobId == id);
        }

    }
}
