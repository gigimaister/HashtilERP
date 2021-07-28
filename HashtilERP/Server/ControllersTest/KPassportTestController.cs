using HashtilERP.DBTestVol1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Server.ControllersTest
{
    [Route("api/[controller]")]
    public class KPassportTestController : ControllerBase
    {
        private readonly OrdersContext _context;

        public KPassportTestController(OrdersContext context)
        {
            _context = context;
        }

        [HttpGet("GetNewMetzayForProduction1")]
        public async Task<IActionResult> GetNewMetzayForProduction1()
        {
            var t = await _context.KPassportExcelInsert.ToListAsync();

            return Ok();
        }
    }
}
