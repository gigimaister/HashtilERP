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


    }
    #endregion

    #region PUT REGION
   

    #endregion
}
