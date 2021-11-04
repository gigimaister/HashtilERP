using HashtilERP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HashtilERP.Client
{
    public class PushNotHttp
    {
        private readonly HttpClient httpClient;

        public PushNotHttp(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task SubscribeToNotifications(NotificationSubscription subscription)
        {
            var response = await httpClient.PutAsJsonAsync("api/Notifications/subscribe", subscription);
            response.EnsureSuccessStatusCode();
        }
    }
}
