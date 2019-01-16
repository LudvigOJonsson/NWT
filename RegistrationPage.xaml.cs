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
	public partial class RegistrationPage : ContentPage
	{
		public RegistrationPage ()
		{
			InitializeComponent ();
		}

        async void Register(object sender, EventArgs e)
        {
            if(App.Online)
            { 
                if(UserLogin.Text != null && UserPassword.Text != null && UserName.Text != null)
                {
                    var User = new UserTable();
                    User.Username = UserLogin.Text;             
                    User.Password = UserPassword.Text;
                    User.Email = UserEmail.Text;
                    User.Name = UserName.Text;
                    User.City = UserCity.Text;
                    User.Plustokens = 20;
                    User.AchievementString = "";
                    User.MissionString = "";
                    User.LoginStreak = 0;
                    User.DailyLogin = 0;

                    int j;
                    if (Int32.TryParse(UserAge.Text, out j))
                        User.Age = j;
                    else
                        User.Age = 18;
                    App.database.Registration(User);

                    var Stats = new StatsTable();
                    Stats.User = App.database.GetUserByName(UserLogin.Text).First().ID;
                    Stats.Startups = 0;
                    Stats.Logins = 0;
                    Stats.UseTime = 0;
                    Stats.ArticlesClicked = 0;
                    Stats.ArticlesRead = 0;
                    Stats.PlusArticlesClicked = 0;
                    Stats.PlusArticlesUnlocked = 0;
                    Stats.InsandareSubmitted = 0;
                    Stats.InsandareRead = 0;
                    Stats.GameStarted = 0;
                    Stats.GameFinished = 0;
                    App.database.InsertStats(Stats);
                    await DisplayAlert("Registration Complete", "User " + UserLogin.Text + " has now been created", "OK");
                }
                else
                {
                    await DisplayAlert("Incorrect Input","Please fill in  the proper Credentials.","OK");
                }
            }
            else
            {
                await DisplayAlert("Offline", "The Server is currently Offline. Please try again later.", "OK");
            }
        }

    }
}