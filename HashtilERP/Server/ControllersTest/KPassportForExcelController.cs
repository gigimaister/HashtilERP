using HashtilERP.Data;
using HashtilERP.Server.Models;
using HashtilERP.TestContextApi;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Server.ControllersTest
{

    [Route("api/[controller]")]
    public class KPassportForExcelController : ControllerBase
    {
        private readonly TestContext _context;
        private readonly HashtilERPContext _Context;

        private readonly UserManager<ApplicationUser> _userManager;
        public KPassportForExcelController(TestContext context, HashtilERPContext Context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _Context = Context;
            _userManager = userManager;
        }

        //Insert into TEST Db!!
        [HttpGet("GetNewMetzayForTest")]
        public async Task<IActionResult> GetNewMetzayForTest()
        {
            var kPassports = await _context.KPassportForTest.ToListAsync();
            if (kPassports.Count() > 0)
                return BadRequest();
            try
            {
                var limitKpassport = await _context.KPassportExcelInsert.ToListAsync();
                foreach(var limitPass in limitKpassport)
                {
                    var kpass = new KPassportForTest();
                    kpass.Hamama = limitPass.Hamama.ToString();
                    kpass.Gamlon = limitPass.Gamlon.ToString();
                    kpass.PassportNum = limitPass.Passport;
                    kpass.MagashAmount = limitPass.Magash;
                    kpass.PassportAvg = limitPass.Avarage;

                    HashtilERP.Data.Passport sap;
                    HashtilERP.Data.Oitm sapOitm;

                    try
                    {
                        sap = await _Context.Passport.Where(X => X.DocNum == kpass.PassportNum).FirstAsync();
                        sapOitm = await _Context.Oitm.Where(X => X.ItemCode == sap.UItemCode).FirstAsync();


                    }
                    //if no passport in SAP
                    catch (Exception)
                    {
                        return StatusCode(500, "NOTFOUND");
                    }

                    var screenName ="בוט השתיל";
                    int? startingAvg;

                    var dup = await _context.KPassportForTest.Where(X => X.PassportNum == kpass.PassportNum).FirstOrDefaultAsync();
                    //if duplicate in K_Passport
                    if (dup != null)
                    {
                        return StatusCode(500, "DUPLICATE");
                    }
                    DateTime passingDate;
                    kpass.UserName = screenName.Trim();
                    kpass.SowDate = sap.UDateSow;
                    passingDate = (DateTime)sap.UDateSow;
                    kpass.DateEnd = sap.UDateEnd;
                    //if ZIREY LAKOACH
                    if (sap.UQuanOrdP > 5555554) { startingAvg = Convert.ToInt32(sap.UQuanProd * 1000 / sap.UTraySow); } else { startingAvg = Convert.ToInt32(sap.UQuanOrdP * 1000 / sap.UTraySow); }
                    kpass.PassportStartingAVG = startingAvg;
                    kpass.GrowingDays = Convert.ToInt32(((TimeSpan)(sap.UDateEnd - sap.UDateSow)).Days);
                    kpass.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
                    kpass.MagashAmount = Convert.ToInt32(sap.UTraySow);
                    kpass.PlantsAmount = sap.UQuanProd * 1000;
                    kpass.PassportStatus = Status.InGreenHouse.Trim();
                    kpass.PassportStatusCode = (int)PassportStatusCode.InsideGreenHouse;
                    kpass.ItemCode = sap.UItemCode.Trim();
                    kpass.SapDocEntry = sap.DocEntry;
                    kpass.PassportCondition = Status.NotChecked.Trim();
                    kpass.GrowingRoomExitDay = passingDate.AddDays(Convert.ToInt32(sap.UNights));
                    kpass.Zan = sap.UZanZl ?? sapOitm.UHebZan ?? sapOitm.UHebGidul;
                    kpass.CelsTray = Convert.ToInt32(sapOitm.UCelsTray * 1000);
                    kpass.Gidul = sapOitm.UHebGidul ?? sapOitm.ItemName.Split(new char[] { ' ' })[0];
                    kpass.IsSavedForCx = false;
                    kpass.IsNeedToCut = true && sapOitm.ItemName.Contains("מפוצל");
                    _context.KPassportForTest.Add(kpass);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }


                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Ok();
        }

        //Insert into Production Db!!
        [HttpGet("GetNewMetzayForProduction")]
        public async Task<IActionResult> GetNewMetzayForProduction()
        {
            //var kPassports = await _Context.KPassport.ToListAsync();
            //if (kPassports.Count() > 0)
            //    return BadRequest();
            try
            {
                var limitKpassport = await _context.KPassportExcelInsert.ToListAsync();
                foreach (var limitPass in limitKpassport)
                {
                    var kpass = new HashtilERP.TestContextApi.KPassport();
                    kpass.Hamama = limitPass.Hamama.ToString();
                    kpass.Gamlon = limitPass.Gamlon.ToString();
                    kpass.PassportNum = Convert.ToInt32(limitPass.Passport);
                    kpass.MagashAmount = limitPass.Magash;
                    kpass.PassportAvg = limitPass.Avarage > 0 ? limitPass.Avarage : -1;

                    HashtilERP.Data.Passport sap;
                    HashtilERP.Data.Oitm sapOitm;

                    try
                    {
                        sap = await _Context.Passport.Where(X => X.DocNum == kpass.PassportNum).FirstAsync();
                        sapOitm = await _Context.Oitm.Where(X => X.ItemCode == sap.UItemCode).FirstAsync();


                    }
                    //if no passport in SAP
                    catch (Exception)
                    {
                        return StatusCode(500, "NOTFOUND");
                    }

                    var screenName = "בוט השתיל";
                    int? startingAvg;

                    var dup = await _context.KPassport.Where(X => X.PassportNum == kpass.PassportNum).FirstOrDefaultAsync();
                    //if duplicate in K_Passport
                    if (dup != null)
                    {
                        continue;                  
                    }
                    DateTime passingDate;
                    kpass.UserName = screenName.Trim();
                    kpass.SowDate = sap.UDateSow;
                    passingDate = (DateTime)sap.UDateSow;
                    kpass.DateEnd = sap.UDateEnd;
                    //if ZIREY LAKOACH
                    if (sap.UQuanOrdP > 5555554) { startingAvg = Convert.ToInt32(sap.UQuanProd * 1000 / sap.UTraySow); } else { startingAvg = Convert.ToInt32(sap.UQuanOrdP * 1000 / sap.UTraySow); }
                    kpass.PassportStartingAvg = startingAvg;
                    kpass.GrowingDays = Convert.ToInt32(((TimeSpan)(sap.UDateEnd - sap.UDateSow)).Days);
                    kpass.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
                    kpass.MagashAmount = Convert.ToInt32(sap.UTraySow);
                    kpass.PlantsAmount = sap.UQuanProd * 1000;
                    kpass.PassportStatus = Status.InGreenHouse.Trim();
                    kpass.PassportStatusCode = (int)PassportStatusCode.InsideGreenHouse;
                    kpass.ItemCode = sap.UItemCode.Trim();
                    kpass.SapDocEntry = sap.DocEntry;
                    kpass.PassportCondition = Status.NotChecked.Trim();
                    kpass.GrowingRoomExitDay = passingDate.AddDays(Convert.ToInt32(sap.UNights));
                    kpass.Zan = sap.UZanZl ?? sapOitm.UHebZan ?? sapOitm.UHebGidul;
                    kpass.CelsTray = Convert.ToInt32(sapOitm.UCelsTray * 1000);
                    kpass.Gidul = sapOitm.UHebGidul ?? sapOitm.ItemName.Split(new char[] { ' ' })[0];
                    kpass.IsSavedForCx = false;
                    kpass.IsNeedToCut = true && sapOitm.ItemName.Contains("מפוצל");
                    kpass.MetzayEnteringDate = DateTime.Today;
                    _context.KPassport.Add(kpass);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }


                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Ok();
        }
    }
}
