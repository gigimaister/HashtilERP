using HashtilMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HashtilMobile.Services
{
    public class RestService : IRestService
    {
        public async Task<MobileUser> Login(string usr, string pwd)
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
    }
    
}
