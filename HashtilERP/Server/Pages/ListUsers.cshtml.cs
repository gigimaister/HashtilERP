using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashtilERP.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HashtilERP.Server.Pages
{
    [Authorize(Roles = "Administrator")]
    public class ListUsersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ListUsersModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;          
        }

        public List<ApplicationUser> users { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            users = await _userManager.Users.ToListAsync();
            return Page();
        }
    }
}
