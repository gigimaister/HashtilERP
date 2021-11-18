using HashtilMobile.Models;
using HashtilMobile.Services;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace HashtilMobile.ViewModels
{
    public class ThaiSowingViewModel
    {
        public ObservableCollection<SfSegmentItem> SegmentItems { get; set; }
        //Command
        public ICommand ScanCommand { private set; get; }

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
            ScanCommand = new Command(Scan);
        }

        private async void Scan()
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var result = await scanner.Scan();

            if (result != null)
            {
                var url = "";
                Console.WriteLine("Scanned Barcode: " + result.Text);
                MobileUser mobileuser = new MobileUser();
                K_Passport passport = new K_Passport();
                await _rest.PostPassportAsync(url, mobileuser,  passport);
            }
                
        }
    }
}
