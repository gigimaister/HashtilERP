using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HashtilERP.Server.Pages
{
    

    public class CreateRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public CreateRoleModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "יש להזין תפקיד")]
            [Display(Name = "תפקיד")]
            public string RoleName { get; set; }
        }

          

        

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
           
            if (ModelState.IsValid)
            {               
                var result = await _roleManager.CreateAsync(new IdentityRole(Input.RoleName));
                if (result.Succeeded)
                {
                    
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
