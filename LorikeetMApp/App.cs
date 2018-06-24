using System.Diagnostics;
using LorikeetMApp.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LorikeetMApp
{
	public class App : Application
	{
        public int ResumeMemberId { get; set; }
		public int ResumeContactId { get; set; }

		public App()
		{
            //Helpers.Settings.IsInitialized = false;
			var isInitialized = LorikeetMApp.Helpers.Settings.IsInitialized;
			if (isInitialized)
			{
                var typeOfLogin = LorikeetMApp.Helpers.Settings.typeOfLogin;
                if (typeOfLogin.Equals("Pin"))
                {
                    MainPage = new LoginPinPage();
                }
                else if (typeOfLogin.Equals("Password"))
                {
                    MainPage = new LoginPasswordPage();
                }
                else if (typeOfLogin.Equals("Fingerprint"))
                {
                    MainPage = new LoginFingerprintPage();
                }
                else
                {
                    MainPage = new CreateAccountPage(false);
                }
			}
            else
            {
                MainPage = new CreateAccountPage(false);
            }
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}