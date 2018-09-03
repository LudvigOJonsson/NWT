using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NWT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage (UserTable User)
		{
			InitializeComponent ();
            Welcome.Text = User.Username;
		}

        public void Logout()
        {
            App.database.Logout();
            var NG = (NewsGridPage)App.Mainpage.Children[1];
            foreach (NewsGridPage.Article A in NG.ArticleList)
            {               
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        A.Frame.Color = Color.FromRgb(150, 150, 150);
                    });               
            }
        }

        async void Settings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
        async void Achivements(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AchivementsPage());
        }
        async void History(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryPage());
        }
        async void Missions(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MissionsPage());
        }
    }
}