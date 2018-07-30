using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

// await DisplayAlert("Task", "PING","OK");

namespace NWT
{
	public partial class MainPage : CarouselPage
	{
        public static int Startnr = 1;
        public static int Stopnr = 7;
        public static List<KeyValuePair<BoxView, KeyValuePair<Label, Image>>> ArticleList = new List<KeyValuePair<BoxView, KeyValuePair<Label, Image>>>();
        public List<string> imageLinks = new List<string>();
        Random rnd = new Random();


        public MainPage()
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
            //AddNews();          
            //NewsButtonN.Image = ImageSource.FromFile("newsfeed.png");
        }

        



        async void LoadNews(object sender, EventArgs e)
        {
            var Header = ((Label)sender);
            var id = Int32.Parse(Header.ClassId);
            await Navigation.PushAsync(new NewsPage(id));
        }

        public void PrintNews()
        {
            int X = 1;
            bool NWT = true;
            bool Mariestad = true;
            bool HJO = true;
            bool SLA = true;


            if (App.Instanciated)
            {
                NWT = App.SideMenu.NWT.IsToggled;
                Mariestad = App.SideMenu.Mariestad.IsToggled;
                HJO = App.SideMenu.HJO.IsToggled;
                SLA = App.SideMenu.SLA.IsToggled;

            }



            var TGR = new TapGestureRecognizer();

            TGR.Tapped += (s, e) => {
                LoadNews(s, e);
            };
            TGR.NumberOfTapsRequired = 1;

            int ColourNR = 1;

            Color Colour = Color.Black;

            //var Rss = App.database.GetRSS(Stopnr);
            //Console.WriteLine(RSS.Count);
            ArticleList.Clear();
            /*
            foreach (RSSTable RSS in Rss)
            {
                switch (ColourNR)
                {
                    case 1:
                        Colour = Color.Red;
                        ColourNR++;
                        break;

                    case 2:
                        Colour = Color.Orange;
                        ColourNR++;
                        break;

                    case 3:
                        Colour = Color.Yellow;
                        ColourNR++;
                        break;

                    case 4:
                        Colour = Color.Green;
                        ColourNR++;
                        break;

                    case 5:
                        Colour = Color.Blue;
                        ColourNR++;
                        break;

                    case 6:
                        Colour = Color.Purple;
                        ColourNR = 1;
                        break;

                }



                if ((RSS.Source == "NWT" && NWT) ||
                   (RSS.Source == "Mariestad" && Mariestad) ||
                   (RSS.Source == "Hjo" && HJO) ||
                   (RSS.Source == "SLA" && SLA))
                {

                    NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                    var boxview = new BoxView { };
                    boxview = new BoxView
                    {
                        Color = Color.FromHex("#FFFFFF"),
                        WidthRequest = 200,
                        HeightRequest = 80,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.End,
                        ClassId = RSS.Source
                    };

                    var label = new Label { };
                    label = new Label
                    {
                        Text = RSS.Title,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.End,
                        HeightRequest = 80,
                        TextColor = Colour,
                        FontSize = 25,
                        ClassId = RSS.ID.ToString(),
                        Margin = 10
                    };

                    var image = new Image { };

                    image = new Image
                    {
                        Source = imageLinks[rnd.Next(7)],
                        WidthRequest = 200,
                        HeightRequest = 300,
                        Aspect = Aspect.AspectFill
                    };

                    label.GestureRecognizers.Add(TGR);
                    var SubArticle = new KeyValuePair<Label, Image>(label, image);
                    var Article = new KeyValuePair<BoxView, KeyValuePair<Label, Image>>(boxview, SubArticle);
                    ArticleList.Add(Article);

                }
            }
            NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });


            NewsGrid.Children.Clear();

            var LoadNewsButton = new Button()
            {
                Text = "Load",
            };

            LoadNewsButton.Clicked += (s, e) => {
                AddNews();
            };

            foreach (var s in ArticleList)
            {               
                    NewsGrid.RowSpacing = 0;
                    NewsGrid.Children.Add(s.Value.Value, 0, X - 1); //Image
                    
                    NewsGrid.Children.Add(s.Key, 0, X - 1); //Boxview
                    NewsGrid.Children.Add(s.Value.Key, 0, X - 1); //Label            
                X++;
            }
            NewsGrid.Children.Add(LoadNewsButton,0, X - 1);
            */
        }

        public void AddNews()
        {
            App.database.LoadRSS(Startnr, Stopnr);
            PrintNews();
            Startnr = Stopnr;
            Stopnr += 6;
        }

        async void PlaySudoku(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SudokuPage());
        }

        void Community()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[0];
        }
        void News()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[1];
        }
        void Profile()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[2];
        }
        void Games()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[3];
        }


    }
}
