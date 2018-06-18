using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class CreatePasswordPage : ContentPage
    {
        public CreatePasswordPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Helpers.Settings.IsInitialized = true;
            Helpers.Settings.typeOfLogin = "Password";
            Helpers.Settings.Password = Password.Text;
            // load main page
            Application.Current.MainPage = new MainPage();
        }
    }
}
