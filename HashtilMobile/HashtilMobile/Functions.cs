using HashtilMobile.Models;
using HashtilMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HashtilMobile
{
    public static class Functions
    {
         public  enum Pages
        {
            ThaiSowing,
            ThaiGreenHouseScan,
            Admin,
            Counter,
            GreenHouseManger
        }

        public static Pages GetPage(string role)
        {
            Pages pages = new Pages();
            switch (role)
            {
                case Constants.Roles.Administrator:
                    pages =  Pages.Admin;
                    break;
                case Constants.Roles.ThaiGreenHouse:
                    pages = Pages.ThaiGreenHouseScan;
                    break;
                case Constants.Roles.ThaiGuy:
                    pages = Pages.ThaiSowing;
                    break;
                case Constants.Roles.AuditAvgCounter:
                    pages = Pages.Counter;
                    break;
            }
            return pages;
        }

        // Move To Page Based On Role
        public static async Task MoveToPageAsync(string role, bool blockAlert = false)
        {
            switch (role)
            {
                // Admin
                case Constants.Roles.Administrator:
                    
                    break;

                // Thai Green
                case Constants.Roles.ThaiGreenHouse:
                    await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ThaiGreenHouse()));
                    break;

                // Thai Sowing
                case Constants.Roles.ThaiGuy:
                    await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ThaiSowing()));
                    break;

                // Counter
                case Constants.Roles.AuditAvgCounter:

                    break;

                //If We Got Untill Here, Wrong Pass Or UsrName
                default:
                    if (!blockAlert)
                    {
                        await Application.Current.MainPage.DisplayAlert(Constants.Heb_Error, Constants.Heb_WrongUserPwd, Constants.Heb_Close);
                    }
                    break;
            }

        }

        // Return MobileUser Object From Prep Settings
        public static MobileUser GetMobileUser()
        {
            MobileUser mobileUser = new MobileUser();
            mobileUser.UserName = Preferences.Get("UserName","");
            mobileUser.Password = Preferences.Get("Password", "");
            mobileUser.Role = Preferences.Get("Role", "");

            return mobileUser;
        }

        
        
    }
}
