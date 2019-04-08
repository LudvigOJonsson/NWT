using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace NWT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserNewsPage : ContentPage
    {
        public List<string> imageLinks = new List<string>();
        Random rnd = new Random();
        public int red = 248;
        public int green = 248;
        public int blue = 248;
        public System.Timers.Timer Timer;
        public int ArticleNR;
        public int CC = 8;
        public bool Read = true;
        public bool CommentLock = false;

        public void MakeComment(CommentTable s)
        {
            var User = App.database.GetUser(s.User).First();

            Console.WriteLine("TEST");
            /*var CommentBox = new BoxView
            {
                Color = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = 3,
            };*/

            var Box = new Button
            {
                CornerRadius = 10,
                Margin = 5,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            var Comment = new Label
            {
                Text = s.Comment,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.Black,
                FontSize = 16,
                WidthRequest = 290,
                Margin = 20,
            };
            var Username = new Label
            {
                Text = "  " + User.Name,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                TextColor = Color.Black,
                FontSize = 16,

            };


            /*var Userimage = new Image
            {
                Source = "snail.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };*/

            var Reply = new Button()
            {
                BackgroundColor = Color.FromHex("#2f6e83"),
                TextColor = Color.FromHex("#FFFFFF"),
                WidthRequest = 60,
                HeightRequest = 30,
                Text = "Reply",
                FontSize = 10,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                Margin = 20,
            };

            Reply.Clicked += async (o, e) => {
                PostClicked(o, e);
                await Navigation.PushAsync(new CommentPage(ArticleNR, s));
            };



            async void PostClicked(object sender, System.EventArgs e)
            {
                Button button = (Button)sender;
                await button.RotateTo(-5, 80, Easing.BounceOut);
                await button.RotateTo(5, 120, Easing.BounceOut);
                await button.RotateTo(0, 80, Easing.BounceOut);
            }

            
            //0, 1, Rownr, Rownr + 3
            UserCommentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            UserCommentGrid.Children.Add(Box, 0, 6, s.CommentNR, s.CommentNR + 1);
            UserCommentGrid.Children.Add(Comment, 1, 5, s.CommentNR, s.CommentNR + 1);
            UserCommentGrid.Children.Add(Username, 1, 5, s.CommentNR, s.CommentNR + 1);
            
            //CommentGrid.Children.Add(Userimage, 0, s.CommentNR + 8);
            //UserCommentGrid.Children.Add(Reply, 5, 6, s.CommentNR, s.CommentNR + 1);
        }



        public UserNewsPage(UserRSSTable RSS)
        {
            InitializeComponent();
            imageLinks.Add("http://media2.hitzfm.nu/2016/11/Nyheter_3472x1074.jpg");
            imageLinks.Add("https://pbs.twimg.com/media/CynmmdYWgAAjky1.jpg");
            imageLinks.Add("https://www.surfertoday.com/images/stories/clouds.jpg");
            imageLinks.Add("https://s-ec.bstatic.com/images/hotel/max1024x768/683/68345961.jpg");
            imageLinks.Add("https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Hertig_Johans_torg%2C_Sk%C3%B6vde%2C_2014_01.JPG/1200px-Hertig_Johans_torg%2C_Sk%C3%B6vde%2C_2014_01.JPG");
            imageLinks.Add("https://cdn2.acsi.eu/5/8/5/2/5852b667270eb.jpeg");
            imageLinks.Add("https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Runder_Berg.JPG/1200px-Runder_Berg.JPG");
            imageLinks.Add("https://thumbs.dreamstime.com/z/online-robber-17098197.jpg");

            if (App.LoggedinUser != null || false)
            {
                bool read = false;
                var History = App.database.GetAllHistory(App.LoggedinUser.ID);
                foreach (HistoryTable HT in History)
                {
                    if(RSS.ID == HT.Article)
                    {
                        read = true;
                        break;
                    }
                }
                if (read)
                {
                    TimerButton.IsEnabled = false;
                    Read = true;
                    TimerIcon.Source = "tokenicon.png";
                    UserNewsPageView.BackgroundColor = Color.FromRgb(80, 210, 194);
                    Dot.TextColor = Color.FromRgb(80, 210, 194);
                    TimerButton.BackgroundColor = Color.FromRgb(80, 210, 194);
                    
                }
                else
                {
                    UserNewsPageView.BackgroundColor = Color.FromRgb(red, green, blue);
                    Timer = new System.Timers.Timer();
                    Timer.Interval = 60;
                    Timer.Elapsed += OnTimedEvent;
                    Timer.Enabled = true;
                }
            }
            else
            {
                UserNewsPageView.BackgroundColor = Color.FromRgb(248, 248, 248);
            }
            LoadNews(RSS);
            
        }


        async void ButtonClicked(object sender, System.EventArgs e)
        {
            //IconRotation();
            Button button = (Button)sender;
            await button.RotateTo(-2, 40, Easing.BounceOut);
            await button.RotateTo(2, 60, Easing.BounceOut);
            await button.RotateTo(0, 40, Easing.BounceOut);

            

        }
        async void TimerButtonClicked(object sender, System.EventArgs e)
        {
            //IconRotation();
            Button button = (Button)sender;
            await button.RotateTo(-2, 40, Easing.BounceOut);
            await button.RotateTo(2, 60, Easing.BounceOut);
            await button.RotateTo(0, 40, Easing.BounceOut);
            App.database.StatUpdate("InsandareRead");
            /*
            if (TimerButton.BackgroundColor == Color.FromRgb(80, 210, 194) && Read == false)
            {
               
                var HT = new HistoryTable();
                HT.User = App.LoggedinUser.ID;
                HT.Article = ArticleNR;
                HT.Readat = DateTime.Now;
                HT.Header = Rubrik.Text;
                HT.Image = imageLinks[rnd.Next(7)];
                App.database.InsertHistory(HT);
                TimerIcon.Source = "tokenicon.png";
                var NG = (UserNewsGridPage)App.Mainpage.Children[0].Navigation.NavigationStack[1];
                foreach (UserNewsGridPage.Article A in NG.ArticleList)
                {
                    if (A.ID == ArticleNR)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            A.Box.BorderColor = Color.FromRgb(80, 210, 194);
                        });

                    }
                }
                Read = true;
                TimerButton.IsEnabled = false;
            }*/
        }
        async void TimerDone(object sender)
        {
            //IconRotation();
            Label label = (Label)sender;
            await label.RotateTo(-2, 40, Easing.BounceOut);
            await label.RotateTo(2, 60, Easing.BounceOut);
            await label.RotateTo(0, 40, Easing.BounceOut);
        }
        /*async void IconRotation()
        {
            Image image = TimerIcon;
            await image.RotateTo(-2, 40, Easing.BounceOut);
            await image.RotateTo(2, 60, Easing.BounceOut);
            await image.RotateTo(0, 40, Easing.BounceOut);
        }*/

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (red != 80)
            {
                red--;
            }
            if (green != 210)
            {
                green--;
            }
            if (blue != 194)
            {
                blue--;
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                UserNewsPageView.BackgroundColor = Color.FromRgb(red, green, blue);
                Dot.TextColor = Color.FromRgb(red, green, blue);
                TimerButton.BackgroundColor = Color.FromRgb(red, green, blue);
            });

            if (green == 210 && blue == 194 && red == 80)
            {

                
                Timer.Stop();
                Timer.Close();
                Timer.Dispose();
                TimerDone(Rubrik);
                TimerDone(Ingress);
                TimerDone(Brodtext);

            }
            else
            {
                Timer.Start();
            }

        }
        async public void LoadReference(object sender, EventArgs e)
        {                       
            var id = Int32.Parse(ClassId);
            if(id != -1)
            {
                var RSS = App.database.GetServerRSS(id).First();
                await Navigation.PushAsync(new NewsPage(RSS,0));
            }           
        }

        void LoadNews(UserRSSTable RSS)
        {
            ClassId = RSS.Referat.ToString();
            Rubrik.Text = RSS.Rubrik;
            Dot.Text = "⚫   ";
            Ingress.Text = RSS.Ingress;
            Brodtext.Text = RSS.Brodtext;
            if(RSS.Author != -1)
            {
                Author.Text = App.database.GetUser(RSS.Author).First().Name;
            }

            
            if(RSS.Referat != -1)
            {
                Link.Text = App.database.GetServerRSS(RSS.Referat).First().Link;
            }
            else
            {
                Link.IsVisible = false;
            }
            
            ArticleNR = RSS.ID;
            Date.Text = "  Publicerad: " + RSS.PubDate;
            ArticleImage.Source = imageLinks[rnd.Next(7)];
            if (App.Online)
            {
                LoadComments();
            }

        }

        async void SubmitComment(object sender, EventArgs e)
        {
            if (App.Online)
            {
                if (App.database.TokenCheck() && (Comment.Text != null || Comment.Text != ""))
                {
                    var CNR = App.database.CommentCount(ArticleNR);
                    var SC = new CommentTable();

                    SC.Article = ArticleNR;
                    SC.CommentNR = CNR;
                    SC.UserSubmitted = 1;
                    SC.User = App.LoggedinUser.ID;
                    SC.Replynr = -1;
                    SC.Replylvl = 0;
                    SC.Comment = Comment.Text;
                    SC.Point = 0;
                    Comment.Text = "";
                    App.database.InsertComment(SC);
                    MakeComment(SC);
                }
            }
            else
            {
                await DisplayAlert("Offline", "The Server is currently Offline. Please try again later.", "OK");
            }

        }



        void LoadComments()
        {
            var Query = App.database.GetComments(ArticleNR,0,-1);
            
            UserCommentGrid.Children.Clear();

            foreach (var s in Query)
            {
                MakeComment(s);
            }
        }

        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }
    }
}
