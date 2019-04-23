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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            BindingContext = this;
            IsBusy = false;
		}


        async void Login(object sender, EventArgs e)
        {
            IsBusy = true;
                //await Navigation.PushAsync(App.LoadingScreen);
            
            if (App.Online && UserLogin.Text != null && UserPassword.Text != null)
            {
               
                

                UserTable User = new UserTable();
                User.Username = UserLogin.Text;
                User.Email = UserLogin.Text;
                User.Password = UserPassword.Text;


                App.database.Login(User);
                if (App.LoggedinUser != null)
                {
                    //App.database.Plustoken(App.LoggedinUser, 3);

                    App.Startpage.Detail = new NavigationPage(App.Mainpage) { BarBackgroundColor = Color.FromHex("#2f6e83"), BarTextColor = Color.FromHex("#FFFFFF"), };
                    
                    

                    var x = (ProfilePage)App.Mainpage.Children[2];
                    x.Login(App.LoggedinUser);
                    App.Mainpage.CurrentPage = App.Mainpage.Children[0];
                    IsBusy = false;
                    
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
                                    A.CheckImage.Source = "checkmark.png";
                                    //A.Box.BorderColor = Color.FromRgb(80, 210, 194);
                                });

                            }
                        }                       
                    }
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
            //await Navigation.PopAsync();
        }

        async void Register(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }



    }
}