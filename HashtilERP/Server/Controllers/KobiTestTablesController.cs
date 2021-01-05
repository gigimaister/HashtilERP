using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HashtilERP.Server.Data;
using HashtilERP.Shared.Models;

namespace HashtilERP.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class KobiTestTablesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KobiTestTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/KobiTestTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KobiTestTable>>> GetKobiTestTable()
        {
            return await _context.KobiTestTable.ToListAsync();
        }

        // GET: api/KobiTestTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KobiTestTable>> GetKobiTestTable(string id)
        {
            var kobiTestTable = await _context.KobiTestTable.FindAsync(id);

            if (kobiTestTable == null)
            {
                return NotFound();
            }

            return kobiTestTable;
        }

        // PUT: api/KobiTestTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKobiTestTable(string id, KobiTestTable kobiTestTable)
        {
            if (id != kobiTestTable.Id)
            {
                return BadRequest();
            }

            _context.Entry(kobiTestTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KobiTestTableExists(id))
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

        // POST: api/KobiTestTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KobiTestTable>> PostKobiTestTable(KobiTestTable kobiTestTable)
        {
            _context.KobiTestTable.Add(kobiTestTable);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KobiTestTableExists(kobiTestTable.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKobiTestTable", new { id = kobiTestTable.Id }, kobiTestTable);
        }

        // DELETE: api/KobiTestTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKobiTestTable(string id)
        {
            var kobiTestTable = await _context.KobiTestTable.FindAsync(id);
            if (kobiTestTable == null)
            {
                return NotFound();
            }

            _context.KobiTestTable.Remove(kobiTestTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KobiTestTableExists(string id)
        {
            return _context.KobiTestTable.Any(e => e.Id == id);
        }
    }
}
