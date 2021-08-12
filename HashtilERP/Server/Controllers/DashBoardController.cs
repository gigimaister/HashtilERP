using HashtilERP.DBTestVol1;
using HashtilERP.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly OrdersContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DashBoardController(OrdersContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet("GetThaiEmployeesDict")]
        public async Task<Dictionary<int,int>> GetThaiEmployeesDict()
        {
            var thaiDict = new Dictionary<int, int>();

            var thaiWorkers = await _userManager.GetUsersInRoleAsync("Thai-GreenHouse");            
            thaiDict.Add(1,thaiWorkers.Count(x => x.Hamama == "1"));
            thaiDict.Add(2, thaiWorkers.Count(x => x.Hamama == "2"));
            thaiDict.Add(3, thaiWorkers.Count(x => x.Hamama == "3"));
            thaiDict.Add(4, thaiWorkers.Count(x => x.Hamama == "4"));
            thaiDict.Add(5, thaiWorkers.Count(x => x.Hamama == "5"));
            thaiDict.Add(6, thaiWorkers.Count(x => x.Hamama == "6"));
            thaiDict.Add(7, thaiWorkers.Count(x => x.Hamama == "7"));
            
            foreach(var key in thaiDict)
            {
                if(key.Value == 0)
                {
                    thaiDict.Remove(key.Key);
                }
            }

            return thaiDict;
        }








        #region Green Houses Bar Chart
        public class GreenHousesChartData
        {
            public string GreenHouse { get; set; }
            public int? Magash { get; set; }
        }

        public List<GreenHousesChartData> greenHousesChartDatas { get; set; }

        #endregion
        #region Thai
        public class ThaiToGreen
        {
            public int Hamama { get; set; }
            public int NumOfThai { get; set; }
        }
        #endregion
    }
}
