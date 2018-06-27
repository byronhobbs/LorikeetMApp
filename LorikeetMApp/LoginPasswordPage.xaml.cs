using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class LoginPasswordPage : ContentPage
    {
        public LoginPasswordPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {

            if (Password.Text.Equals(Helpers.Settings.Password))
            {
                Application.Current.MainPage = new MainMenu();
            }
            else
            {
                DisplayAlert("Warning", "Error in password", "OK");
                Application.Current.MainPage = new LoginPasswordPage();
            }
        }
    }
}
