using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Syncfusion.Blazor;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using HashtilERP.Shared;
using Blazored.LocalStorage;

namespace HashtilERP.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDgyNzE5QDMxMzkyZTMyMmUzMFdlMHV5cWpjWC9vc1ZwME9oV1R2eVZWYmZwclpiSWhoTStVU3lxbXNMd0E9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("HashtilERP.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("HashtilERP.ServerAPI"));

            builder.Services.AddApiAuthorization();
            builder.Services.AddSyncfusionBlazor();

            builder.Services.AddBlazoredLocalStorage(config =>
      config.JsonSerializerOptions.WriteIndented = true);
            await builder.Build().RunAsync();
        }
    }
}
