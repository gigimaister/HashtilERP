using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ScreenName { get; set; }
        public string DepartmentName { get; set; }
        public string Hamama { get; set; }
    }
}
