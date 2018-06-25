using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class MemberPage : ContentPage
    {
        private ModelsLinq.MemberSQLite member;

        public MemberPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 

            member = (ModelsLinq.MemberSQLite)this.BindingContext;

            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), member.MemberId + ".jpg");
            if (!File.Exists(filePath))
            {

            }
            else
            {
                MemberImage.Source = ImageSource.FromFile(filePath);
            }
            Debug.WriteLine(filePath);
        }

        async void OnContactClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ContactPage(member.MemberId));
		}

        async void OnTakePictureClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TakePicture(member.MemberId));
        }
    }
}
