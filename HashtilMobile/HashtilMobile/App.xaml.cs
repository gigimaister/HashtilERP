using HashtilMobile.Services;
using HashtilMobile.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HashtilMobile
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM1OTUyQDMxMzkyZTMzMmUzMGd2aWxLeXVEcGVtVkhxL1c5UE9MWUZ1Nm1LeHRab1dhZGw2SEdDUU1OYkk9");

            InitializeComponent();
            DependencyService.Register<IRestService, RestService>();

            Device.SetFlags(new string[] { "Shapes_Experimental", "MediaElement_Experimental" });

            //For Http Req
            IRestService _rest = DependencyService.Get<IRestService>();

            var user = Functions.GetMobileUser();
            var validUser = Task.Run(() => _rest.PostAsync(user));
           
            // Page Sowing
            if(validUser.Result.Role == Constants.Roles.ThaiGuy)
            {
                MainPage = new NavigationPage(new ThaiSowing());
            }
            // Page Thai Green House
            else if(validUser.Result.Role == Constants.Roles.ThaiGreenHouse)
            {
                MainPage = new NavigationPage(new ThaiGreenHouse());
            }
            // Page Admin
            else if (validUser.Result.Role == Constants.Roles.Administrator)
            {
                MainPage = new NavigationPage(new ThaiGreenHouse());
            }
            // Page Counter
            else if (validUser.Result.Role == Constants.Roles.AuditAvgCounter)
            {
                MainPage = new NavigationPage(new ThaiGreenHouse());
            }
            // Else - Login
            else
            {
                MainPage = new MainPage();
            }
            
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
