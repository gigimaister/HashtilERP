using HashtilMobile.Services;
using HashtilMobile.Views;
using System;
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
            var user = Functions.GetMobileUser();
            // Page Sowing
            if(user.Role == Constants.Roles.ThaiGuy)
            {
                MainPage = new ThaiSowing();
            }
            // Page Thai Green House
            else if(user.Role == Constants.Roles.ThaiGreenHouse)
            {
                MainPage = new ThaiGreenHouse();
            }
            // Page Admin
            else if (user.Role == Constants.Roles.Administrator)
            {
                MainPage = new ThaiGreenHouse();
            }
            // Page Counter
            else if (user.Role == Constants.Roles.AuditAvgCounter)
            {
                MainPage = new ThaiGreenHouse();
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
