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

            Task.Run(() => Functions.MoveToPageAsync(mobileUser.Role, true));
            return;

        }
    }
}