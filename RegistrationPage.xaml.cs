using Newtonsoft.Json;
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
                    User.TutorialProgress = 0;
                    User.Style = "#649FD4";

                    var Avatar = new List<string>();
                    Avatar.Add("avatar_face1.png");
                    Avatar.Add("avatar_hair1.png");
                    Avatar.Add("avatar_body1.png");
                    Avatar.Add("avatar_expr4.png");
                    Avatar.Add("nothing.png");

                    User.Avatar = JsonConvert.SerializeObject(Avatar);

                    var Inventory = new List<int>();
                    User.Inventory = JsonConvert.SerializeObject(Inventory);

                    int j;
                    if (Int32.TryParse(UserAge.Text, out j))
                        User.Age = j;
                    else
                        User.Age = 18;
                    App.database.Registration(User);

                    

                    App.Loginpage.UserLogin.Text = User.Username;
                    App.Loginpage.UserPassword.Text = User.Password;

                    await DisplayAlert("Registration Complete", "User " + UserLogin.Text + " has now been created", "OK");
                    await Navigation.PopAsync();
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