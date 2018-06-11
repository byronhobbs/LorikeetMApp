using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using LorikeetMApp.Interfaces;
using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;

namespace LorikeetMApp
{
	public partial class MembersPage : ContentPage
    {
		private Data.MemberDatabase database;
		private static string databasePath = DependencyService.Get<Data.IFileHelper>().GetLocalFilePath("MemberSQLite.db");
		private List<ModelsLinq.MemberSQLite> data = new List<ModelsLinq.MemberSQLite>();

        public MembersPage()
        {
            InitializeComponent();

			database = Data.MemberDatabase.Create(databasePath);
        }
        
		protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
				data = await database.GetMemberItemsAsync();
                
				listView.ItemsSource = data;               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        Label rightSwipe = null;
		ModelsLinq.MemberSQLite memberItem = null;

		private async void PhoneDefaultNumber()
		{
			if (memberItem != null)
			{
				if (memberItem.MobileNumber != "")
				{
					if (await this.DisplayAlert(
					 "Dial Mobile Number",
						"Call " + memberItem.MobileNumber + "?",
					 "Yes",
					 "No"))
					{
						var dialer = DependencyService.Get<IDialer>();
						if (dialer != null)
						{
							dialer.Dial(memberItem.MobileNumber);
						}
					}
				}
				else if (memberItem.TelephoneNumber != "")
				{
					if (await this.DisplayAlert(
					 "Dial Home Phone",
						"Call " + memberItem.TelephoneNumber + "?",
					 "Yes",
					 "No"))
					{
						var dialer = DependencyService.Get<IDialer>();
						if (dialer != null)
						{
							dialer.Dial(memberItem.TelephoneNumber);
						}
					}
				}
				else await DisplayAlert("Warning", "There is no Telephone Associated with this member", "OK");
				this.listView.ResetSwipe();
			}
		}

		private void ListView_Swiping(object sender, SwipingEventArgs e)
		{
            if (e.ItemIndex == 1 && e.SwipeOffSet > 140)
                e.Handled = true;
        }

        private void ListView_SwipeStarted(object sender, SwipeStartedEventArgs e)
        {
			memberItem = null;
        }

        private void ListView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
			memberItem = e.ItemData as ModelsLinq.MemberSQLite;
        }
        
        private void rightLabel_BindingContextChanged(object sender, EventArgs e)
        {
            if (rightSwipe == null)
			{
				rightSwipe = sender as Label;
				(rightSwipe.Parent as View).GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(PhoneDefaultNumber)});
				rightSwipe.Text = "Call";
            }
        }

		async void Handle_ItemDoubleTapped(object sender, Syncfusion.ListView.XForms.ItemDoubleTappedEventArgs e)
        {
			memberItem = e.ItemData as ModelsLinq.MemberSQLite;

			if (memberItem != null)
			{
                await Navigation.PushAsync(new MemberPage
                {
                    BindingContext = memberItem
                });

            }
        }

		SearchBar searchBar = null;

        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            searchBar = (sender as SearchBar);
            if (listView.DataSource != null)
            {
                this.listView.DataSource.Filter = FilterContacts;
                this.listView.DataSource.RefreshFilter();
            }
        }

        private bool FilterContacts(object obj)
        {
            if (searchBar == null || searchBar.Text == null)
                return true;

			var members = obj as ModelsLinq.MemberSQLite;
			if (members.FullName.StartsWith(searchBar.Text, true, CultureInfo.InvariantCulture))
                return true;
            else
                return false;
        }
    }
}
                