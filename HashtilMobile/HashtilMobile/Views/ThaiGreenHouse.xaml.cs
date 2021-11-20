using HashtilMobile.ViewModels;
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
    public partial class ThaiGreenHouse : ContentPage
    {
        public ThaiGreenHouse()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            var vm = (ThaiGreenHouseViewModel)BindingContext;
            return true;
        }
    }
}