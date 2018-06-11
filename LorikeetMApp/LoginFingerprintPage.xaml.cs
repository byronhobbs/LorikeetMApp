using System;
using System.Collections.Generic;
using Plugin.Fingerprint;
using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class LoginFingerprintPage : ContentPage
    {
        public LoginFingerprintPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var result = await CrossFingerprint.Current.IsAvailableAsync(false);

            if (result)
            {
                var auth = await CrossFingerprint.Current.AuthenticateAsync("To Authenticate");

                if (auth.Authenticated)
                {
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    Application.Current.MainPage = new LoginFingerprintPage();
                }
            }
            else
            {
                await DisplayAlert("Failure", "Sorry your phone doesnt support fingerprint", "OK");
                Application.Current.MainPage = new CreateAccountPage();
            }
        }
    }
}
