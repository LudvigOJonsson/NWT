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
            BindingContext = this;
            
		}


        async void Login(object sender, EventArgs e)
        {

            //await Navigation.PushAsync(App.LoadingScreen);


            
          


            if (App.Online && UserLogin.Text != null && UserPassword.Text != null)
            {


                UserTable User = new UserTable
                {
                    Username = UserLogin.Text,
                    Email = UserLogin.Text,
                    Password = UserPassword.Text
                };


                App.database.Login(User);
                if (App.LoggedinUser != null)
                {

                    //FOR TESTING ONLY
                    //App.LoggedinUser.TutorialProgress = 0;
                    //App.database.UpdateTutorialProgress(App.LoggedinUser);
                    //REMOVE FOR OFFICIAL RELEASE

                    if (App.LoggedinUser.TutorialProgress == 0)
                    {

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            App.Startpage.Detail = new IntroWalkthrough() { };

                        });

                        
                    } else
                    {

                        await System.Threading.Tasks.Task.Run(async () =>
                        {
                            Device.BeginInvokeOnMainThread(async() =>
                            {
                                App.Startpage.Detail = new LoadingPopUp();
                                await System.Threading.Tasks.Task.Delay(1000);
                                App.Startpage.Detail.BackgroundColor = Color.Red;

                            });
                            await System.Threading.Tasks.Task.Delay(3000);
                            StartApp();

                        });



                    }
                }
                else
                {
                    
                        

                    


                    await DisplayAlert("Failed Login", "Incorrect Username/Password", "OK");



                }
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Startpage.Detail = App.Loginpage;

                });

                await DisplayAlert("Offline", "The Server is currently Offline. Please try again later.", "OK");
            }
            //await Navigation.PopAsync();
        }

        async void Register(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        public void StartApp()
        {

            

            App.Startpage.Detail = new NavigationPage(App.Mainpage) { BarBackgroundColor = Color.FromHex("#2f6e83"), BarTextColor = Color.FromHex("#FFFFFF"), };

            


            
            
            var x = (ProfilePage)App.Mainpage.Children[3];
            x.Login(App.LoggedinUser);
            App.Mainpage.CurrentPage = App.Mainpage.Children[0];

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
                            //A.CheckImage.Source = "checkmark.png";
                            //A.Box.BorderColor = Color.FromRgb(80, 210, 194);
                        });

                    }
                }
            }
            /*
            App.SideMenu.SetTags();
            var y = (CustomNewsFeed)App.Mainpage.Children[0];
            y.TagUpdate();
            */

            
        }

    }
}