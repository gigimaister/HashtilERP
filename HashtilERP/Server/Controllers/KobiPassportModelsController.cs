using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HashtilERP.Server.Data;
using HashtilERP.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace HashtilERP.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class KobiPassportModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KobiPassportModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/KobiPassportModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KobiPassportModel>>> GetKobiPassportModel()
        {

            return await _context.KobiPassportModel.ToListAsync();
        }

        // GET: api/KobiPassportModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KobiPassportModel>> GetKobiPassportModel(long id)
        {
            var kobiPassportModel = await _context.KobiPassportModel.FindAsync(id);

            if (kobiPassportModel == null)
            {
                return NotFound();
            }

            return kobiPassportModel;
        }

        // PUT: api/KobiPassportModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKobiPassportModel(long id, KobiPassportModel kobiPassportModel)
        {
            if (id != kobiPassportModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(kobiPassportModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KobiPassportModelExists(id))
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

        // POST: api/KobiPassportModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles ="Administrator,Guy-Thai")]
        [HttpPost]
        public async Task<ActionResult> PostKobiPassportModel(KobiPassportModel kobiPassportModel)
        {
            _context.KobiPassportModel.Add(kobiPassportModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKobiPassportModel", new { id = kobiPassportModel.Id }, kobiPassportModel);
        }


        [Authorize(Roles ="Administrator")]
        // DELETE: api/KobiPassportModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKobiPassportModel(long id)
        {
            var kobiPassportModel = await _context.KobiPassportModel.FindAsync(id);
            if (kobiPassportModel == null)
            {
                return NotFound();
            }

            _context.KobiPassportModel.Remove(kobiPassportModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KobiPassportModelExists(long id)
        {
            return _context.KobiPassportModel.Any(e => e.Id == id);
        }
    }
}
