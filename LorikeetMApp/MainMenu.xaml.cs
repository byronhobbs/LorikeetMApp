using System;
using System.Collections.Generic;
using LorikeetMApp.Models;
using Xamarin.Forms;

namespace LorikeetMApp
{
    public partial class MainMenu : MasterDetailPage
    {
        public List<MainMenuItem> MainMenuItems { get; set; }

        public MainMenu()
        {
            BindingContext = this;

            MainMenuItems = new List<MainMenuItem>()
            {
                new MainMenuItem() { Title = "Members Page", Icon = "menu_inbox.png", TargetType = typeof(MembersListPage) },
                new MainMenuItem() { Title = "Update", Icon = "menu_stock.png", TargetType = typeof(UpdatePage) },
                new MainMenuItem() { Title = "Change Login", Icon = "menu_stock.png", TargetType = typeof(CreateAccountPage)}
            };

            Detail = new NavigationPage(new MembersListPage());

            InitializeComponent();
        }

        public void MainMenuItem_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMenuItem;
            if (item != null)
            {
                if (item.Title.Equals("Members Page"))
                {
                    Detail = new NavigationPage(new MembersListPage());
                }
                else if (item.Title.Equals("Update"))
                {
                    Detail = new NavigationPage(new UpdatePage());
                }
                else if (item.Title.Equals("Change Login"))
                {
                    Detail = new NavigationPage(new CreateAccountPage(true));
                }

                MenuListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
