using HashtilMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HashtilMobile.Services
{
    public interface IRestService
    {
        // Depriciated
        Task<MobileUser> LoginAsync(MobileUser mobileUser);
        // Depriciated
        Task<MobileUser> PostAsync(MobileUser mobileUser);

        //Post Any Object
        Task<T> PostJsonAsync<T>(string url, T obj);

        #region Passport
        Task PostPassportAsync<T>(string url, MobileUser mobileUser, T obj);
        #endregion
    }
}
