using HashtilMobile.Models;
using HashtilMobile.Services;
using HashtilMobile.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HashtilMobile.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Command
        public ICommand LoginCommand { private set; get; }

        //For Http Req
        IRestService _rest = DependencyService.Get<IRestService>();

        MobileUser mobileUser { get; set; } = new MobileUser();

        //User Name
        private string _userName;
        public string UserName
        {
            get => _userName; 
            set 
            {
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }
        //Password
        private string _password;
        public string Password
        {
            get =>_password; 
            set
            {
                _password = value;
                NotifyPropertyChanged("Password");
            }
        }
        //Loading Spinner
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                NotifyPropertyChanged("IsBusy");
            }
        }
        //Pop Up
        private bool _isPopup;
        public bool IsPopUp
        {
            get => _isPopup;
            set
            {
                _isPopup = value;
                NotifyPropertyChanged("IsPopUp");
            }
        }

        public MainPageViewModel()
        {
            IsPopUp = false;
            LoginCommand = new Command(SendLogin);           
        }

        private async void SendLogin(object obj)
        {
            //If Null Or Empty
            if(String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert(Constants.Heb_Error, Constants.Heb_UserAndPwdEmpty, Constants.Heb_Ok);
            }
            else
            {
                try
                {
                    mobileUser.UserName = UserName;
                    mobileUser.Password = Password;                   

                    // Spinner
                    IsBusy = true;

                    // Send Login
                    var user = await _rest.PostAsync(mobileUser);

                    // Save Login Settings For Auto Login
                    Preferences.Set("UserName", UserName);
                    Preferences.Set("Password", Password);
                    Preferences.Set("Role", user.Role);

                    // If Succsess Login Move To Page By Role
                    await Functions.MoveToPageAsync(user.Role);
                    IsBusy = false;
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        #region PropertyChanged
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
