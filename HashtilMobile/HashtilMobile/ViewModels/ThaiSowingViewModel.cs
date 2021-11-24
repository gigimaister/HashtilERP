using HashtilMobile.Models;
using HashtilMobile.Services;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;

namespace HashtilMobile.ViewModels
{
    public class ThaiSowingViewModel
    {
        public ObservableCollection<SfSegmentItem> SegmentItems { get; set; }
        // For Init Growing Room Segment Index
        public string SegmentItem { get; set; }

        // MobileUser
        MobileUser mobileuser { get; set; } = Functions.GetMobileUser();

        // Command
        public ICommand ScanCommand { private set; get; }
        public ICommand LogoutCommand { private set; get; }

        // Select Change Command
        public Command SelectionChangedCommand { get; set; }
      

        //For Http Req
        IRestService _rest = DependencyService.Get<IRestService>();
        public ThaiSowingViewModel()
        {
            SegmentItems = new ObservableCollection<SfSegmentItem>
        {
            new SfSegmentItem(){Text="1",FontColor=Color.FromHex("#6a993c")},
            new SfSegmentItem(){Text="2",FontColor=Color.FromHex("#6a993c")},
            new SfSegmentItem(){Text="3",FontColor=Color.FromHex("#6a993c")},
            new SfSegmentItem(){Text="4",FontColor=Color.FromHex("#6a993c")},
            new SfSegmentItem(){Text="5",FontColor=Color.FromHex("#6a993c")},
            new SfSegmentItem(){Text="6",FontColor=Color.FromHex("#6a993c")},
        };
            // Getting First Obj If No Prep Was Found
            SegmentItem = SegmentItems[Preferences.Get("GrowingRoomSelectedIndex", 0)].Text;

            ScanCommand = new Command(Scan);
            LogoutCommand = new Command(Logout);

            SelectionChangedCommand = new Command<Syncfusion.XForms.Buttons.SelectionChangedEventArgs>(SelectionChanged);
        }

        private async void Scan()
        {

            // Set GrowingRoom Selected Index
            
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var result = await scanner.Scan();

            if (result != null)
            {
                
                Console.WriteLine("Scanned Barcode: " + result.Text);
                
                K_Passport passport = new K_Passport();
                try
                {                  
                    passport.PassportNum = Convert.ToInt32(result.ToString());
                    passport.GrowingRoom = SegmentItems[Preferences.Get("GrowingRoomSelectedIndex", 0)].Text;
                    passport.UserName = mobileuser.UserName;
                }
                catch(Exception)
                {
                    await Application.Current.MainPage.DisplayAlert(Constants.Thai_Error, Constants.Thai_PassportScanError, Constants.OK);

                }
                await _rest.PostPassportAsync($"{Constants.Urls.BaseUrl}/{Constants.Urls.PostKPassport}", passport);
            }
                
        }
        public void SelectionChanged(Syncfusion.XForms.Buttons.SelectionChangedEventArgs obj)
        {
            Preferences.Set("GrowingRoomSelectedIndex", obj.Index);
            

        }
        private async void Logout()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }

    }
}
