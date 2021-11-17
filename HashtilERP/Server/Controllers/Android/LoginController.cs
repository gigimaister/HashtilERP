using HashtilERP.Data;
using HashtilERP.Server.Models;
using HashtilERP.Shared.Models.Mobile;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Server.Controllers.Android
{
    [Route("api/Android/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HashtilERPContext _context;
        public LoginController(HashtilERPContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region POST    

        [HttpPost("PostLogin")]
        public async Task<ActionResult<MobileUser>> PostLogin(MobileUser mobileUser)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(mobileUser.UserName);
            bool isValidUser = await _userManager.CheckPasswordAsync(user, mobileUser.Password);

            if (isValidUser)
            {
                mobileUser.Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
                mobileUser.ScreenName = user.ScreenName;
                return mobileUser;
            }

            return Unauthorized();
        }
        #endregion
    }
}
