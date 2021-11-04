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
using HashtilERP.Shared;

namespace HashtilERP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly OrdersContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public NotificationsController(OrdersContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        #region GET REGION
        //Preperetion Report(MIRI)
        [HttpGet("subscribe")]
        public async Task<NotificationSubscription> Subscribe(NotificationSubscription subscription)
        {
            var user = await _userManager.GetUserAsync(User);
            var usrId = user.Id;
            var pushUsr = await _context.NotificationSubscription.Where(x => x.UserId == usrId).FirstOrDefaultAsync();
            if (String.IsNullOrEmpty(pushUsr.UserId))
            {
                _context.NotificationSubscription.Add(pushUsr);
                
            }
            else 
            {
                _context.Entry(pushUsr).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }

            return subscription;
        }
        #endregion
    }
}
