﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NWT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        public SettingsPage()
		{
			InitializeComponent ();
            Fullname.Text = App.LoggedinUser.Name;
            UserEmail.Text = App.LoggedinUser.Email;
            UserCity.Text = App.LoggedinUser.City;
            UserAge.Text = App.LoggedinUser.Age.ToString();
        }

        public void ChangePassword()
        {
            if(App.database.TokenCheck())
            {
                App.database.ChangePassword(NPass.Text,RPass.Text);
            }
        }

        public async void UpdateInfo()
        {
            if (App.database.TokenCheck())
            {
                bool Change = false;

                if(App.LoggedinUser.Name != Fullname.Text)
                {
                    App.LoggedinUser.Name = Fullname.Text;
                    Change = true;
                }
                    
                if(App.LoggedinUser.Email != UserEmail.Text)
                {
                    App.LoggedinUser.Email = UserEmail.Text;
                    Change = true;
                }
                    
                if(App.LoggedinUser.City != UserCity.Text)
                {
                    App.LoggedinUser.City = UserCity.Text;
                    Change = true;
                }
                    

                
                int j;
                if (Int32.TryParse(UserAge.Text, out j))
                {
                    if(j != App.LoggedinUser.Age)
                    {
                        App.LoggedinUser.Age = j;
                        Change = true;
                    }
                }
                else
                {
                    Change = false;
                }
                    

                if(Change)
                    App.database.UpdateInfo(App.LoggedinUser);
                else
                    await DisplayAlert("Bad Credentials", "Please type in new information and/or a valid age", "OK");
            }
        }
    }
}