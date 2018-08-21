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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            
		}


        async void Login(object sender, EventArgs e)
        {
            if (App.Online)
            {
                UserTable User = new UserTable();
                User.Username = UserLogin.Text;
                User.Password = UserPassword.Text;
                App.database.Login(User);
                if (App.LoggedinUser != null)
                {
                    App.Mainpage.Children[2] = new ProfilePage(App.LoggedinUser);
                    App.Mainpage.CurrentPage = App.Mainpage.Children[2];
                    App.database.Plustoken(App.LoggedinUser, 1);


                }
                else
                {
                    await DisplayAlert("Failed Login", "Incorrect Username/Password", "OK");
                }
            }
            else
            {
                await DisplayAlert("Offline", "The Server is currently Offline. Please try again later.", "OK");
            }

        }

        async void Register(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }



    }
}