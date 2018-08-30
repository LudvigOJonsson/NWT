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
    public partial class UserNewsGridPage : ContentPage
    {
        public static int Startnr = 1;
        public static int Stopnr = 1;
        public static int DBLN = 10;
        public static int NTN = DBLN / 2;
        public static int Rownr = 1;
        public static TapGestureRecognizer TGR = new TapGestureRecognizer();
        public List<Article> ArticleList = new List<Article>();
        public static List<string> imageLinks = new List<string>();
        public static Random rnd = new Random();
        public static Button LoadNewsButton = new Button() { Text = "Load" };
        public bool NWT = true;
        public bool Mariestad = true;
        public bool HJO = true;
        public bool SLA = true;

        public class Article
        {
            public int ID = 0;
            public int Referat = -1;
            public bool Plus = false;
            public Button Box = new Button { };
            public BoxView Frame = new BoxView { };
            public Label Label = new Label { };
            public Image Image = new Image { };

            public Article(UserRSSTable RSS)
            {
                
                ID = RSS.ID;
                Referat = RSS.Referat;
                Box = new Button
                {
                    BackgroundColor = Color.FromHex("#FFFFFF"),
                    WidthRequest = 200,
                    HeightRequest = 420,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.End,
                    ClassId = RSS.ID.ToString(),
                    Margin = 5,
                    CornerRadius = 5,
                };

                Frame = new BoxView
                {
                    Color = Color.FromRgb(150, 150, 150),
                    WidthRequest = 200,
                    HeightRequest = 420,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Center,
                    ClassId = RSS.ID.ToString()
                };

                Label = new Label
                {
                    Text = RSS.Rubrik,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Start,
                    FontSize = 36,
                    FontAttributes = FontAttributes.Bold,
                    HeightRequest = 50,
                    TextColor = Color.Black,
                    ClassId = RSS.ID.ToString(),
                    Margin = 8
                };

                Label.GestureRecognizers.Add(TGR);

                Image = new Image
                {

                    Source = imageLinks[rnd.Next(7)],
                    WidthRequest = 200,
                    HeightRequest = 300,
                    Aspect = Aspect.AspectFill,
                    Margin = 5,

                };
            }

            public void Visibility(bool State)
            {

                Image.IsVisible = State;
                Label.IsVisible = State;
                Box.IsVisible = State;
                Frame.IsVisible = State;
            }




        }


        public UserNewsGridPage()
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

            TGR.NumberOfTapsRequired = 1;

            TGR.Tapped += (s, e) => {
                LoadNews(s, e);

            };

            LoadNewsButton.Clicked += (s, e) => {
                AddNews();
            };

            AddNews();
            //NewsButtonN.Image = ImageSource.FromFile("newsfeed.png");
        }

        async void LoadNews(object sender, EventArgs e)
        {
            var Header = ((Label)sender);

            var id = Int32.Parse(Header.ClassId);
            var RSS = App.database.GetUserRss(id).First();
            
            await Navigation.PushAsync(new UserNewsPage(RSS));
        }     

        public void AddNews()
        {

            if (Startnr < (Stopnr + NTN))
            {
                FillLocalDB();
            }
            Stopnr += NTN;
            var Rss = App.database.GetUserRSS(Stopnr);
            Console.WriteLine(Rss.Count);

            foreach (UserRSSTable RSS in Rss)
            {
                bool Exists = false;

                foreach (var Article in ArticleList)
                {
                    if (Article.ID == RSS.ID)
                    {
                        Exists = true;
                    }
                }

                if (!Exists)
                {
                    var Box = new Article(RSS);
                    ArticleList.Add(Box);
                    UserNewsGrid.RowDefinitions.Add(new RowDefinition { Height = 300 });
                    UserNewsGrid.RowDefinitions.Add(new RowDefinition { Height = 100 });
                    UserNewsGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
                    UserNewsGrid.RowSpacing = 0;

                    //column (left) = 0, right = column + column span; 0 + 5 = 6.  row (top) = 1, bottom = row + row span; 1 + 1 = 2
                    UserNewsGrid.Children.Add(Box.Frame, 0, 1, Rownr - 1, Rownr + 2); //Boxview
                    UserNewsGrid.Children.Add(Box.Box, 0, 1, Rownr - 1, Rownr + 2); //Boxview
                    UserNewsGrid.Children.Add(Box.Image, 0, 1, Rownr, Rownr + 1); //Image
                    UserNewsGrid.Children.Add(Box.Label, 0, 1, Rownr + 1, Rownr + 2); //Label
                    Rownr++;
                    Rownr++;
                    Rownr++;

                }
            }
            UserNewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            UserNewsGrid.Children.Add(LoadNewsButton, 0, Rownr - 1);
        }

        public void FillLocalDB()
        {
            var X = App.database.LoadUserRSS(Startnr, (Startnr + DBLN));
            
            Startnr += DBLN;
        }


    }
}