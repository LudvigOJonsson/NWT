using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NWT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomizationPage : ContentPage
	{
        bool Tutorial = true;
        public CustomizationPage()
		{
            InitializeComponent ();

            BackgroundColor = App.MC;
            StartButton.BackgroundColor = App.MC;
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
            if (!Tutorial)
            {
                App.SideMenu.UpdateTags();
            }

            Button.IsEnabled = false;
            CheckStartButton();
        }
        void TagButtonClicked(object sender, System.EventArgs e)
        {

            var Button = (Button)sender;
            App.SideMenu.Tags.Add(Button.ClassId);
            
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
                    Tutorial = false;
                }
            }
            else
            {
                StartButton.IsEnabled = false;
                StartButton.IsVisible = false;
                Tutorial = false;
            }
        }

        public async void StartButtonPressed(object sender, EventArgs e)
        {
            App.MC = Color.FromHex("#649FD4");
            App.Startpage.Detail = new NavigationPage(App.Mainpage) { BarBackgroundColor = App.MC, BarTextColor = Color.FromHex("#FFFFFF"), };

            var x = (ProfilePage)App.Mainpage.Children[2];
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

            App.SideMenu.UpdateTags();
            var y = (CustomNewsFeed)App.Mainpage.Children[0];
            y.TagUpdate();
            App.LoggedinUser.TutorialProgress = 1;
            App.database.UpdateTutorialProgress(App.LoggedinUser);
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