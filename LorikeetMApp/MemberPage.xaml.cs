using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class MemberPage : ContentPage
    {
        public MemberPage()
        {
            InitializeComponent();
        }
        
		async void OnContactClicked(object sender, EventArgs e)
		{
			ModelsLinq.MemberSQLite member = (ModelsLinq.MemberSQLite)this.BindingContext;
         
			await Navigation.PushAsync(new ContactPage(member.MemberId));
		}

        async void OnTakePictureClicked(object sender, EventArgs e)
        {
            ModelsLinq.MemberSQLite member = (ModelsLinq.MemberSQLite)this.BindingContext;

            await Navigation.PushAsync(new TakePicture(member.MemberId));
        }
    }
}
