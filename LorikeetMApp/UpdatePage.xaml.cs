using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class UpdatePage : ContentPage
    {
        public static Data.MemberManager memberManager { get; private set; }
        private static Data.MemberDatabase database;
        private static string databasePath = DependencyService.Get<Data.IFileHelper>().GetLocalFilePath("MemberSQLite.db");
		Button UpdateButton = new Button();
        ActivityIndicator activityIndicator = new ActivityIndicator();
        Label errorLabel = new Label();

        public UpdatePage()
        {
            InitializeComponent();
            
            memberManager = new Data.MemberManager(new Data.RestService());
            database = Data.MemberDatabase.Create(databasePath);

            activityIndicator.WidthRequest = 100;
            activityIndicator.HeightRequest = 100;
            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;
            this.child1.Children.Add(activityIndicator);

            this.child2.Children.Add(errorLabel);

			UpdateButton.Text = "Update Data";
			UpdateButton.BackgroundColor = Color.AntiqueWhite;
			UpdateButton.HorizontalOptions = LayoutOptions.CenterAndExpand;
			UpdateButton.VerticalOptions = LayoutOptions.CenterAndExpand;
			UpdateButton.HeightRequest = 70;
			UpdateButton.WidthRequest = 100;
			UpdateButton.Clicked += UpdateButton_PressedAsync;
			this.child3.Children.Add(UpdateButton);
        }
        
        async void UpdateButton_PressedAsync(object sender, EventArgs e)
        {
            try
			{
				UpdateButton.IsVisible = false;
				
                activityIndicator.IsVisible = true;
                activityIndicator.IsRunning = true;
				errorLabel.Text = "Trying to connect to server...";
                errorLabel.IsVisible = true;

				List<Models.Member> Members = await memberManager.GetMembersAsync();
				List<Models.Contact> Contacts = await memberManager.GetContactsAsync();

				if (Members != null || Contacts != null)
				{
                    errorLabel.Text = "Getting Data...";

					var membersToDelete = await database.GetMemberItemsAsync();
					database.DeleteMemberItems(membersToDelete);
					var contactsToDelete = await database.GetContactItemsAsync();
					database.DeleteContactItems(contactsToDelete);

					var MyProgressBar = new ProgressBar();
                    MyProgressBar.WidthRequest = 150;
					MyProgressBar.Progress = 0.0;
					this.child2.Children.Add(MyProgressBar);
					double progress = 0.0;
					int count = 0;

					int progressCount = Members.Count;

                    errorLabel.Text = "Saving Members to Database...";
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

					count = 0;
					progressCount = Contacts.Count;
                    errorLabel.Text = "Saving Contacts to Database...";
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
                    activityIndicator.IsRunning = false;
                    errorLabel.Text = "Finished Updating...";
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
