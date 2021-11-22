using HashtilERP.Data;
using HashtilERP.Server.Models;
using HashtilERP.Shared.Models.Mobile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Server.Controllers.Android
{
    [Route("api/Android/[controller]")]
    [ApiController]
    public class KPassportController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HashtilERPContext _context;
        public KPassportController(HashtilERPContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("PostKPassport")]
        public async Task<ActionResult<K_Passport>> PostKPassport(MobileUser mobileUser, K_Passport kPassport)
        {
            // Check If Valid User
            ApplicationUser userTest = await _userManager.FindByNameAsync(mobileUser.UserName);
            bool isValidUser = await _userManager.CheckPasswordAsync(userTest, mobileUser.Password);

            if (!isValidUser)
            {
                return Unauthorized();              
            }
            // Init
            Passport sap;
            Oitm sapOitm;
            int? startingAvg;
            try
            {
                sap = await _context.Passport.Where(X => X.DocNum == kPassport.PassportNum).FirstAsync();
                sapOitm = await _context.Oitm.Where(X => X.ItemCode == sap.UItemCode).FirstAsync();
            }
            //if no passport in SAP
            catch (Exception)
            {
                return Accepted();
            }


            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;

            var dup = await _context.KPassport.Where(X => X.PassportNum == kPassport.PassportNum).FirstOrDefaultAsync();
            //if duplicate in K_Passport
            if (dup != null)
            {
                return Conflict();
            }
            DateTime passingDate;
            kPassport.UserName = screenName.Trim();
            kPassport.SowDate = sap.UDateSow;
            passingDate = (DateTime)sap.UDateSow;
            kPassport.DateEnd = sap.UDateEnd;
            //if ZIREY LAKOACH
            if (sap.UQuanOrdP > 5555554) { startingAvg = Convert.ToInt32(sap.UQuanProd * 1000 / sap.UTraySow); } else { startingAvg = Convert.ToInt32(sap.UQuanOrdP * 1000 / sap.UTraySow); }
            kPassport.PassportStartingAVG = startingAvg; kPassport.GrowingDays = Convert.ToInt32(((TimeSpan)(sap.UDateEnd - sap.UDateSow)).Days);
            kPassport.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.MagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.PlantsAmount = sap.UQuanProd * 1000;
            kPassport.PassportStatus = Status.GrowingRoom.Trim();
            kPassport.PassportStatusCode = (int)PassportStatusCode.Growingroom;
            kPassport.ItemCode = sap.UItemCode.Trim();
            kPassport.SapDocEntry = sap.DocEntry;
            kPassport.PassportCondition = Status.NotChecked.Trim();
            kPassport.GrowingRoomExitDay = passingDate.AddDays(Convert.ToInt32(sap.UNights));
            kPassport.Zan = sap.UZanZl ?? sapOitm.UHebZan ?? sapOitm.UHebGidul;
            kPassport.CelsTray = Convert.ToInt32(sapOitm.UCelsTray * 1000);
            kPassport.Gidul = sapOitm.UHebGidul ?? sapOitm.ItemName.Split(new char[] { ' ' })[0];
            kPassport.IsSavedForCx = false;
            kPassport.IsNeedToCut = true && sapOitm.ItemName.Contains("מפוצל");
            _context.KPassport.Add(kPassport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                
            }

            return CreatedAtAction("GetKPassport", new { id = kPassport.K_PassportId }, kPassport);
        }
    }
}
