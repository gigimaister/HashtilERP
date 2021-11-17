using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace HashtilMobile.ViewModels
{
    public class ThaiSowingViewModel
    {
        public ObservableCollection<SfSegmentItem> SegmentItems { get; set; }
        public ThaiSowingViewModel()
        {
            SegmentItems = new ObservableCollection<SfSegmentItem>
        {
            new SfSegmentItem(){Text="XS",FontColor=Color.FromHex("#6a993c")},
            new SfSegmentItem(){Text="S",FontColor=Color.FromHex("#6a993c")},
            new SfSegmentItem(){Text="M",FontColor=Color.FromHex("#6a993c")},
            new SfSegmentItem(){Text="L",FontColor=Color.FromHex("#6a993c")},
            new SfSegmentItem(){Text="XL",FontColor=Color.FromHex("#6a993c")},
        };
        }
    }
}
