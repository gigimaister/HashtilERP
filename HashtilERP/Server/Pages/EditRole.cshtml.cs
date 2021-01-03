using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HashtilERP.Server.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static HashtilERP.Server.Areas.Identity.Pages.Account.LoginModel;

namespace HashtilERP.Server.Pages
{
    public class EditRoleModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public EditRoleModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            Users = new List<string>();
            IdentityRole role = new IdentityRole();
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Parameter]
            public string Id { get; set; }

            [Required]
            [Parameter]
            public string RoleName { get; set; }
        }

        

        public List<string> Users { get; set; }

        public IdentityRole role { get; set; }


        public async Task<IActionResult> OnGet(string id)
        {

            role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    Users.Add(user.UserName);
                }
            }

            return Page();
        }

        

        public async Task<IActionResult> OnPostAsync(string id)
        {

            var role2 = await _roleManager.FindByIdAsync(id);
            if (role2 == null)
            {
                return NotFound();
            }
            else
            {
                role2.Name = Input.RoleName;
                var result = await _roleManager.UpdateAsync(role2);
                if (result.Succeeded)
                {
                    return LocalRedirect("/pages/listroles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
