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

        void Logout()
        {
            App.database.Logout();
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