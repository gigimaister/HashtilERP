using HashtilERP.DBTestVol1;
using HashtilERP.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace HashtilERP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcellController : ControllerBase
    {
        private readonly OrdersContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ExcellController(OrdersContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //Create COM Objects. Create a COM object for everything that is referenced
        static Excel.Application xlApp = new Excel.Application();
        static Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"J:\\קובי\\מערכת השתיל-כללי\\העברת מצאי מאקסל לשרת");
        static Excel._Worksheet xlWorksheet = (Excel._Worksheet)xlWorkbook.Sheets[1];
        static Excel.Range xlRange = xlWorksheet.UsedRange;

        [HttpGet("GetExcell")]
        public async Task<int> GetExcell()
        {
            var t = xlWorksheet;
            return 0;
        }


    } 

    
}
