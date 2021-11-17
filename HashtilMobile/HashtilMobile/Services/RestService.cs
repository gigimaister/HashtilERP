using HashtilMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HashtilMobile.Services
{
    public class RestService : IRestService
    {
        #region OLD - STILL IN USE
        // Depriciated
        public async Task<MobileUser> LoginAsync(MobileUser mobileUser)
        {
            string url = "";

            //For Https req
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            HttpClient client = new HttpClient(httpClientHandler);
            
            var response = await client.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<MobileUser>(result);

                return json;
            }
            return null;
        }
        // Depriciated
        public async Task<MobileUser> PostAsync(MobileUser mobileUser) 
        {
            string controllerUrl = "Login/PostLogin";
            //For Https req
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            HttpClient client = new HttpClient(httpClientHandler);
            string jsonData = JsonConvert.SerializeObject(mobileUser);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{Constants.Urls.BaseUrl}/{controllerUrl}", content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {              
                var json = JsonConvert.DeserializeObject<MobileUser>(result);
                return json;
            }
            return mobileUser;

        }
        #endregion

        #region POST
        public async Task<T> PostJsonAsync<T>(string url, T obj)
        {
            //For Https req
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            HttpClient client = new HttpClient(httpClientHandler);

            string jsonData = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{url}", content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<T>(result);
                return json;
            }
            return obj;
        }
        #endregion
    }

}
