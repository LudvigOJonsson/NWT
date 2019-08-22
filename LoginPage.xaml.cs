using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
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
                            Device.BeginInvokeOnMainThread(() =>
                            {

                                LoadingPopUp x = new LoadingPopUp();
                                x.loadingAnimation.Play();

                                App.Startpage.Detail = x;
                                
                                

                               

                                

                            });
                            StartApp();
                            await System.Threading.Tasks.Task.Delay(1000);
                            Device.BeginInvokeOnMainThread( () =>
                            {
                                Console.WriteLine("Initiering Klar");

                                App.Startpage.Detail = new NavigationPage(App.Mainpage) { BarBackgroundColor = App.MC, BarTextColor = Color.FromHex("#FFFFFF"), };
                                App.Mainpage.CurrentPage = App.Mainpage.Children[0];
                                App.SideMenu.UpdatingSideMenu();
                            });

                        });



                    }
                    App.SideMenu.SetTags();
                    var y = (CustomNewsFeed)App.Mainpage.Children[0];
                    y.TagUpdate();




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

        void Grafiker(object sender, EventArgs e)
        {
            App.Startpage.Detail = new NavigationPage(new GSampleTabbedPage());
            //App.Startpage.Master = new NavigationPage(new GSampleSidemenu());
        }



        public void StartApp()
        {

            var CustomNewsGridPage = (CustomNewsFeed)App.Mainpage.Children[0];
            var NG = (NewsGridPage)App.Mainpage.Children[1];

            NG.CreateFeed(0);
            CustomNewsGridPage.CreateFeed();
            
            var x = (ProfilePage)App.Mainpage.Children[2];
            x.Login(App.LoggedinUser);

           
            

            


        }

    }
}