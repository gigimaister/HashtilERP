using HashtilERP.Data;
using HashtilERP.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Server.Controllers.Android
{
    [Route("api/Android/[controller]")]
    [ApiController]
    public class KPassportController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HashtilERPContext _context;
        public KPassportController(HashtilERPContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


    }
}
