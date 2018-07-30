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
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage (UserTable User)
		{
			InitializeComponent ();
            Welcome.Text = User.Username;
		}

        void Community()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[0];
        }
        void News()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[1];
        }
        void Profile()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[2];
        }
        void Games()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[3];
        }

        void Logout()
        {
            App.database.Logout();
        }

        async void Settings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}