using System;
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
        
        public int red = (int)App.MC.R;
        public int green = (int)App.MC.G;
        public int blue = (int)App.MC.B;
        public System.Timers.Timer Timer;
        public static int ArticleNR;
        public int CC = 8;
        public bool Read = false;
        public int Row = 7;
        public bool Topimg = true;
        public bool Favorited = false;

        public NewsPage(RSSTable RSS, int argc)
        {
            InitializeComponent();

            red = (int)App.MC.R  + (255 -(int)App.MC.R -1);
            green = (int)App.MC.G + (255 - (int)App.MC.G - 1);
            blue = (int)App.MC.B + (255 - (int)App.MC.B - 1);

            Category.TextColor = App.MC;
            FavIcon.BackgroundColor = App.MC;
            if (App.LoggedinUser != null && argc == 0)
            {
                bool read = false;
                var History = App.database.GetAllHistory(App.LoggedinUser.ID);
                foreach (HistoryTable HT in History)
                {
                    if (RSS.ID == HT.Article)
                    {
                        read = true;
                        break;
                    }                 
                }            
                if (read)
                {                   
                    TimerButton.IsEnabled = false;
                    TimerIcon.Source = "tokenicon.png";
                    TimerButton.Text = "Samlad";
                    Read = true;
                    NewsPageView.BackgroundColor = App.MC;
                    //Dot.TextColor = Color.FromRgb(47, 110, 131); 
                    TimerButton.BackgroundColor = App.MC;
                                     
                }
                else
                {
                    NewsPageView.BackgroundColor = Color.FromRgb(red, green, blue);
                    Timer = new System.Timers.Timer
                    {
                        Interval = 60
                    };
                    Timer.Elapsed += OnTimedEvent;
                    Timer.Enabled = true;
                }
            }
            else
            {
                NewsPageView.BackgroundColor = Color.FromRgb(248, 248, 248);
            }

            if(argc == 1)
            {
                TimerButton.IsVisible = false;
                TimerIcon.IsVisible = false;
                //FavButton.IsVisible = false;
                FavIcon.IsVisible = false;
            }
            
            if (argc == 2)
            {
                TimerButton.IsVisible = false;
                TimerIcon.IsVisible = false;
                Favorited = true;
            }

            LoadNews(RSS);
            
            
        }

 

        void LoadNews(RSSTable RSS)
        {
            ArticleNR = RSS.ID;
            Rubrik.Text = RSS.Title;
            //Dot.Text = "⚫";
            Ingress.Text = RSS.Description;
            Top.Text = "Publicerad: " + RSS.PubDate + "   "+RSS.Source;
            Author.Text = RSS.Author;
            Category.Text = "-"+RSS.Category;
            if (RSS.Tag.Length > 20)
            {
                //Tags.Text = "";
            }
            else if(RSS.Tag != "")
            {
                //Tags.Text = "Tags: " + RSS.Tag + "  ";
            } else
            {
                //Tags.Text = "";
            }

            var TGR = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };
            TGR.Tapped += (s, e) => {
                IsEnabled = false;
                App.SideMenu.Author = RSS.Author;
                IsEnabled = true;
            };
            Author.GestureRecognizers.Add(TGR);

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
                            Text = Text[TextCount],
                            HorizontalTextAlignment = TextAlignment.Start,
                            VerticalTextAlignment = TextAlignment.Start,
                            FontSize = 14,
                            TextColor = Color.Black,                        
                            Margin = new Thickness(0, 0, 10, 10)
                        };
                        ArticleGrid.Children.Add(Label, 1, 5, Row, Row + 1);
                        Row++;
                        Count++;
                        TextCount++;
                }
                else if (Type == 1)
                {
                        var IMGText = new Label
                        {
                            Text = ImageText[ImageCount],
                            HorizontalOptions = LayoutOptions.Start ,
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalTextAlignment = TextAlignment.Start,
                            VerticalTextAlignment = TextAlignment.Start,
                            FontSize = 12,
                            TextColor = Color.Gray,
                            Margin = new Thickness(0, 0, 10, 10)
                        };
                        
                        if (Topimg == true)
                        {
                            Image img = new Image { Source = Images[ImageCount] };
                            

                            ArticleImage.Source = Images[ImageCount];
                            ArticleImage.HeightRequest = img.Height;
                            ArticleImage.WidthRequest = 300;
                            ArticleGrid.Children.Add(IMGText, 1, 5, 5, 6);
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
                            ArticleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            Image.Source = Images[ImageCount];
                            ArticleGrid.Children.Add(Image, 0, 6, Row, Row + 1);
                            ArticleGrid.Children.Add(IMGText, 1, 5, Row+1, Row + 2);
                            Row++;
                            Row++;
                            Row++;
                    }
                        Count++;
                        ImageCount++;
                }
            }
            string[] Categories = RSS.Category.Split(new[] { ", " }, StringSplitOptions.None);
            string[] Tags = RSS.Tag.Split(new[] { ", " }, StringSplitOptions.None);

            int TagRow = 0;

            foreach (String Category in Categories)
            {
                var Box = new Button
                {
                    CornerRadius = 10,
                    Margin = 2,
                    BackgroundColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    ClassId = Category,
                    
                };
                Box.Clicked += CategoryButtonClicked;

                var Comment = new Label
                {
                    Text = Category,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    TextColor = Color.Black,
                    FontSize = 16,
                    WidthRequest = 290,
                    Margin = 20,
                    InputTransparent = true,
                };
                TagGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                TagGrid.Children.Add(Box, 0, 6, TagRow, TagRow + 1);
                TagGrid.Children.Add(Comment, 0, 6, TagRow, TagRow + 1);
                TagRow++;

                if (App.SideMenu.Categories.Contains(Category))
                {
                    Box.IsEnabled = false;
                }


            }



            foreach (String Tag in Tags)
            {
                var Box = new Button
                {
                    CornerRadius = 10,
                    Margin = 2,
                    BackgroundColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    ClassId = Tag
                };
                Box.Clicked += TagButtonClicked;
                var Comment = new Label
                {
                    Text = Tag,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    TextColor = Color.Black,
                    FontSize = 16,
                    WidthRequest = 290,
                    Margin = 20,
                    InputTransparent = true,
                };
                TagGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                TagGrid.Children.Add(Box, 0, 6, TagRow, TagRow + 1);
                TagGrid.Children.Add(Comment, 0, 6, TagRow, TagRow + 1);
                TagRow++;


                if (App.SideMenu.Tags.Contains(Tag))
                {
                    Box.IsEnabled = false;
                }
            }



            ArticleGrid.Children.Add(BG, 0, 6, 0, Row);
            ArticleGrid.Children.Add(BackGround, 0, 6, Row + 1, Row + 3);
            ArticleGrid.Children.Add(TimerButton, 1, 4, Row + 1, Row + 2);
            ArticleGrid.Children.Add(TimerIcon, 2, Row + 1);
            //ArticleGrid.Children.Add(tokenAnimation, 2, Row + 1);
            //ArticleGrid.Children.Add(FavButton, 5, 6, Row + 1, Row + 2);
            ArticleGrid.Children.Add(FavIcon, 4, Row + 1);
            ArticleGrid.Children.Add(TagGrid, 0, 6, Row + 2, Row + 3);
            ArticleGrid.Children.Add(Comment, 0, 6, Row + 3, Row + 4);
            ArticleGrid.Children.Add(CommentButton, 0, 6, Row + 3, Row + 4);
            ArticleGrid.Children.Add(CommentGrid, 0, 6, Row + 4, Row + 5);






            if (App.Online)
            {
                LoadComments();
            }

        }


        void CategoryButtonClicked(object sender, System.EventArgs e)
        {

            var Button = (Button)sender;
            App.SideMenu.Categories.Add(Button.ClassId);
            App.SideMenu.UpdateTags();
            Button.IsEnabled = false;
        }
        void TagButtonClicked(object sender, System.EventArgs e) {

            var Button = (Button)sender;
            App.SideMenu.Tags.Add(Button.ClassId);
            App.SideMenu.UpdateTags();
            Button.IsEnabled = false;
        }

        async void FavButtonClicked(object sender, System.EventArgs e)
        {
            
            if (App.LoggedinUser != null)
            {
                
                if (Favorited)
                {
                    App.database.Execute("DELETE FROM Favorites WHERE User = "+ App.LoggedinUser.ID +" AND Article = "+ ArticleNR);
                    await DisplayAlert("Favorite", "Article Unfavorited", "Ok");
                    Favorited = false;
                }
                else
                {
                    var fav = new FavoritesTable
                    {
                        User = App.LoggedinUser.ID,
                        Article = ArticleNR,
                        Image = ArticleImage.Source.ToString(),
                        Header = Rubrik.Text
                    };
                    App.database.InsertFavorite(fav);
                    await DisplayAlert("Favorite", "Article Added to Favorites", "Ok");
                    Favorited = true;
                }
                
            }
            else
            {
                await DisplayAlert("Favorite", "Please Log in in order to Favorite Articles", "Ok");
            }
            
        }
        async void TimerButtonClicked(object sender, System.EventArgs e)
        {
            //IconRotation();
            TimerButton.IsEnabled = false;
            Button button = (Button)sender;
            await button.RotateTo(0, 1, Easing.BounceOut);
            await button.RotateTo(2, 1, Easing.BounceOut);
            await button.RotateTo(0, 1, Easing.BounceOut);

            if (TimerButton.BackgroundColor == App.MC && Read == false)
            {
                var HT = new HistoryTable
                {
                    User = App.LoggedinUser.ID,
                    Article = ArticleNR,
                    Readat = DateTime.Now,
                    Header = Rubrik.Text,
                    Image = ArticleImage.Source.ToString()
                };
                App.database.InsertHistory(HT);
                TimerIcon.Source = "tokenicon.png";
                TimerButton.Text = "Samlad";
                var NG = (NewsGridPage)App.Mainpage.Children[1];
                foreach (NewsGridPage.Article A in NG.ArticleList)
                {
                    if (A.ID == ArticleNR)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {

                            //A.CheckImage.Source = "checkmark.png";

                            //A.CornerImage.Source = "plusBackground.png";

                            //A.Box.Color = Color.FromRgb(80, 210, 194);
                            //A.Image.Margin = 6;
                        });

                    }
                }
                App.database.Plustoken(App.LoggedinUser, 1);
                Read = true;
                TimerButton.IsEnabled = false;

                var variable = (ProfilePage)App.Mainpage.Children[3];
                variable.TokenNumber.Text = App.LoggedinUser.Plustokens.ToString();
            }
            else
            {
                TimerButton.IsEnabled = true;
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
        /*
        async void IconRotation()
        {
            Image image = TimerIcon;
            await image.RotateTo(-2, 40, Easing.BounceOut);
            await image.RotateTo(2, 60, Easing.BounceOut);
            await image.RotateTo(0, 40, Easing.BounceOut);
        }
        */
        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (red != App.MC.R)
            {
                red--;
            }
            if (green != App.MC.G)
            {
                green--;
            }
            if (blue != App.MC.B)
            {
                blue--;
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                NewsPageView.BackgroundColor = Color.FromRgb(red, green, blue);
                //Dot.TextColor = Color.FromRgb(red, green, blue);
                TimerButton.BackgroundColor = Color.FromRgb(red, green, blue);
            });

            if (green == App.MC.G && blue == App.MC.B && red == App.MC.R)
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
            CommentButton.IsEnabled = false;
            if (App.Online)
            {
                if (App.database.TokenCheck() && Comment.Text != null && Comment.Text != "" && Comment.Text.Length < 0)
                {
                    var CNR = App.database.CommentCount(ArticleNR);
                    var SC = new CommentTable
                    {
                        Article = ArticleNR,
                        CommentNR = CNR,
                        UserSubmitted = 0,
                        User = App.LoggedinUser.ID,
                        Replynr = -1,
                        Replylvl = 0,
                        Comment = Comment.Text,
                        Point = 0
                    };
                    Comment.Text = "";
                    App.database.InsertComment(SC);
                    MakeComment(SC);
                }
            }
            else
            {
                await DisplayAlert("Offline", "The Server is currently Offline. Please try again later.", "OK");
            }
            CommentButton.IsEnabled = true;
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
            var Elispses = new Button()
            {
                BackgroundColor = Color.Transparent,
                WidthRequest = 60,
                HeightRequest = 30,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                Margin = 20,
                ImageSource = "elipses.png",
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
            //CommentGrid.Children.Add(Reply, 4, 5, s.CommentNR, s.CommentNR + 1);
            CommentGrid.Children.Add(Elispses, 5, 6, s.CommentNR, s.CommentNR + 1);

        }
        
        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }

    }
}
