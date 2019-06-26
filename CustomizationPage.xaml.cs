﻿using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NWT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomizationPage : ContentPage
	{
		public CustomizationPage()
		{
            InitializeComponent ();

            CheckStartButton();

            foreach (var Box in CustomizationGrid.Children)
            {
                var Category = (Button)Box;


                if (App.SideMenu.Categories.Contains(Category.ClassId) || App.SideMenu.Tags.Contains(Category.ClassId))
                {
                    Box.IsEnabled = false;
                }


            }



        }

        void CategoryButtonClicked(object sender, System.EventArgs e)
        {

            var Button = (Button)sender;
            App.SideMenu.Categories.Add(Button.ClassId);
            App.SideMenu.UpdateTags();
            Button.IsEnabled = false;
            CheckStartButton();
        }
        void TagButtonClicked(object sender, System.EventArgs e)
        {

            var Button = (Button)sender;
            App.SideMenu.Tags.Add(Button.ClassId);
            App.SideMenu.UpdateTags();
            Button.IsEnabled = false;
            CheckStartButton();
        }

        void CheckStartButton()
        {
            if (App.LoggedinUser.TutorialProgress == 0)
            {
                if (App.SideMenu.Tags.Count > 0 || App.SideMenu.Categories.Count > 0)
                {
                    StartButton.IsEnabled = true;
                }
                else
                {
                    StartButton.IsEnabled = false;
                }
            }
            else
            {
                StartButton.IsEnabled = false;
                StartButton.IsVisible = false;
            }
        }

        public async void StartButtonPressed(object sender, EventArgs e)
        {
            App.Startpage.Detail = new NavigationPage(App.Mainpage) { BarBackgroundColor = Color.FromHex("#2f6e83"), BarTextColor = Color.FromHex("#FFFFFF"), };

            var x = (ProfilePage)App.Mainpage.Children[3];
            x.Login(App.LoggedinUser);
            App.Mainpage.CurrentPage = App.Mainpage.Children[0];

            var History = App.database.GetAllHistory(App.LoggedinUser.ID);

            var NG = (NewsGridPage)App.Mainpage.Children[1];
            foreach (NewsGridPage.Article A in NG.ArticleList)
            {
                foreach (HistoryTable HT in History)
                {
                    if (A.ID == HT.Article)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            //A.CheckImage.Source = "checkmark.png";
                            //A.Box.BorderColor = Color.FromRgb(80, 210, 194);
                        });

                    }
                }
            }

            App.SideMenu.SetTags();
            var y = (CustomNewsFeed)App.Mainpage.Children[0];
            y.TagUpdate();

            await PopupNavigation.Instance.PushAsync(new TutorialPopUp1());
        }

        public void FollowButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "Follow")
            {
                button.Text = "Unfollow";
            } else if (button.Text == "Unfollow")
            {
                button.Text = "Unfollow";
            }
        }
    }
}