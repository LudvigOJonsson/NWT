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
    public partial class NewsPage : ContentPage
    {
        public List<string> imageLinks = new List<string>();
        Random rnd = new Random();
        public int red = 248;
        public int green = 248;
        public int blue = 248;
        public System.Timers.Timer Timer;
        public static int ArticleNR;
        public int CC = 8;
        public bool Read = false;

        public NewsPage(RSSTable RSS)
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

            if (App.LoggedinUser != null)
            {
                if (App.database.GetReadArticle(RSS.ID).Count == 0)
                {
                    NewsPageView.BackgroundColor = Color.FromRgb(red, green, blue);
                    Timer = new System.Timers.Timer();
                    Timer.Interval = 60;
                    Timer.Elapsed += OnTimedEvent;
                    Timer.Enabled = true;
                }
                else
                {
                    NewsPageView.BackgroundColor = Color.FromRgb(80, 210, 194);
                    Dot.TextColor = Color.FromRgb(80, 210, 194);
                    TimerButton.BackgroundColor = Color.FromRgb(80, 210, 194);
                    Read = true;
                }

            }
            else
            {
                NewsPageView.BackgroundColor = Color.FromRgb(248, 248, 248);
            }

            LoadNews(RSS);
            App.database.UpdateStats("ArticlesClicked");
            
        }

        async void TimerButtonClicked(object sender, System.EventArgs e)
        {
            //IconRotation();
            Button button = (Button)sender;
            await button.RotateTo(-2, 40, Easing.BounceOut);
            await button.RotateTo(2, 60, Easing.BounceOut);
            await button.RotateTo(0, 40, Easing.BounceOut);
            
            if (TimerButton.BackgroundColor == Color.FromRgb(80, 210, 194) && Read == false)
            {
                var RA = new RAL();
                RA.User = App.LoggedinUser.ID;
                RA.Article = ArticleNR;
                RA.Date = DateTime.Now;
                App.database.ReadArticle(RA);
                TimerIcon.Source = "tokenicon3.png";
                var NG = (NewsGridPage)App.Mainpage.Children[1];
                foreach (NewsGridPage.Article A in NG.ArticleList)
                {
                    if (A.ID == ArticleNR)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            A.Box.BorderColor = Color.FromRgb(80, 210, 194);
                        });

                    }
                }
                App.database.UpdateStats("ArticlesRead");
                App.database.MissionUpdate(App.LoggedinUser, "ArticleRead");
                Read = true;
            }
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
                NewsPageView.BackgroundColor = Color.FromRgb(red, green, blue);
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
                TimerDone(Brödtext);
                
            }
            else
            {
                Timer.Start();
            }

        }

        void LoadNews(RSSTable RSS)
        {

            Rubrik.Text = RSS.Title;
            Dot.Text = "⚫    ";
            Ingress.Text = RSS.Description;
            for (int i = 0; i < 3 + rnd.Next(7); i++)
            {
                Brödtext.Text += "    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ";
            }



            Link.Text = RSS.Link;
            ArticleNR = RSS.ID;
            Date.Text = "  Publicerad: " + RSS.PubDate;
            ArticleImage.Source = imageLinks[rnd.Next(7)];
            if (App.Online)
            {
                LoadComments();
            }

        }

        async void SubmitComment()
        {
            if (App.Online)
            {
                if (App.database.TokenCheck() && (Comment.Text != null || Comment.Text != ""))
                {
                    var CNR = App.database.CommentCount(ArticleNR);
                    var SC = new CommentTable();

                    SC.Article = ArticleNR;
                    SC.CommentNR = CNR;
                    SC.UserSubmitted = 0;
                    SC.User = App.LoggedinUser.ID;
                    SC.Replynr = -1;
                    SC.Replylvl = 0;
                    SC.Comment = Comment.Text;
                    SC.Point = 0;
                    Comment.Text = "";
                    App.database.InsertComment(SC);
                    LoadComments();
                }
                if (App.LoggedinUser != null)
                {
                    App.database.MissionUpdate(App.LoggedinUser, "CommentPosted");
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
            bool UUV = false;
            CommentGrid.Children.Clear();
            foreach (var s in Query)
            {
                var User = App.database.GetUser(s.User).First();
                UpvoteTable Upvote = null;
                var UpvoteList = App.database.GetUpvote(s.CommentNR, ArticleNR, s.UserSubmitted);
                Console.WriteLine("The current Upvotelist: " + UpvoteList);



                if (UpvoteList.Any() && App.LoggedinUser != null)
                {
                    Console.WriteLine("The current Upvotelist First: " + UpvoteList.First());

                    foreach (UpvoteTable UV in UpvoteList)
                    {
                        if (UV.User == App.LoggedinUser.ID)
                        {
                            UUV = true;
                            Upvote = UV;
                            break;
                        }
                    }





                }

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
                var VoteArrowUp = new Button()
                {
                    Image = "uparrow.png",
                    BackgroundColor = Color.Transparent,
                    WidthRequest = 20,
                    HeightRequest = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    Margin = 0,
                    ClassId = "false"
                };
                var VoteNumber = new Label
                {
                    Text = s.Point.ToString(),
                    TextColor = Color.Black,
                    BackgroundColor = Color.Transparent,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = 0,
                    FontSize = 16,

                };
                var VoteArrowDown = new Button()
                {
                    Image = "downarrow.png",
                    BackgroundColor = Color.Transparent,
                    WidthRequest = 20,
                    HeightRequest = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.End,
                    Margin = 0,
                    ClassId = "false"
                };
                /*var Userimage = new Image
                {
                    Source = "snail.png",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };*/

                var Reply = new Button()
                {
                    BackgroundColor = Color.FromHex("#50d2c2"),
                    TextColor = Color.FromHex("#FFFFFF"),
                    WidthRequest = 60,
                    HeightRequest = 30,
                    Text = "Reply",
                    FontSize = 10,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.End,
                    Margin = 20,
                };
                var Elispses = new Button()
                {
                    BackgroundColor = Color.Transparent,
                    WidthRequest = 60,
                    HeightRequest = 30,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.End,
                    Margin = 20,
                    Image = "elipses.png",
                };

                if (App.LoggedinUser != null)
                {
                    App.database.MissionUpdate(App.LoggedinUser, "CommentPosted");

                    if (UUV)
                    {
                        if (Upvote.Point == 1)
                        {
                            VoteArrowUp.ClassId = "true";
                            VoteArrowUp.Image = "uparrowBlue.png";
                        }
                        else if (Upvote.Point == -1)
                        {
                            VoteArrowDown.ClassId = "true";
                            VoteArrowDown.Image = "downarrowRed.png";
                        }
                    }


                }




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

                Elispses.Clicked += async (o, e) => {
                    var action = await DisplayActionSheet("Alternativ", "Avbryt", null, "Markera med Token", "Dela", "Rapportera");
                    Debug.WriteLine("Action: " + action);

                    switch (action)
                    {
                        case "Markera med Token":
                            //DoSomething();
                            break;
                        case "Dela":
                            //DoSomethingElse();
                            break;
                        case "Rapportera":
                            //DoSomethingElse();
                            break;
                    }
                };

                VoteArrowUp.Clicked += (o, e) => {
                    if (App.LoggedinUser != null)
                    {
                        var UV = new UpvoteTable();
                        UV.User = App.LoggedinUser.ID;
                        UV.UserSubmitted = s.UserSubmitted;
                        UV.Article = ArticleNR;
                        UV.CommentNR = s.CommentNR;
                        UV.Point = 0;

                        if (VoteArrowUp.ClassId == "false")
                        {
                            VoteArrowUp.ClassId = "true";
                            VoteArrowUp.Image = "uparrowBlue.png";
                            UpClicked(o, e);
                            UV.Point = 1;
                            App.database.InsertUpvote(UV);
                            if (VoteArrowDown.ClassId == "true")
                            {
                                VoteArrowDown.ClassId = "false";
                                VoteArrowDown.Image = "downarrow.png";
                                App.database.DeleteUpvote(Upvote);
                                UV.Point = 2;
                            }


                        }
                        else
                        {
                            VoteArrowUp.ClassId = "false";
                            VoteArrowUp.Image = "uparrow.png";
                            App.database.DeleteUpvote(Upvote);
                            UV.Point = -1;

                        }

                        App.database.PointUpdate(s, UV.Point);
                        LoadComments();

                    }


                };
                VoteArrowDown.Clicked += (o, e) => {
                    if (App.LoggedinUser != null)
                    {
                        var UV = new UpvoteTable();
                        UV.User = App.LoggedinUser.ID;
                        UV.UserSubmitted = s.UserSubmitted;
                        UV.Article = ArticleNR;
                        UV.CommentNR = s.CommentNR;
                        UV.Point = 0;

                        if (VoteArrowDown.ClassId == "false")
                        {
                            VoteArrowDown.ClassId = "true";
                            VoteArrowDown.Image = "downarrowRed.png";
                            DownClicked(o, e);
                            UV.Point = -1;
                            App.database.InsertUpvote(UV);
                            if (VoteArrowUp.ClassId == "true")
                            {
                                VoteArrowUp.ClassId = "false";
                                VoteArrowUp.Image = "uparrow.png";
                                App.database.DeleteUpvote(Upvote);
                                UV.Point = -2;
                            }


                        }
                        else
                        {
                            VoteArrowDown.ClassId = "false";
                            VoteArrowDown.Image = "downarrow.png";
                            App.database.DeleteUpvote(Upvote);
                            UV.Point = 1;

                        }

                        App.database.PointUpdate(s, UV.Point);
                        LoadComments();

                    }
                };
                async void UpClicked(object sender, System.EventArgs e)
                {
                    await VoteArrowUp.RotateTo(-15, 80, Easing.BounceOut);
                    await VoteArrowUp.RotateTo(15, 120, Easing.BounceOut);
                    await VoteArrowUp.RotateTo(0, 80, Easing.BounceOut);
                }
                async void DownClicked(object sender, System.EventArgs e)
                {
                    await VoteArrowDown.RotateTo(-15, 80, Easing.BounceOut);
                    await VoteArrowDown.RotateTo(15, 120, Easing.BounceOut);
                    await VoteArrowDown.RotateTo(0, 80, Easing.BounceOut);
                }
                //0, 1, Rownr, Rownr + 3
                CommentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                CommentGrid.Children.Add(Box, 0, 6, s.CommentNR, s.CommentNR + 1);
                CommentGrid.Children.Add(Comment, 1, 5, s.CommentNR, s.CommentNR + 1);
                CommentGrid.Children.Add(Username, 1, 5, s.CommentNR, s.CommentNR + 1);
                CommentGrid.Children.Add(VoteArrowDown, 0, 1, s.CommentNR, s.CommentNR + 1);
                CommentGrid.Children.Add(VoteArrowUp, 0, 1, s.CommentNR, s.CommentNR + 1);
                CommentGrid.Children.Add(VoteNumber, 0, 1, s.CommentNR, s.CommentNR + 1);
                //CommentGrid.Children.Add(Userimage, 0, s.CommentNR + 8);
                CommentGrid.Children.Add(Reply, 4, 5, s.CommentNR, s.CommentNR + 1);
                CommentGrid.Children.Add(Elispses, 5, 6, s.CommentNR, s.CommentNR + 1);
            }
        }
    }
}
