using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashtilERP.Server.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HashtilERP.Server.Pages
{
    public class EditUsersInRolesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;       
        public EditUsersInRolesModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;                       
        }

        [BindProperty]
        public List<RoleModel> Users { get; set; } = new List<RoleModel>();

        public async Task<IActionResult> OnGet(string roleId)
        {

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound();
            }
            
            foreach (var user in _userManager.Users.ToList())
            {              
                var roleModel = new RoleModel
                {                   
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    roleModel.IsSelected = true;
                }
                else
                {
                    roleModel.IsSelected = false;
                }
                Users.Add(roleModel);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(List<RoleModel> Users, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }
            for(int i = 0; i < Users.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(Users[i].UserId);
                IdentityResult result = null;
                if (Users[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!Users[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);

                }
                else
                {
                    continue;
                }
            }
            return LocalRedirect("/pages/listroles");

        }
    }
}
