using HashtilMobile.Models;
using HashtilMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HashtilMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckLoginSplash : ContentPage
    {
        //For Http Req
        IRestService _rest = DependencyService.Get<IRestService>();
        MobileUser mobileUser { get; set; } = new MobileUser();
        public CheckLoginSplash()
        {
            InitializeComponent();
            mobileUser = Functions.GetMobileUser();
            // If No Prep Move To Login
            if (String.IsNullOrEmpty(mobileUser.UserName) || String.IsNullOrEmpty(mobileUser.Password)
                || String.IsNullOrEmpty(mobileUser.Role))
            {
                Task.Run(() => Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MainPage())));
            }
            // If Succsess Login Move To Page By Role
            Task.Run(() => Functions.MoveToPageAsync(mobileUser.Role, true));
        }
    }
}