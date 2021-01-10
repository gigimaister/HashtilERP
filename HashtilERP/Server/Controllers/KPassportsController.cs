using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        
        private readonly HashtilERPContext _context;

        public KPassportsController(HashtilERPContext context)
        {
            _context = context;
           
        }

        // GET: api/KPassports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KPassport>>> GetKPassport()
        {
            var usr =  HttpContext.User?.Identity?.Name;
            return await _context.KPassport.Where(x=>x.PassportStatus==Status.GrowingRoom)
                .ToListAsync();

        }

        // GET: api/KPassports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KPassport>> GetKPassport(int id)
        {

            var kPassport = await _context.KPassport.FindAsync(id);

            if (kPassport == null)
            {
                return NotFound();
            }

            return kPassport;
        }

        // PUT: api/KPassports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKPassport(int id, KPassport kPassport)
        {
            if (id != kPassport.KPassportId)
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

        // POST: api/KPassports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KPassport>> PostKPassport(KPassport kPassport)
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
            
            
           
            var dup = await _context.KPassport.Where(X => X.PassportNum == kPassport.PassportNum).FirstOrDefaultAsync();
            //if duplicate in K_Passport
            if (dup != null)
            {
                return StatusCode(500, "DUPLICATE");
            }

            kPassport.SowDate = sap.UDateSow;
            kPassport.OriginalMagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.MagashAmount = Convert.ToInt32(sap.UTraySow);
            kPassport.PlantsAmount = Convert.ToInt32(sap.UQuanProd);
            kPassport.PassportStatus = Status.GrowingRoom;
            _context.KPassport.Add(kPassport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KPassportExists(kPassport.KPassportId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKPassport", new { id = kPassport.KPassportId }, kPassport);
        }

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
            return _context.KPassport.Any(e => e.KPassportId == id);
        }
    }
}
