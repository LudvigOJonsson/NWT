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

        public int red = 255;
        public int green = 255;
        public int blue = 255;
        public System.Timers.Timer Timer;
        public static int ArticleNR;
        public int CC = 8;
        public bool Read = false;
        public int Row = 8;
        public bool Topimg = true;
        public bool Favorited = false;
        public ListView CommentListView;
        public List<CommentTable> CommentTableList = new List<CommentTable>();
        public List<Comment> CommentList = new List<Comment>();
        public int LWH = 500;
        public class Comment
        {
            public UserTable User { get; set; }
            public string UserName { get; set; }
            public List<string> UserAvatar { get; set; }
            public string CommentText { get; set; }
            public int CommentNR { get; set; }           
            public CommentTable CB { get; set; }

            public Comment(CommentTable s, UserTable User)
            {
                CB = s;
                
                UserName = User.Name;
                UserAvatar = JsonConvert.DeserializeObject<List<string>>(User.Avatar);
                CommentText = s.Comment;
                CommentNR = s.CommentNR;
            }
        }



        public NewsPage(RSSTable RSS, int argc)
        {
            InitializeComponent();


            
            //Category.TextColor = App.MC;
            FavIcon.BackgroundColor = App.MC;
            if (App.LoggedinUser != null && argc == 0)
            {
                bool read = false;
                var History = App.database.GetAllHistory(App.LoggedinUser.ID);

                if(History == null)
                {
                    read = true;
                }
                else
                {
                    foreach (HistoryTable HT in History)
                    {
                        if (RSS.ID == HT.Article)
                        {
                            read = true;
                            break;
                        }
                    }
                }
                          
                if (read)
                {                   
                    TimerButton.IsEnabled = false;
                    TimerIcon.Source = "tokenicon.png";
                    TimerButton.Text = "Samlad";
                    Read = true;
                    NewsPageView.BackgroundColor = App.MC;
                    TimerButton.BackgroundColor = App.MC;
                                     
                }
                else
                {
                    NewsPageView.BackgroundColor = Color.White;
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

        void TagSelected(object sender, EventArgs e)
        {
            CommentEntry.IsVisible = false;
            CommentButton.IsVisible = false;
            CommentListView.IsVisible = false;
            TagGrid.IsVisible = true;
        }
        void CommentSelected(object sender, EventArgs e)
        {
            CommentEntry.IsVisible = true;
            CommentButton.IsVisible = true;
            CommentListView.IsVisible = true;
            TagGrid.IsVisible = false;
        }
        void LoadNews(RSSTable RSS)
        {
            ArticleNR = RSS.ID;
            Rubrik.Text = RSS.Title;
            //Dot.Text = "⚫";
            Ingress.Text = RSS.Description;
            Top.Text = "Publicerad: " + RSS.PubDate + "   "+RSS.Source;
            if (RSS.Author == "Ingen Byline")
            {
                Author.Text = "";
            } else
            {
                Author.TextColor = Color.FromHex("#649FD4");
                Author.Text = RSS.Author;
            }
            Title = RSS.Category;
            
            //Category.Text = "-"+RSS.Category;

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

                //Menu.Author = RSS.Author;
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
                            FontSize = 16,
                            TextColor = Color.Black,                        
                            Margin = new Thickness(0, 0, 5, 10)
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
                            FontSize = 18,
                            TextColor = Color.Gray,
                            Margin = new Thickness(0, 10, 0, 20)
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


            ArticleGrid.Children.Add(Author, 1, 4, Row, Row + 1);
            ArticleGrid.Children.Add(BG, 0, 6, 0, Row + 1);
            ArticleGrid.Children.Add(BackGround, 0, 6, Row + 1, Row + 2);
            ArticleGrid.Children.Add(TimerButton, 1, 4, Row + 1, Row + 2);
            ArticleGrid.Children.Add(TimerIcon, 2, Row + 1);
            //ArticleGrid.Children.Add(tokenAnimation, 2, Row + 1);
            //ArticleGrid.Children.Add(FavButton, 5, 6, Row + 1, Row + 2);
            ArticleGrid.Children.Add(FavIcon, 4, Row + 1);

            ArticleGrid.Children.Add(TagSelectButton, 0, 3, Row + 2, Row + 3);
            ArticleGrid.Children.Add(CommentSelectButton, 3, 6, Row + 2, Row + 3);

            
            ArticleGrid.Children.Add(CommentEntry, 0, 6, Row + 3, Row + 4);
            ArticleGrid.Children.Add(CommentButton, 0, 6, Row + 3, Row + 4);
            






            if (App.Online)
            {
                LoadComments();
            }
            ArticleGrid.Children.Add(TagGrid, 0, 6, Row + 3, Row + 4);
            ArticleGrid.Children.Add(CommentListView, 0, 6, Row + 3, Row + 4);

            CommentEntry.IsVisible = false;
            CommentButton.IsVisible = false;
            CommentListView.IsVisible = false;
            TagGrid.IsVisible = true;


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

                var variable = (ProfilePage)App.Mainpage.Children[2];
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
            if (red > (App.MC.R*255))
            {
                red--;
            }
            if (green > (App.MC.G * 255))
            {
                green--;
            }
            if (blue > (App.MC.B * 255))
            {
                blue--;
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                //NewsPageView.BackgroundColor = Color.FromRgb(red, green, blue);
                //Dot.TextColor = Color.FromRgb(red, green, blue);
                TimerButton.BackgroundColor = Color.FromRgb(red, green, blue);
            });

            if (App.MC == Color.FromRgb(red, green, blue))
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
                var CNR = App.database.CommentCount(ArticleNR);

                if (App.database.TokenCheck() && CommentEntry.Text != null && CommentEntry.Text != "" && CommentEntry.Text.Length > 0 && CNR != -1)
                {
                    
                    var SC = new CommentTable
                    {
                        Article = ArticleNR,
                        CommentNR = CNR,
                        UserSubmitted = 0,
                        User = App.LoggedinUser.ID,
                        Replynr = -1,
                        Replylvl = 0,
                        Comment = CommentEntry.Text,
                        Point = 0
                    };
                    CommentEntry.Text = "";
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
        async void LoadComments()
        {
            CommentTableList = App.database.GetComments(ArticleNR,0,-1);
            if (CommentTableList != null)
            {
                foreach (var CommentTable in CommentTableList)
                {
                    MakeComment(CommentTable);
                }
                if (7 < CommentList.Count)
                {
                    LWH = 500;
                }
                else
                {
                    LWH = 70 * CommentList.Count;
                }

                CreateCommentListView();
            }
            else
            {
                await DisplayAlert("Offline", "Comments could not be loaded. Please try again later.", "OK");
            }
            
            
        }
        public void MakeComment(CommentTable s)
        {
            var User = App.database.GetUser(s.User);
            if(User != null)
            {
                var C = new Comment(s, User.First());

                CommentList.Add(C);
            }
                      
        }

        public void CreateCommentListView()
        {
            CommentListView = new ListView
            {
                // Source of data items.

                ItemsSource = CommentList,
                HasUnevenRows = true,
                SeparatorVisibility = SeparatorVisibility.None,
                HeightRequest = LWH,

                
                





                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    



                    var Box = new BoxView
                    {
                        CornerRadius = 10,
                        Margin = 5,
                        HeightRequest = 50,
                        BackgroundColor = Color.White,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                    };
                    var Comment = new Label
                    {
                        //Text = s.Comment,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.Start,
                        TextColor = Color.Black,
                        FontSize = 16,
                        
                        Margin = 20,
                    };
                    var Username = new Label
                    {
                        //Text = "  " + User.Name,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Start,
                        TextColor = Color.Black,
                        FontSize = 16,

                    };

                    var AvatarFace = new Image
                    {
                        WidthRequest = 50,
                        HeightRequest = 50,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        BackgroundColor = App.MC,
                        Margin = 5

                    };
                    var AvatarHair = new Image
                    {
                        WidthRequest = 50,
                        HeightRequest = 50,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,

                        Margin = 5

                    };

                    var AvatarBody = new Image
                    {
                        WidthRequest = 50,
                        HeightRequest = 50,
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,

                        Margin = 5

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







                    var CommentGrid = new Grid
                    {

                        RowDefinitions = {
                    new RowDefinition { Height = GridLength.Auto }
                    },

                        ColumnDefinitions = {
                    new ColumnDefinition { Width = 10 },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = 0 },
                    },
                        RowSpacing = 0,
                        ColumnSpacing = 0,
                        
                        IsClippedToBounds = true


                    };


                    Comment.SetBinding(Label.TextProperty, "CommentText");
                    Username.SetBinding(Label.TextProperty, "UserName");
                    AvatarFace.SetBinding(Image.SourceProperty, "UserAvatar[0]");                    
                    AvatarHair.SetBinding(Image.SourceProperty, "UserAvatar[1]");
                    AvatarBody.SetBinding(Image.SourceProperty, "UserAvatar[2]");

                    CommentGrid.Children.Add(Box, 0, 8, 0, 1);

                    CommentGrid.Children.Add(AvatarFace, 1, 2, 0, 1);
                    CommentGrid.Children.Add(AvatarHair, 1, 2, 0, 1);
                    CommentGrid.Children.Add(AvatarBody, 1, 2, 0, 1);
                    CommentGrid.Children.Add(Comment, 2, 6, 0, 1);
                    CommentGrid.Children.Add(Username, 2, 6, 0, 1);
                    CommentGrid.Children.Add(Elispses, 6, 7, 0, 1);


                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = CommentGrid
                    };
                })

            };



        }


        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }

    }
}
