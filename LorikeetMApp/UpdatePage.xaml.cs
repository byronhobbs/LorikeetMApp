using System;
using System.Collections.Generic;
using System.Diagnostics;
using Syncfusion.SfBusyIndicator.XForms;
using Syncfusion.XForms.ProgressBar;
using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class UpdatePage : ContentPage
    {
        public static Data.MemberManager memberManager { get; private set; }
        private static Data.MemberDatabase database;
        private static string databasePath = DependencyService.Get<Data.IFileHelper>().GetLocalFilePath("MemberSQLite.db");
		Button UpdateButton = new Button();
		SfBusyIndicator busyIndicator = new SfBusyIndicator();

        public UpdatePage()
        {
            InitializeComponent();
            
            memberManager = new Data.MemberManager(new Data.RestService());
            database = Data.MemberDatabase.Create(databasePath);

			busyIndicator.AnimationType = AnimationTypes.Ball;
            busyIndicator.ViewBoxWidth = 150;
            busyIndicator.ViewBoxHeight = 150;
            busyIndicator.TextColor = Color.Blue;
			busyIndicator.IsBusy = false;
			busyIndicator.IsVisible = false;
            
			UpdateButton.Text = "Update Data";
			UpdateButton.BackgroundColor = Color.AntiqueWhite;
			UpdateButton.HorizontalOptions = LayoutOptions.CenterAndExpand;
			UpdateButton.VerticalOptions = LayoutOptions.CenterAndExpand;
			UpdateButton.HeightRequest = 70;
			UpdateButton.WidthRequest = 100;
			UpdateButton.Clicked += UpdateButton_PressedAsync;
			this.child2.Children.Add(UpdateButton);
        }
        
        async void UpdateButton_PressedAsync(object sender, EventArgs e)
        {
            try
			{
				UpdateButton.IsVisible = false;
				Label errorLabel = new Label();
				child1.Children.Add(errorLabel);
				errorLabel.Text = "Trying to connect to server...";
                errorLabel.IsVisible = true;

				List<Models.Member> Members = await memberManager.GetMembersAsync();
				List<Models.Contact> Contacts = await memberManager.GetContactsAsync();

				if (Members != null || Contacts != null)
				{
					errorLabel.IsVisible = false;
                    busyIndicator.IsVisible = true;
                    busyIndicator.IsBusy = true;
					this.child1.Children.Clear();
                    this.child1.Children.Add(busyIndicator);

					busyIndicator.Title = "Erasing Database...";
					var membersToDelete = await database.GetMemberItemsAsync();
					database.DeleteMemberItems(membersToDelete);
					var contactsToDelete = await database.GetContactItemsAsync();
					database.DeleteContactItems(contactsToDelete);

					busyIndicator.Title = "Adding Members...";

					var MyProgressBar = new ProgressBar();
					MyProgressBar.Progress = 0.0;
					this.child2.Children.Add(MyProgressBar);
					double progress = 0.0;
					int count = 0;

					int progressCount = Members.Count;

					foreach (var item in Members)
					{
						ModelsLinq.MemberSQLite modelToAdd = new ModelsLinq.MemberSQLite();
						modelToAdd.MemberId = item.MemberId;
						modelToAdd.Surname = item.Surname;
						modelToAdd.FirstName = item.FirstName;
						modelToAdd.DateOfBirth = item.DateOfBirth;
						modelToAdd.Sex = item.Sex;
						modelToAdd.Aboriginal = item.Aboriginal;
						modelToAdd.StreetAddress = item.StreetAddress;
						modelToAdd.PostCode = item.PostCode;
						modelToAdd.Suburb = item.Suburb;
						modelToAdd.State = item.State;
						modelToAdd.Country = item.Country;
						modelToAdd.TelephoneNumber = item.TelephoneNumber;
						modelToAdd.MobileNumber = item.MobileNumber;
						modelToAdd.EmailAddress = item.EmailAddress;
						modelToAdd.DateAltered = item.DateAltered;

						await database.SaveMemberItemAsync(modelToAdd);
						count++;
						progress = (double)(decimal.Divide(count, progressCount));
						await MyProgressBar.ProgressTo(progress, 1, Easing.Linear);
					}

					busyIndicator.Title = "Adding Contacts...";

					count = 0;
					progressCount = Contacts.Count;
					foreach (var item in Contacts)
					{
						ModelsLinq.ContactSQLite contactToAdd = new ModelsLinq.ContactSQLite();
						contactToAdd.ContactId = item.ContactId;
						contactToAdd.MemberId = item.MemberId;
						contactToAdd.ContactType = item.ContactType;
						contactToAdd.ContactName = item.ContactName;
						contactToAdd.ContactAddress = item.ContactAddress;
						contactToAdd.ContactPhone = item.ContactPhone;

						await database.SaveContactItemAsync(contactToAdd);
						count++;
						progress = (double)(decimal.Divide(count, progressCount));
						await MyProgressBar.ProgressTo(progress, 1, Easing.Linear);
					}
					busyIndicator.EnableAnimation = false;
					busyIndicator.Title = "Finished...";
				}
				else 
				{
					errorLabel.Text = "Error connecting to server...";
					UpdateButton.IsVisible = true;
				}
            }   
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
