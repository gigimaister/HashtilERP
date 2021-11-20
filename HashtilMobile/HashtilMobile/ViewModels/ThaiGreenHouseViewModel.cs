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
        public ThaiGreenHouseViewModel()
        {
            ScanCommand = new Command(Scan);
        }
        private async void Scan()
        {         
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();
            if (result != null)
            {
               
            }
        }
    }   
}
