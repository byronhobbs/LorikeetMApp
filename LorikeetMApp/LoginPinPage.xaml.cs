using System;
using System.Collections.Generic;
using LorikeetMApp.ViewModels;
using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class LoginPinPage : ContentPage
    {
        public LoginPinPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            var viewModel = new PinAuthViewModel();

            viewModel.PinViewModel.Success += (object sender, EventArgs e) =>
            {
                Application.Current.MainPage = new MainMenu();
            };
            base.BindingContext = viewModel;
        }

        protected override bool OnBackButtonPressed() => false;
    }
}
