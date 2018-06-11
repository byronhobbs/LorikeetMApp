using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class ContactPage : ContentPage
    {
		private Data.MemberDatabase database;
        private static string databasePath = DependencyService.Get<Data.IFileHelper>().GetLocalFilePath("MemberSQLite.db");
		private int memberID = -1;

        public ContactPage(int memberID)
        {
            InitializeComponent();

			this.memberID = memberID;
			database = Data.MemberDatabase.Create(databasePath);
        }

		protected override async void OnAppearing()
        {
            base.OnAppearing();

			try
			{
				((App)App.Current).ResumeContactId = -1;

				listView.ItemsSource = await database.GetContactItemsAsync(memberID);
			}
            catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

        }

		async void OnListContactSelected(object sender, SelectedItemChangedEventArgs e)
        {
			((App)App.Current).ResumeContactId = (e.SelectedItem as ModelsLinq.ContactSQLite).ContactId;

            await Navigation.PushAsync(new ContactIDPage
            {
				BindingContext = e.SelectedItem as ModelsLinq.ContactSQLite
            });
        }
    }
}
