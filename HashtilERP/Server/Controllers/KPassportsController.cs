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
    public class KPassportsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HashtilERPContext _context;

        public KPassportsController(HashtilERPContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region GET
        //GET WITH PASSPORT STATUS CONDITION
        [HttpGet("multi/{status}")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetKPassport(string status)
        {
            var ChosenList = new List<K_Passport>();
            var user = await _userManager.GetUserAsync(User);
           
            try
            {
                switch (status)
                {
                    //growing room
                    case "1":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus == Status.GrowingRoom)
                            .Include(e=>e.KPassportInsertAudit)
                           .Include(e=>e.Passport)
                           .ThenInclude(e=>e.Passprods)
                           .OrderByDescending(x=>x.KPassportInsertAudit.Date)
                            .ToListAsync();
                        break;
                    //waiting for confirmation
                    case "2":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus == Status.WaitingForOK)                          
                           .ToListAsync();
                        break;
                    //inside green house
                    case "3":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatusCode==(int)PassportStatusCode.InsideGreenHouse)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)                          
                           .Include(e => e.PassportAuditForms)                          
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                        break;
                    //need to be audit
                    case "4":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse && x.IsNeedToBeAudit == true)
                            .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)                         
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                        break;
                    //need to be checked
                    case "5":
                        ChosenList = await _context.KPassport.Where(x => x.IsNeedToBeChecked == true)
                            .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)                          
                           .Include(e => e.PassportAuditForms)                          
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                        break;
                    //for AVG Counter
                    case "6":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus.Trim() == Status.InGreenHouse && x.PassportAvg != -1)
                           .OrderByDescending(x =>x.AVGEnteringDate)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)                          
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                        break;
                    //archive
                    case "7":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus.Trim() == Status.Destroyed || x.PassportStatus.Trim() == Status.Finished && x.IsNeedToBeChecked==false)
                           .OrderByDescending(x => x.AVGEnteringDate)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)                        
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                        break;
                   

                }
                return ChosenList;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
           
           

        }
        //Metzay
        [HttpGet("metzay")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetCurrentMetzay()
        {
            var metzay = new List<K_Passport>();
            

            try
            {
                metzay = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)                                                  
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                return metzay;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        //Tomatos For cut
        [HttpGet("NeedToBeCut")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetNeedToBeCut()
        {
            var metzay = new List<K_Passport>();
            try
            {
                metzay = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse && x.IsNeedToCut == true)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                return metzay;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        //Saved For cx
        [HttpGet("savedforcx")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetSavedForCx()
        {
            var metzay = new List<K_Passport>();
            try
            {
                metzay = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse && x.IsSavedForCx == true)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                return metzay;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        //GET for LowAVG 
        [HttpGet("lowavg")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetLoeAVGMetzay()
        {
            var metzay = new List<K_Passport>();


            try
            {
                metzay = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse && x.IsLowAVG == true)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                return metzay;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        //List Of Passports For Metzay Report for day...
        [HttpGet("report/{date}")]
        public async Task<ActionResult<IEnumerable<K_Passport>>> GetMetzayReportStatus(string date)
        {
            var ChosenList = new List<K_Passport>();
            var user = await _userManager.GetUserAsync(User);

            try
            {
                ChosenList = await _context.KPassport.Where(x => x.MetzayEnteringDate < Convert.ToDateTime(date) && Convert.ToDateTime(date) < x.MetzayOutGoingDate)
               
                            .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Oitm)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.UpdateK_PassportAudit)
                           .Include(e => e.k_PassportAuditTblVer2s)                          
                           .ToListAsync();

                return ChosenList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



        }

        //GET FOR OBJECT RETURN TO RAZOR PAGE
        [HttpGet("Thai/GreenHouse/{id}")]
        public async Task<ActionResult<K_Passport>> GetThaiKPassport(int id)
        {

            var kPassport = await _context.KPassport.Where(x=>x.PassportNum==id).Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Oitm)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.UpdateK_PassportAudit)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .FirstOrDefaultAsync();

            if (kPassport == null)
            {
                return NotFound();
            }

            return kPassport;
        }


        // GET: api/KPassports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<K_Passport>> GetKPassport(int id)
        {

            var kPassport = await _context.KPassport.FindAsync(id);

            if (kPassport == null)
            {
                return NotFound();
            }

            return kPassport;
        }

        #endregion

        #region PUT
        // PUT: GREENHOUSE THAI HAMAMA AND GAMLON UPDATE       
        [HttpPut("Thai/Passport/Update/{id}")]
        public async Task<IActionResult> PutThaiKPassport(int id, K_Passport kPassport)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            kPassport.UserName = screenName;

            if (id != kPassport.K_PassportId)
            {
                return BadRequest();
            }


            _context.Entry(kPassport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (Exception e)
            {
                if (!KPassportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            return Ok();
        }

        // PUT: GREENHOUSE MANAGER STATUS TO CONFIRMED
        
        [HttpPut("GreenManager/Passport/UpdateStatus/{id}")]
        public async Task<IActionResult> PutGreenManagerKPassportStatus(int id, K_Passport kPassport)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            kPassport.UserName = screenName;
            
            if (id != kPassport.K_PassportId)
            {
                return BadRequest();
            }


            _context.Entry(kPassport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (Exception e)
            {
                if (!KPassportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            return Ok();
        }


        // PUT: api/KPassports/5
        //PUT DEFAULT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKPassport(int id, K_Passport kPassport)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
            kPassport.UserName = screenName;
            if (kPassport.PassportAvg == -1)
            {
                kPassport.PlantsAmount = (kPassport.MagashAmount * kPassport.PassportStartingAVG);
            }
            else
            {
                kPassport.PlantsAmount = (kPassport.MagashAmount * kPassport.PassportAvg);
            }
            if (id != kPassport.K_PassportId)
            {
                return BadRequest();
            }

            _context.Entry(kPassport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            
            catch (DbUpdateConcurrencyException)
            {
                if (!KPassportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #endregion

        #region POST
        // POST: api/KPassports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<K_Passport>> PostKPassport(K_Passport kPassport)
        {
            Passport sap;
            Oitm sapOitm;
            try
            {
                 sap = await _context.Passport.Where(X => X.DocNum == kPassport.PassportNum).FirstAsync();
                 sapOitm = await _context.Oitm.Where(X => X.ItemCode == sap.UItemCode).FirstAsync();


            }
            //if no passport in SAP
            catch (Exception)
            {
                return StatusCode(500, "NOTFOUND");
            }


            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;

            var dup = await _context.KPassport.Where(X => X.PassportNum == kPassport.PassportNum).FirstOrDefaultAsync();
            //if duplicate in K_Passport
            if (dup != null)
            {
                return StatusCode(500, "DUPLICATE");
            }
            DateTime passingDate;
            kPassport.UserName = screenName.Trim();
            kPassport.SowDate = sap.UDateSow;
            passingDate = (DateTime)sap.UDateSow;
            kPassport.DateEnd = sap.UDateEnd;
            kPassport.PassportStartingAVG = Convert.ToInt32((sap.UQuanOrdP*1000) / sap.UTraySow);  
            kPassport.GrowingDays = Convert.ToInt32(((TimeSpan)(sap.UDateEnd - sap.UDateSow)).Days) ;
            kPassport.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.MagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.PlantsAmount = sap.UQuanProd*1000;
            kPassport.PassportStatus = Status.GrowingRoom.Trim();
            kPassport.PassportStatusCode = (int)PassportStatusCode.Growingroom;
            kPassport.ItemCode = sap.UItemCode.Trim();
            kPassport.SapDocEntry = sap.DocEntry;
            kPassport.PassportCondition = Status.NotChecked.Trim();
            kPassport.GrowingRoomExitDay = passingDate.AddDays(Convert.ToInt32(sap.UNights));
            kPassport.Zan = sap.UZanZl ?? sapOitm.UHebZan;
            kPassport.CelsTray = Convert.ToInt32(sapOitm.UCelsTray * 1000);
            kPassport.Gidul = sapOitm.UHebGidul.Trim();
            kPassport.IsSavedForCx = false;
            kPassport.IsNeedToCut = true && sapOitm.ItemName.Contains("מפוצל");
            _context.KPassport.Add(kPassport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (KPassportExists(kPassport.K_PassportId))
                {
                    return Conflict();
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            return CreatedAtAction("GetKPassport", new { id = kPassport.K_PassportId }, kPassport);
        }

        [HttpPost("NewPassportInsertByManager")]
        public async Task<ActionResult<K_Passport>> PostNewKPassportByManager(K_Passport kPassport)
        {
            Passport sap;
            Oitm sapOitm;
            try
            {
                sap = await _context.Passport.Where(X => X.DocNum == kPassport.PassportNum).FirstAsync();
                sapOitm = await _context.Oitm.Where(X => X.ItemCode == sap.UItemCode).FirstAsync();


            }
            //if no passport in SAP
            catch (Exception)
            {
                return StatusCode(500, "NOTFOUND");
            }


            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;

            var dup = await _context.KPassport.Where(X => X.PassportNum == kPassport.PassportNum).FirstOrDefaultAsync();
            //if duplicate in K_Passport
            if (dup != null)
            {
                return StatusCode(500, "DUPLICATE");
            }
            DateTime passingDate;
            kPassport.UserName = screenName.Trim();
            kPassport.SowDate = sap.UDateSow;
            passingDate = (DateTime)sap.UDateSow;
            kPassport.DateEnd = sap.UDateEnd;
            kPassport.PassportStartingAVG = Convert.ToInt32((sap.UQuanOrdP * 1000) / sap.UTraySow);
            kPassport.GrowingDays = Convert.ToInt32(((TimeSpan)(sap.UDateEnd - sap.UDateSow)).Days);
            kPassport.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.MagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.PlantsAmount = sap.UQuanProd * 1000;
            kPassport.PassportStatus = Status.InGreenHouse.Trim();
            kPassport.PassportStatusCode = (int)PassportStatusCode.InsideGreenHouse;
            kPassport.ItemCode = sap.UItemCode.Trim();
            kPassport.SapDocEntry = sap.DocEntry;
            kPassport.PassportCondition = Status.NotChecked.Trim();
            kPassport.GrowingRoomExitDay = passingDate.AddDays(Convert.ToInt32(sap.UNights));
            kPassport.Zan = sap.UZanZl ?? sapOitm.UHebZan;
            kPassport.CelsTray = Convert.ToInt32(sapOitm.UCelsTray * 1000);
            kPassport.Gidul = sapOitm.UHebGidul.Trim();
            kPassport.IsSavedForCx = false;
            kPassport.IsNeedToCut = true && sapOitm.ItemName.Contains("מפוצל");
            _context.KPassport.Add(kPassport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (KPassportExists(kPassport.K_PassportId))
                {
                    return Conflict();
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            return CreatedAtAction("GetKPassport", new { id = kPassport.K_PassportId }, kPassport);
        }

        #endregion

        #region DELETE
        // DELETE: api/KPassports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKPassport(int id)
        {
            var kPassport = await _context.KPassport.FindAsync(id);
            if (kPassport == null)
            {
                return NotFound();
            }

            _context.KPassport.Remove(kPassport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KPassportExists(int id)
        {
            return _context.KPassport.Any(e => e.K_PassportId == id);
        }


        #endregion
    }

}
