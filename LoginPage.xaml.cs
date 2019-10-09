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

            

            var properties = App.Current.Properties;
            if (properties.ContainsKey("username"))
            {
                UserLogin.Text = (string)properties["username"];
            }

            if (properties.ContainsKey("password"))
            {
                UserPassword.Text = (string)properties["password"];
            }


        }

        void LoginCheck()
        {
            App.LoggedinUser = new UserTable();
            

            var Test = App.database.GetUserStats(1);
            if(Test != null)
            {
                App.Online = true;
            }

            App.LoggedinUser = null;
        }



        async void Login(object sender, EventArgs e)
        {

            //await Navigation.PushAsync(App.LoadingScreen);
            Button button = (Button)sender;
            await button.ScaleTo(0.8f, 100, Easing.BounceOut);
            await button.ScaleTo(1f, 100, Easing.BounceOut);

            LoginCheck();




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
                   /* App.LoggedinUser.TutorialProgress = 0;
                    App.database.UpdateTutorialProgress(App.LoggedinUser);*/
                    //REMOVE FOR OFFICIAL RELEASE

                    if (App.LoggedinUser.TutorialProgress == 0)
                    {
                        App.TutorialSafety = false;
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            App.Startpage.Detail = new IntroWalkthrough() { };

                        });

                        
                    }
                    else
                    {
                        
                        await System.Threading.Tasks.Task.Run(async () =>
                        {
                            
                            Device.BeginInvokeOnMainThread(() =>
                            {

                                
                                App.LS.loadingAnimation.Play();

                                App.Startpage.Detail = App.LS;


                                App.LS.LoadingText.Text = "Appen laddas in.";
                               

                                

                            });
                            StartApp();
                            await System.Threading.Tasks.Task.Delay(1000);
                            Device.BeginInvokeOnMainThread( () =>
                            {
                                Console.WriteLine("Initiering Klar");
                                
                                App.Startpage.Detail = new NavigationPage(App.Mainpage) { BarBackgroundColor = App.MC, BarTextColor = Color.FromHex("#FFFFFF"), };
                                App.Mainpage.CurrentPage = App.Mainpage.Children[0];
                                App.SideMenu.UpdatingSideMenu();
                                App.database.UpdateTutorialProgress(App.LoggedinUser);
                                var MP = App.Mainpage;
                                var PP = (ProfilePage)MP.Children[2];
                                PP.ChangeIntroStep(App.LoggedinUser.TutorialProgress);
                            });

                        });
                        
                        App.SideMenu.SetTags();

                        //var y = (CustomNewsFeed)App.Mainpage.Children[0];
                        //y.TagUpdate();
                        
                        StylePage.ColorFunction(Color.FromHex(App.LoggedinUser.Style));

                    }

                    var properties = App.Current.Properties;
                    if (!properties.ContainsKey("username"))
                    {
                        properties.Add("username", UserLogin.Text);
                    }
                    else
                    {
                        properties["username"] = UserLogin.Text;
                    }

                    if (!properties.ContainsKey("password"))
                    {
                        properties.Add("password", UserPassword.Text);
                    }
                    else
                    {
                        properties["password"] = UserPassword.Text;
                    }
                    if (!properties.ContainsKey("showingress"))
                    {
                        properties.Add("showingress", true);
                    }


                    //Använd detta för att reseta en användares Tutorial
                    //App.LoggedinUser.TutorialProgress = 0; 
                    //App.database.UpdateTutorialProgress(App.LoggedinUser);


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

        void Grafiker(object sender, EventArgs e)
        {
            App.Startpage.Detail = new NavigationPage(new GSampleTabbedPage());
            //App.Startpage.Master = new NavigationPage(new GSampleSidemenu());
        }

   
        public void StartApp()
        {

            var CustomNewsGridPage = (CustomNewsFeed)App.Mainpage.Children[0];
            var NG = (NewsGridPage)App.Mainpage.Children[1];
            Device.BeginInvokeOnMainThread(() =>
            {
                App.LS.LoadingText.Text = "Laddar in Dina Val";
            });
            
            NG.CreateFeed(0);
            Device.BeginInvokeOnMainThread(() =>
            {

            });

            CustomNewsGridPage.CreateFeed();
            Device.BeginInvokeOnMainThread(() =>
            {
                App.LS.LoadingText.Text = "Laddar in det samlade Nyhetsflödet";
            });

            var x = (ProfilePage)App.Mainpage.Children[2];
            x.Login(App.LoggedinUser);

            Device.BeginInvokeOnMainThread(() =>
            {
                App.LS.LoadingText.Text = "Updaterar Profilsidan";
            });





        }

    }
}