using Plugin.Fingerprint;
using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class CreateFingerprintPage : ContentPage
    {
        private bool canCancel;
        
        public CreateFingerprintPage(bool canCancel)
        {
            InitializeComponent();

            this.canCancel = canCancel;
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
                    await DisplayAlert("Success", "You have enabled the Fingerprint ID", "OK");
                    Helpers.Settings.IsInitialized = true;
                    Helpers.Settings.typeOfLogin = "Fingerprint";
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    var answer = await DisplayAlert("Failure", "Try Again?", "Yes", "No");
                    if (answer)
                    {
                        Application.Current.MainPage = new CreateFingerprintPage(canCancel);
                    }
                    else
                    {
                        Application.Current.MainPage = new CreateAccountPage(canCancel);
                    }
                }
            }
            else 
            {
                await DisplayAlert("Failure", "Sorry your phone doesnt support fingerprint", "OK");
                Application.Current.MainPage = new CreateAccountPage(canCancel);
            }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new CreateAccountPage(canCancel);
        }
    }
}
