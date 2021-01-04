using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashtilERP.Server.Pages
{

    public class RoleModel: ModelBinderAttribute
    {

        [BindProperty]
        public string UserId { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public bool IsSelected { get; set; }

       
    }
  
}
