using HashtilMobile.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HashtilMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<IRestService, RestService>();

            Device.SetFlags(new string[] { "Shapes_Experimental", "MediaElement_Experimental" });
            MainPage = new MainPage();
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
