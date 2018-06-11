using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class CreateAccountPage : ContentPage
    {
        private bool canCancel = false;

		public CreateAccountPage(bool canCancel)
		{
			InitializeComponent();

            this.canCancel = canCancel;

            if (canCancel)
            {
                CancelButton.IsVisible = true;
            }
            else
            {
                CancelButton.IsVisible = false;
            }
        }

        public CreateAccountPage()
        {
            InitializeComponent();

            this.canCancel = false;
        }

        void HandlePIN_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new CreatePINPage();
        }

        void HandlePassword_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new CreatePasswordPage();
        }

        void HandleFingerprint_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new CreateFingerprintPage(canCancel);
        }

        void HandleClose_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}
