using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using HashtilERP.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace HashtilERP.Server.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Administrator")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        const string ADMINISTRATION_ROLE = "Administrator";
        const string ADMINISTRATOR_USERNAME = "Admin@hashtil";
        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "אימייל")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "סיסמא")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "אישור סיסמא")]
            [Compare("Password", ErrorMessage = "!סיסמאות לא תואמות")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "פלאפון")]
            public string PhoneNum { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email ,PhoneNumber = Input.PhoneNum};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    // Ensure there is a ADMINISTRATION_ROLE
                    var RoleResult = await _roleManager
                    .FindByNameAsync(ADMINISTRATION_ROLE);
                    if (RoleResult == null)
                    {
                        // Create ADMINISTRATION_ROLE role.
                        await _roleManager
                        .CreateAsync(new
                       IdentityRole(ADMINISTRATION_ROLE));
                    }
                    if (user.UserName.ToLower() ==
                    ADMINISTRATOR_USERNAME.ToLower())
                    {
                        // Put admin in Administrator role.
                        await _userManager
                        .AddToRoleAsync(user, ADMINISTRATION_ROLE);
                    }
                    // Log user in.
                    await _signInManager.SignInAsync(user, isPersistent:
                   false);
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
