using Rg.Plugins.Popup.Services;
using Lottie.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace NWT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
        public ProfilePage ()
		{
			InitializeComponent ();
            
        }

        public void Login(UserTable User)
        {
            Welcome.Text = "Hi, " + User.Username + "!";
            TokenNumber.Text = App.LoggedinUser.Plustokens.ToString();

            //Getting update
            updateMissions();
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await DisplayAlert("Daily Login", "You have logged in " + User.LoginStreak + " days in a row and you get " + User.LoginStreak + " tokens as a reward!", "Nice");

                await PopupNavigation.Instance.PushAsync(new DailyPopUp());
            });
        }

        public void updateMissions()
        {
            var Tasklist = JsonConvert.DeserializeObject<List<Task>>(App.LoggedinUser.MissionString);
            
            m1t1.Text = Tasklist[0].Type;
            m1t2.Text = Tasklist[0].Progress + "/" + Tasklist[0].Goal;
            m2t1.Text = Tasklist[1].Type;
            m2t2.Text = Tasklist[1].Progress + "/" + Tasklist[1].Goal;
            m3t1.Text = Tasklist[2].Type;
            m3t2.Text = Tasklist[2].Progress + "/" + Tasklist[2].Goal;
        }

 



        public void Logout()
        {
            App.database.Logout();
            var NG = (NewsGridPage)App.Mainpage.Children[1];

            foreach (NewsGridPage.Article A in NG.ArticleList)
            {               
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        //A.Box.BorderColor = Color.FromRgb(150, 150, 150);
                    });               
            }
        }
        async void Avatar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AvatarPage());
        }
        async void Settings(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new SettingsPage());
        }
        async void Achivements(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new AchivementsPage());
        }
        async void Favorites(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new NewsGridPage(3));
        }
        async void History(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new NewsGridPage(2));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            updateMissions();
        }
    }
}