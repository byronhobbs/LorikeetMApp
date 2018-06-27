using System;
using System.Collections.Generic;
using System.Diagnostics;
using LorikeetMApp.ViewModels;
using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class CreatePINPage : ContentPage
    {
        public CreatePINPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            var viewModel = new PinAddViewModel();

            viewModel.PinViewModel.Success += (object sender, EventArgs e) =>
            {
                Application.Current.MainPage = new MainMenu();
            };
            base.BindingContext = viewModel;
        }

        protected override bool OnBackButtonPressed() => false;
    }
}
