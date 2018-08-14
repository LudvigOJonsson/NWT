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
            if(UserLogin.Text != null && UserPassword.Text != null && UserName.Text != null)
            {
                var User = new UserTable();
                User.Username = UserLogin.Text;             
                User.Password = UserPassword.Text;
                User.Email = UserEmail.Text;
                User.Name = UserName.Text;
                User.City = UserCity.Text;
                User.Plustokens = 0;

                int j;
                if (Int32.TryParse(UserAge.Text, out j))
                    User.Age = j;
                else
                    User.Age = 18;
                App.database.Registration(User);
                await DisplayAlert("Registration Complete", "User " + UserLogin.Text + " has now been created", "OK");
            }
            else
            {
                await DisplayAlert("Incorrect Input","Please fill in  the proper Credentials.","OK");
            }
                
        }

    }
}