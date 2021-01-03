using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HashtilERP.Server.Pages
{
    //[Authorize(Roles = "Administrator")]
    public class ListRolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ListRolesModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<IdentityRole> roles { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            roles = await _roleManager.Roles.ToListAsync();
            return Page();
        }
    }
}
