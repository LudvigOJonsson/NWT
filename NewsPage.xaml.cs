﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace NWT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        Random rnd = new Random();
        public int red = 248;
        public int green = 248;
        public int blue = 248;
        public System.Timers.Timer Timer;
        public static int ArticleNR;
        public int CC = 8;
        public bool Read = false;
        public int Row = 5;
        public bool Topimg = true;


        public NewsPage(RSSTable RSS)
        {
            InitializeComponent();

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

 

        void LoadNews(RSSTable RSS)
        {
            ArticleNR = RSS.ID;
            Rubrik.Text = RSS.Title;
            Dot.Text = "⚫";
            Ingress.Text = "    "+RSS.Description;
            Top.Text = "  Publicerad: " + RSS.PubDate + "   "+RSS.Source;
            Author.Text = RSS.Author;
            Category.Text = "Nyhetskategorier: "+RSS.Category;

            if(RSS.Tag != "")
            {
                Tags.Text = "Tags: " + RSS.Tag;
            }
                   
            ArticleImage.Source = RSS.ImgSource;


            var Order = JsonConvert.DeserializeObject<List<int>>(RSS.Ordning);
            var Text = JsonConvert.DeserializeObject<List<string>>(RSS.Text);
            var Images = JsonConvert.DeserializeObject<List<string>>(RSS.Images);
            var ImageText = JsonConvert.DeserializeObject<List<string>>(RSS.Imagetext);

            int Count = 0;
            int TextCount = 0;
            int ImageCount = 0;
            

            //ArticleGrid.Children.Add(BG, 0, 6, 0, Row+Order.Count);

            foreach (int Type in Order)
            {

                /*
                Device.BeginInvokeOnMainThread(async () => {
                    await App.Mainpage.DisplayAlert("Test", Node.OuterXml, "Exit");
                });*/
                if (Type == 0)
                {                   
                        ArticleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        
                        
                        
                        var Label = new Label
                        {
                            


                            Text = "    " + Text[TextCount],
                            HorizontalTextAlignment = TextAlignment.Start,
                            VerticalTextAlignment = TextAlignment.Start,
                            FontSize = 14,
                            TextColor = Color.Black,                        
                            Margin = 10
                        };
                        ArticleGrid.Children.Add(Label, 0, 6, Row, Row + 1);
                        Row++;
                        Count++;
                        TextCount++;
                }
                else if (Type == 1)
                {
                        var IMGText = new Label
                        {
                            Text = "    " + ImageText[ImageCount],
                            HorizontalOptions = LayoutOptions.Start ,
                            VerticalOptions = LayoutOptions.End,
                            FontAttributes = FontAttributes.Italic,
                            HorizontalTextAlignment = TextAlignment.Start,
                            VerticalTextAlignment = TextAlignment.Start,
                            FontSize = 9,
                            TextColor = Color.Gray,
                            Margin = 0
                        };
                        
                        if (Topimg == true)
                        {
                            ArticleImage.Source = Images[ImageCount];
                            ArticleGrid.Children.Add(IMGText, 0, 6, 1, 2);
                            Topimg = false;
                        }
                        else
                        {

                            var Image = new Image
                            {
                                WidthRequest = 200,
                                HeightRequest = 300,
                                Aspect = Aspect.AspectFill,
                                Margin = 5,
                            };

                            ArticleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            ArticleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            Image.Source = Images[ImageCount];
                            ArticleGrid.Children.Add(Image, 0, 6, Row, Row + 1);
                            ArticleGrid.Children.Add(IMGText, 0, 6, Row+1, Row + 2);
                            Row++;
                            Row++;
                        }
                        Count++;
                        ImageCount++;
                }
            }
            

            ArticleGrid.Children.Add(BG, 0, 6, 0, Row);
            ArticleGrid.Children.Add(BackGround, 0, 6, Row + 1, Row + 4);
            ArticleGrid.Children.Add(TimerButton, 0, 6, Row+1, Row + 2);
            ArticleGrid.Children.Add(TimerIcon, 2, Row+1);
            ArticleGrid.Children.Add(FavButton, 5, 6, Row + 1, Row + 2);
            ArticleGrid.Children.Add(FavIcon, 5, Row + 1);
            ArticleGrid.Children.Add(Comment, 0, 6, Row + 2, Row + 3);
            ArticleGrid.Children.Add(CommentButton, 0, 6, Row + 2, Row + 3);
            ArticleGrid.Children.Add(CommentGrid, 0, 6, Row + 3, Row + 4);
            
            if (App.Online)
            {
                LoadComments();
            }

        }
        async void FavButtonClicked(object sender, System.EventArgs e)
        {
            if (App.LoggedinUser != null)
            {
                var fav = new FavoritesTable();
                fav.User = App.LoggedinUser.ID;
                fav.Article = ArticleNR;
                fav.Image = ArticleImage.Source.ToString();
                fav.Header = Rubrik.Text;
                App.database.InsertFavorite(fav);
                await DisplayAlert("Favorite", "Article Added to Favorites", "Ok");
            }
            else
            {
                await DisplayAlert("Favorite", "Please Log in in order to Favorite Articles", "Ok");
            }
            
        }
            async void TimerButtonClicked(object sender, System.EventArgs e)
        {
            //IconRotation();
            Button button = (Button)sender;
            await button.RotateTo(-2, 1, Easing.BounceOut);
            await button.RotateTo(2, 1, Easing.BounceOut);
            await button.RotateTo(0, 1, Easing.BounceOut);

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
                            A.Image.Margin = 6;
                        });

                    }
                }
                App.database.UpdateStats("ArticlesRead");
                App.database.MissionUpdate(App.LoggedinUser, "ArticleRead");
                App.database.Plustoken(App.LoggedinUser, 1);
                Read = true;


                var variable = (ProfilePage)App.Mainpage.Children[2];
                variable.TokenNumber.Text = App.LoggedinUser.Plustokens.ToString();
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
        async void IconRotation()
        {
            Image image = TimerIcon;
            await image.RotateTo(-2, 40, Easing.BounceOut);
            await image.RotateTo(2, 60, Easing.BounceOut);
            await image.RotateTo(0, 40, Easing.BounceOut);
        }
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
                //TimerDone(Brödtext);

            }
            else
            {
                Timer.Start();
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
                    SC.UserSubmitted = 0;
                    SC.User = App.LoggedinUser.ID;
                    SC.Replynr = -1;
                    SC.Replylvl = 0;
                    SC.Comment = Comment.Text;
                    SC.Point = 0;
                    Comment.Text = "";
                    App.database.InsertComment(SC);
                    MakeComment(SC);
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
            
            CommentGrid.Children.Clear();
            foreach (var s in Query)
            {
                MakeComment(s);
            }
        }
        public void MakeComment(CommentTable s)
        {
            var User = App.database.GetUser(s.User).First();



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
                //Debug.WriteLine("Action: " + action);

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

             

            CommentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            CommentGrid.Children.Add(Box, 0, 6, s.CommentNR, s.CommentNR + 1);
            CommentGrid.Children.Add(Comment, 1, 5, s.CommentNR, s.CommentNR + 1);
            CommentGrid.Children.Add(Username, 1, 5, s.CommentNR, s.CommentNR + 1);                  
            CommentGrid.Children.Add(Reply, 4, 5, s.CommentNR, s.CommentNR + 1);
            CommentGrid.Children.Add(Elispses, 5, 6, s.CommentNR, s.CommentNR + 1);

        }
        
        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }

    }
}
