using HashtilERP.DBTestVol1;
using HashtilERP.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HashtilERP.Server.Controllers.Drivers
{
    [Route("api/Drivers/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly OrdersContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DriversController(OrdersContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region GET REGION
        //GET Drivers From Asp Net Identity!
        [HttpGet("GetListOfDrivers")]
        public async Task<List<Shared.Models.Drivers.Driver>> GetListOfDrivers()
        {
            var drivers = new List<Shared.Models.Drivers.Driver>();
            try
            {
                var driversList = await _userManager.GetUsersInRoleAsync("Driver");

                foreach (var driver in driversList)
                {
                    var _driver = new Shared.Models.Drivers.Driver
                    {
                        DriverId = driver.Id,
                        UserName = driver.UserName,
                        ScreenName = driver.ScreenName,
                        PhoneNumber = driver.PhoneNumber,
                    };

                    drivers.Add(_driver);
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            return drivers;
        }
        #endregion

        #region PUT
        [HttpPut("UpdateDriverForOrder")]
        public async Task<IActionResult> UpdateDriverForOrder(DriversToOrder driver)
        {
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;       
            
            _context.Entry(driver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Ok();
        }

        #endregion

        #region POST

        [HttpPost("PostDriver")]
        public async Task<ActionResult<Shared.Models.Drivers.Driver>> PostDriver(DriversToOrder driver)
        {                     
            var user = await _userManager.GetUserAsync(User);
            var screenName = user.ScreenName;
           
            _context.DriversToOrder.Add(driver);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(501, e.Message);
            }

            return Ok();
        }
        #endregion

    }
}
