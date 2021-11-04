using HashtilMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HashtilMobile.Services
{
    public interface IRestService
    {
        Task<MobileUser> Login(string usr, string pwd);
    }
}
