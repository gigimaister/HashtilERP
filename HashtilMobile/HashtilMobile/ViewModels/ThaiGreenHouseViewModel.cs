using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HashtilMobile.ViewModels
{
    public class ThaiGreenHouseViewModel
    {
        // Command
        public ICommand ScanCommand { private set; get; }
        public ICommand LogoutCommand { private set; get; }
        public ThaiGreenHouseViewModel()
        {
            ScanCommand = new Command(Scan);
            LogoutCommand = new Command(Logout);
        }
        private async void Scan()
        {         
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();
            if (result != null)
            {
               
            }
        }

        private async void Logout()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }
    }   
}
