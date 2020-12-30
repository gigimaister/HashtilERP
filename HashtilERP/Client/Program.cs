using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HashtilERP.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc1MDI1QDMxMzgyZTM0MmUzMEVYdGVwTXNBdWlhTDNqZkVmSXJ1Q25KQThSQzlhY3FCZlhXaER2cmxHc009;Mzc1MDI2QDMxMzgyZTM0MmUzMG1Pci9nVlZUS2tBMENYM3hmS29xT1pSUmJyNDFSaGhGQXJWWXNMY21NTW89");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("HashtilERP.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("HashtilERP.ServerAPI"));

            builder.Services.AddApiAuthorization();
            builder.Services.AddSyncfusionBlazor();
            await builder.Build().RunAsync();
        }
    }
}
