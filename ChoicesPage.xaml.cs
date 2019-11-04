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
    public partial class ChoicesPage : ContentPage
    {

        public string Filter = "";
        public string Author = "";
        public string Tag = "";
        public bool TaglistUpdate = false;
        public List<string> Categories = new List<string>();
        public List<string> Tags = new List<string>();
        public List<string> Authors = new List<string>();

        public static TapGestureRecognizer TGR = new TapGestureRecognizer();
        public static TapGestureRecognizer TGRAll = new TapGestureRecognizer();




        int Rownr = 1;

        public ChoicesPage()
        {

            InitializeComponent();


            TGR.NumberOfTapsRequired = 1;
            TGR.Tapped += (s, e) => {
                IsEnabled = false;
                App.SideMenu.Trash(s, e);
                IsEnabled = true;
            };

            TGRAll.NumberOfTapsRequired = 1;
            TGRAll.Tapped += (s, e) => {
                IsEnabled = false;
                AllNews(s, e);
                IsEnabled = true;
            };



        }

     

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(App.LoggedinUser != null)
            {
                UserSettingsB.IsEnabled = true;
                LogoutB.IsEnabled = true;
            } else
            {
                UserSettingsB.IsEnabled = false;
                LogoutB.IsEnabled = false;
            }

            TaglistCheck();

        }

        public void TaglistCheck()
        {
            if (TaglistUpdate)
            {
                UpdateTags();
                TaglistUpdate = false;
            }
            
        }



        public void UpdatingSideMenu()
        {
            if (App.LoggedinUser != null)
            {
                UserSettingsB.IsEnabled = true;
                LogoutB.IsEnabled = true;
            }
            else
            {
                UserSettingsB.IsEnabled = false;
                LogoutB.IsEnabled = false;
            }
        }
        public void SetTags(){
            Rownr = 1;
            NewsGridOri.RowDefinitions.Clear();
            NewsGridOri.RowDefinitions.Add(new RowDefinition { Height = 60 });
            //Reassign Infotext
            NewsGridOri.Children.Add(ChoiceLabel, 1, 8, 0, 1);
            MakeAllButton("",0);

            var TagList = JsonConvert.DeserializeObject<List<List<string>>>(App.LoggedinUser.TaggString);
            Categories = TagList[0];
            Tags = TagList[1];
            Authors = TagList[2];

            foreach (string s in Categories)
            {
                MakeButton(s,0);
            }
            foreach (string s in Tags)
            {
                MakeButton(s,1);

            }
            foreach (string s in Authors)
            {
                MakeButton(s,2);
            }


        }

        public void MakeButton(string s, int Type)
        {
            var Box = new Subject(s, Type);





            NewsGridOri.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            NewsGridOri.RowDefinitions.Add(new RowDefinition { Height = 5 });
            NewsGridOri.Children.Add(Box.Box, 2, 7, Rownr, Rownr + 1); //Boxview
            NewsGridOri.Children.Add(Box.Label, 2, 7, Rownr, Rownr + 1); //Label
                                                                         //NewsGridOri.Children.Add(Box.BellImage, 6, 7, Rownr, Rownr + 1); //Label
            NewsGridOri.Children.Add(Box.TrashImage, 6, 7, Rownr, Rownr + 1); //Boxview







            Rownr++;
            Rownr++;
        }
        public void MakeAllButton(string s, int Type)
        {
            var Box = new Subject("Alla Nyheter", Type);





            NewsGridOri.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            NewsGridOri.RowDefinitions.Add(new RowDefinition { Height = 5 });
            NewsGridOri.Children.Add(Box.AllBox, 2, 7, Rownr, Rownr + 1); //Boxview
            NewsGridOri.Children.Add(Box.Label, 2, 7, Rownr, Rownr + 1); //Label
                                                                         //NewsGridOri.Children.Add(Box.BellImage, 6, 7, Rownr, Rownr + 1); //Label
            //NewsGridOri.Children.Add(Box.TrashImage, 6, 7, Rownr, Rownr + 1); //Boxview







            Rownr++;
            Rownr++;
        }

        public async void UpdateTags()
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Console.WriteLine(App.LoggedinUser.TutorialProgress);
                    if (App.TutorialSafety)
                    {
                        App.LS.loadingAnimation.Play();
                        await Navigation.PushAsync(App.LS);
                        App.LS.LoadingText.Text = "Uppdaterar Taggar.";
                    }
                    
                });


                //ANVÄND KOD HÄR
                List<List<string>> Taglist = new List<List<string>>
            {
                Categories,
                Tags,
                Authors
            };
                App.LoggedinUser.TaggString = JsonConvert.SerializeObject(Taglist);
                App.database.UpdateChoices(App.LoggedinUser);
                
                

                Device.BeginInvokeOnMainThread( () =>
                {

                    //ANVÄND GRAFISK KOD HÄR
                    NewsGridOri.Children.Clear();
                    SetTags();


                });
                var CNP = (CustomNewsFeed)App.Mainpage.Children[0];
                CNP.TagsModified = true;


                await System.Threading.Tasks.Task.Delay(1000);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Console.WriteLine("Initiering Klar");

                    await Navigation.PopAsync();
                    
                    
                    
                });
                App.TutorialSafety = true;
            });




 
        }



    public class Subject
        {
            public Button Box = new Button { };
            public Button AllBox = new Button { };
            public Label Label = new Label { };
            public Image TrashImage = new Image { };
            public Image BellImage = new Image { };

            public Subject(string s, int Type)
            {

                Label = new Label
                {
                    Text = s,
                    FontSize = 20,
                    TextColor = Color.Black,
                    InputTransparent = true,
                    VerticalOptions = LayoutOptions.Center,

                };

                //Label.GestureRecognizers.Add(TGR);

                Box = new Button
                {
                    BackgroundColor = Color.White,
                    ClassId = s,
                    HeightRequest = 30
                };
                AllBox = new Button
                {
                    BackgroundColor = Color.White,
                    ClassId = s,
                    HeightRequest = 30
                };
                AllBox.Clicked += App.SideMenu.AllNews;

                if (Type == 0)
                {
                    Box.Clicked += App.SideMenu.SetKategori;
                }
                else if (Type == 1)
                {
                    Box.Clicked += App.SideMenu.SetTag;
                }
                else if (Type == 2)
                {
                    Box.Clicked += App.SideMenu.SetAuthor;
                }

                TrashImage = new Image
                {
                    Source = "Icon_Trash.png",
                    WidthRequest = 10,
                    HeightRequest = 10,
                    ClassId = s
                };
                TrashImage.GestureRecognizers.Add(TGR);

                BellImage = new Image
                {
                    Source = "Icon_Bell.png",
                    WidthRequest = 10,
                    HeightRequest = 10,
                };

            }
        }

        //-------------------------------------------------------------

        public void SetKategori(object sender, EventArgs e)
        {
            Clear(sender,e);
            var Sender = (Button)sender;
            Filter = Sender.ClassId;
            PrintNews(sender, e);
        }
        public void SetTag(object sender, EventArgs e)
        {
            Clear(sender, e);
            var Sender = (Button)sender;
            Tag = Sender.ClassId;
            PrintNews(sender, e);
        }
        public void SetAuthor(object sender, EventArgs e)
        {
            Clear(sender, e);
            var Sender = (Button)sender;
            Author = Sender.ClassId;
            PrintNews(sender, e);
        }

        public void Trash(object sender, EventArgs e)
        {

            var Sender = (Image)sender;
            var Trash = Sender.ClassId;

            foreach (string s in Categories)
            {
                if (s.Contains(Trash))
                {
                    Categories.Remove(s);
                    UpdateTags();                    
                    break;
                }
                
            }
            foreach (string s in Tags)
            {
                if (s.Contains(Trash))
                {
                    Tags.Remove(s);
                    UpdateTags();
                    break;
                }
                
            }
            foreach (string s in Authors)
            {
                if (s.Contains(Trash))
                {
                    Authors.Remove(s);
                    UpdateTags();
                    break;
                }
                
            }
         
        }


        public void Clear(object sender, EventArgs e)
        {
            Filter = "";
            Author = "";
            Tag = "";
        }

        public void AllNews(object sender, EventArgs e)
        {
            Clear(sender, e);
            PrintNews(sender, e);
            //App.Startpage.IsPresented = false;
        }


        public async void PrintNews(object sender, EventArgs e)
        {
            ButtonLock();
            await System.Threading.Tasks.Task.Run(async () =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    
                    App.LS.loadingAnimation.Play();
                    await Navigation.PushAsync(App.LS);
                    App.LS.LoadingText.Text = "Updaterar Dina Val Nyhetsflödet.";

                });


                
                NewsGridPage Page = (NewsGridPage)App.Mainpage.Children[1];
                App.database.LocalExecute("DELETE FROM NF");

                Page.PREV = 0;
                Page.CURR = NewsGridPage.DBLN;
                Page.NEXT = NewsGridPage.DBLN * 2;
                Page.Loadnr = 1;
                Page.Filter = Filter;
                Page.Author = Author;
                Page.Tag = Tag;
                Page.ArticleList.Clear();
                Page.LoadLocalDB();


                Device.BeginInvokeOnMainThread( () =>
                {

                    //ANVÄND GRAFISK KOD HÄR
                    Page.AddNews(0);

                    Page.ArticleListView.ItemsSource = null;
                    Page.ArticleListView.ItemsSource = Page.ArticleList;


                });

                if (Filter != "")
                    Page.ChangeName(Filter);
                if (Author != "")
                    Page.ChangeName(Author);
                if (Tag != "")
                    Page.ChangeName(Tag);

                await System.Threading.Tasks.Task.Delay(1000);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Console.WriteLine("Initiering Klar");

                    await Navigation.PopAsync();
                    //App.Mainpage.CurrentPage = App.Mainpage.Children[1];
                });

            });
            ButtonLock();








            

        }

        public void ButtonLock()
        {
            foreach (var Child in NewsGridOri.Children)
            {
                Child.IsEnabled = !Child.IsEnabled;
            }


            /*Nyheter.IsEnabled = !Nyheter.IsEnabled;
            Sport.IsEnabled = !Sport.IsEnabled;
            Ekonomi.IsEnabled = !Ekonomi.IsEnabled;
            NöjeochKultur.IsEnabled = !NöjeochKultur.IsEnabled; 
            Åsikter.IsEnabled = !Åsikter.IsEnabled;
            Familj.IsEnabled = !Familj.IsEnabled;*/
            UserSettingsB.IsEnabled = !UserSettingsB.IsEnabled;
            AboutB.IsEnabled = !AboutB.IsEnabled;
            LogoutB.IsEnabled = !LogoutB.IsEnabled;

            /*Tibro.IsEnabled = !Tibro.IsEnabled;
            Skövde.IsEnabled = !Skövde.IsEnabled;
            Falkköping.IsEnabled = !Falkköping.IsEnabled;
            Karlsborg.IsEnabled = !Karlsborg.IsEnabled;
            Rensa.IsEnabled = !Rensa.IsEnabled;
            Verkställ.IsEnabled = !Verkställ.IsEnabled;
            */

        }


        async void Logout(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            ProfilePage page = (ProfilePage)App.Mainpage.Children[2];
            await button.ScaleTo(0.8f, 80, Easing.BounceOut);
            await button.ScaleTo(1, 80, Easing.BounceOut);
            page.Logout();
            ButtonLock();
        }
        async void Settings(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            ProfilePage page = (ProfilePage)App.Mainpage.Children[2];
            await button.ScaleTo(0.8f, 80, Easing.BounceOut);
            await button.ScaleTo(1, 80, Easing.BounceOut);
            App.Startpage.IsPresented = false;
            page.Settings(sender, e);
            ButtonLock();
        }
        async void About(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.ScaleTo(0.8f, 80, Easing.BounceOut);
            await button.ScaleTo(1, 80, Easing.BounceOut);
            await Navigation.PushAsync(new AboutPage());
            ButtonLock();
        }
        async void UserSettings(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.ScaleTo(0.8f, 80, Easing.BounceOut);
            await button.ScaleTo(1, 80, Easing.BounceOut);
            await Navigation.PushAsync(new UserSettingsPage());
            ButtonLock();
        }
        async void Customization(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.ScaleTo(0.8f, 80, Easing.BounceOut);
            await button.ScaleTo(1, 80, Easing.BounceOut);

            CustomizationPage c = new CustomizationPage();
            //c.fromSidemenu = true;
            await App.Mainpage.Navigation.PushAsync(c);

            App.Startpage.IsPresented = false;

            ButtonLock();
        }


    }
}