using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HashtilERP.Data;
using Microsoft.AspNetCore.Identity;
using HashtilERP.Server.Models;
using Microsoft.AspNetCore.Authorization;

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
                    case "1":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus == Status.GrowingRoom)
                            .Include(e=>e.KPassportInsertAudit)
                           .Include(e=>e.Passport)
                           .ThenInclude(e=>e.Passprods)
                           .Include(e=>e.Passport)
                           .ThenInclude(e=>e.Oitm)
                            .ToListAsync();
                        break;

                    case "2":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus == Status.WaitingForOK)
                           .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Oitm)
                           .ToListAsync();
                        break;

                    case "3":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus.Trim() == Status.InGreenHouse.Trim())
                            .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Oitm)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.UpdateK_PassportAudit)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                        break;

                    case "4":
                        ChosenList = await _context.KPassport.Where(x => x.PassportStatus == Status.InGreenHouse && x.IsNeedToBeAudit == true)
                            .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Oitm)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.UpdateK_PassportAudit)
                           .Include(e => e.k_PassportAuditTblVer2s)
                           .ToListAsync();
                        break;
                    case "5":
                        ChosenList = await _context.KPassport.Where(x => x.IsNeedToBeChecked == true)
                            .Include(e => e.KPassportInsertAudit)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Passprods)
                           .Include(e => e.Passport)
                           .ThenInclude(e => e.Oitm)
                           .Include(e => e.PassportAuditForms)
                           .Include(e => e.UpdateK_PassportAudit)
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


        //GET FOR OBJECT RETURN TO RAZOR PAGE
        [HttpGet("Thai/GreenHouse/{id}")]
        public async Task<ActionResult<K_Passport>> GetThaiKPassport(int id)
        {

            var kPassport = await _context.KPassport.Where(x=>x.PassportNum==id).FirstOrDefaultAsync();

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
            if (kPassport.PassportAvg == null)
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
            try
            {
                 sap = await _context.Passport.Where(X => X.DocNum == kPassport.PassportNum).FirstAsync();
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
            kPassport.UserName = screenName;
            kPassport.SowDate = sap.UDateSow;
            kPassport.DateEnd = sap.UDateEnd;
            kPassport.PassportStartingAVG = Convert.ToInt32((sap.UQuanProd*1000)/(sap.UTraySow));
            kPassport.GrowingDays = Convert.ToInt32(((TimeSpan)(sap.UDateEnd - sap.UDateSow)).Days);
            kPassport.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.MagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.PlantsAmount = sap.UQuanProd*1000;
            kPassport.PassportStatus = Status.GrowingRoom;
            kPassport.ItemCode = sap.UItemCode;
            kPassport.SapDocEntry = sap.DocEntry;
            kPassport.PassportCondition = Status.NotChecked;
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


        
    }




}
