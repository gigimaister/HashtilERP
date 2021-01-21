using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HashtilERP.Data;
using Microsoft.AspNetCore.Identity;
using HashtilERP.Server.Models;

namespace HashtilERP.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassportAuditFormController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HashtilERPContext _context;

        public PassportAuditFormController(HashtilERPContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<PassportAuditForm>> PostAuditForm(PassportAuditForm passportAuditForm)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var screenName = user.ScreenName;               
                passportAuditForm.UserName = screenName;                
                _context.PassportAuditForm.Add(passportAuditForm);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return Ok();
        }

    }
}