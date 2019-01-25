using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NWT
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsGridPage : ContentPage
    {
        public int Startnr = 1;
        public int Stopnr = 1;
        public static int DBLN = 10;
        public static int NTN = DBLN;
        public int Rownr = 1;
        public static TapGestureRecognizer TGR;
        public List<Article> ArticleList = new List<Article>();
        public static string Defaultimage = "http://media2.hitzfm.nu/2016/11/Nyheter_3472x1074.jpg";
        public static Random rnd = new Random();
        public Button LoadNewsButton = new Button() { Text = "Load" };
        public bool NWT = true;
        public bool Mariestad = true;
        public bool HJO = true;
        public bool SLA = true;
        public bool First = true;

        public class Article
        {
            public int ID = 0;
            public string Source = "";
            public bool Plus = false;
            public bool Full = false;
            public Button Box = new Button { };
            public BoxView Frame = new BoxView { };
            public BoxView ArticleMargin = new BoxView { };
            public Label Label = new Label { };
            public Image Image = new Image { };
            public Image PlusImage = new Image { };
            public Image CornerImage = new Image { };

            public Article(RSSTable RSS)
            {
                Console.WriteLine(RSS.ID +" som har NS: " +RSS.NewsScore );

                Source = RSS.Source;
                ID = RSS.ID;
                Plus = Convert.ToBoolean(RSS.Plus);
                
                int IMGXC = 200;
                int IMGYC = 300;

                if (RSS.NewsScore > 3 && RSS.ImgSource != "http://media2.hitzfm.nu/2016/11/Nyheter_3472x1074.jpg")
                {
                    Console.WriteLine("Full Artikel");
                    Full = true;
                }
                else
                {
                    Console.WriteLine("Halv Artikel");
                }
                


                if (Full) {

                    //RSS.Title = RSS.Title.Replace("\"", "'");
                    



                    Label = new Label
                    {
                        Text = RSS.Title,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Start,
                        FontSize = 25,
                        FontAttributes = FontAttributes.Bold,
                        WidthRequest = IMGXC,
                        HeightRequest = ((RSS.Title.Length/30))*50,
                        TextColor = Color.Black,
                        ClassId = RSS.ID.ToString(),
                        Margin = 12,
                    };

                    Label.GestureRecognizers.Add(TGR);
                  
                    if (RSS.ImgSource == null) { RSS.ImgSource = Defaultimage; }
                    Image = new Image
                    {
                    
                        Source = RSS.ImgSource,
                        WidthRequest = IMGXC,
                        HeightRequest = IMGYC,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        Aspect = Aspect.AspectFill,
                        Margin = 0,
                        ClassId = RSS.ID.ToString()

                    };

                    Box = new Button
                    {
                        BackgroundColor = Color.Transparent,
                        WidthRequest = IMGXC,
                        HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.End,
                        ClassId = RSS.ID.ToString(),
                        Margin = 0,
                        CornerRadius = 5,
                        BorderColor = Color.Transparent,
                        BorderWidth = 2,
                    };

                    Frame = new BoxView
                    {
                        Color = Color.White,
                        WidthRequest = IMGXC,
                        HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        ClassId = RSS.ID.ToString()
                    };


                    Image.GestureRecognizers.Add(TGR);
                    //Image.IsVisible = false;
                    PlusImage = new Image
                    {
                        Source = "plus.png",
                        WidthRequest = 60,
                        HeightRequest = 60,
                        Margin = 0,
                        Aspect = Aspect.AspectFill,
                        ClassId = RSS.ID.ToString(),
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.End,

                    };

                    CornerImage = new Image
                    {

                        Source = "cornerTriangle.png",
                        WidthRequest = 120,
                        HeightRequest = 120,
                        Margin = 0,
                        Aspect = Aspect.AspectFill,
                        ClassId = RSS.ID.ToString(),
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.End,

                    };
                    ArticleMargin = new BoxView
                    {
                        Color = Color.LightGray,
                        WidthRequest = IMGXC,
                        HeightRequest = 0,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        ClassId = RSS.ID.ToString()
                    };
                }
                else
                {

                    Label = new Label
                    {
                        Text = RSS.Title,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 15,
                        FontAttributes = FontAttributes.Bold,
                        WidthRequest = IMGXC/2,
                        HeightRequest = ((RSS.Title.Length / 15)) * 10,
                        TextColor = Color.Black,
                        ClassId = RSS.ID.ToString(),
                        Margin = 12,
                    };

                    Label.GestureRecognizers.Add(TGR);
                    if (RSS.ImgSource == null) { RSS.ImgSource = Defaultimage; }
                    Image = new Image
                    {

                        Source = RSS.ImgSource,
                        WidthRequest = IMGXC/2,
                        HeightRequest = IMGYC/2,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        Aspect = Aspect.AspectFit,
                        Margin = 25,
                        ClassId = RSS.ID.ToString()

                    };

                    Box = new Button
                    {
                        BackgroundColor = Color.Transparent,
                        WidthRequest = IMGXC,
                        HeightRequest = ((RSS.Title.Length / 15)+1) * 50,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.End,
                        ClassId = RSS.ID.ToString(),
                        Margin = 0,
                        CornerRadius = 5,
                        BorderColor = Color.Transparent,
                        BorderWidth = 2,
                    };

                    Frame = new BoxView
                    {
                        Color = Color.White,
                        WidthRequest = IMGXC,
                        HeightRequest = ((RSS.Title.Length / 15)+1) * 50,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        ClassId = RSS.ID.ToString()
                    };


                    Image.GestureRecognizers.Add(TGR);
                    //Image.IsVisible = false;
                    PlusImage = new Image
                    {
                        Source = "plus.png",
                        WidthRequest = 15,
                        HeightRequest = 15,
                        Margin = 20,
                        Aspect = Aspect.AspectFill,
                        ClassId = RSS.ID.ToString(),
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.End,

                    };

                    CornerImage = new Image
                    {

                        Source = "cornerTriangle.png",
                        WidthRequest = 30,
                        HeightRequest = 30,
                        Margin = 20,
                        Aspect = Aspect.AspectFill,                        
                        ClassId = RSS.ID.ToString(),
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.End,

                    };

                    ArticleMargin = new BoxView
                    {
                        Color = Color.LightGray,
                        WidthRequest = IMGXC,
                        HeightRequest = 0,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        ClassId = RSS.ID.ToString()
                    };
                }
                Console.WriteLine("Artikel Klar");
            }

            public void Visibility(bool State)
            {

                Image.IsVisible = State;
                Label.IsVisible = State;
                Box.IsVisible = State;
                Frame.IsVisible = State;
            }




        }


        public NewsGridPage(int Argc)
        {
            InitializeComponent();




            TGR = new TapGestureRecognizer();
            TGR.NumberOfTapsRequired = 1;

            LoadNewsButton.Clicked += (s, e) => {
                if (Startnr < (Stopnr + NTN))
                {
                    LoadLocalDB();
                }
                Stopnr += NTN;
                AddNews(0);
            };

            LoadNewsButton.IsVisible = false;

            if (Argc == 0)
            {     
                Console.WriteLine("NewsGrid");
                TGR.Tapped += (s, e) => {
                    IsEnabled = false;
                    LoadNews(s, e);
                    IsEnabled = true;
                };
                LoadLocalDB();
                Stopnr += NTN;
                AddNews(0);
            }
            else if (Argc == 1)
            {
                
                Console.WriteLine("ReferatSida");
                TGR.Tapped += (s, e) => {
                    IsEnabled = false;
                    var Header = (View)s;
                    Console.WriteLine(Header.ClassId);
                    Console.WriteLine(App.Mainpage.Children[0].Navigation.NavigationStack[1].GetType());
                    App.Mainpage.Children[0].Navigation.NavigationStack[1].ClassId = Header.ClassId;
                   
                    Navigation.PopAsync();
                };
                AddNews(0);
            }
            else if (Argc == 2)
            {

                Console.WriteLine("HistorikSida");
                TGR.Tapped += (s, e) => {
                    IsEnabled = false;
                    LoadNews(s, e);
                    IsEnabled = true;
                };
                LoadHistory();
            }
            else if (Argc == 3)
            {

                Console.WriteLine("FavoritSida");
                TGR.Tapped += (s, e) => {
                    IsEnabled = false;
                    LoadNews(s, e);
                    IsEnabled = true;
                };
                LoadFavorites();
            }






            //NewsButtonN.Image = ImageSource.FromFile("newsfeed.png");
        }

        async void LoadNews(object sender, EventArgs e)
        {

            var Header = (View)sender;

            var id = Int32.Parse(Header.ClassId);
            var RSS = App.database.GetRss(id).First();
            if (RSS.Plus == 1)
            {
                if (App.LoggedinUser != null)
                {
                    if (App.database.CheckPlus(RSS.ID))
                    {
                        await Navigation.PushAsync(new NewsPage(RSS));
                    }
                    else
                    {
                        App.database.UpdateStats("PlusArticlesClicked");
                        var answer = await DisplayAlert("Plus", "This is a Plus Article. You have to spend 3 Plustoken to gain access to it. Spend a token? (You have " + App.LoggedinUser.Plustokens + " Tokens left.)", "Yes", "No");
                        if (answer)
                        {
                            if (App.database.Plustoken(App.LoggedinUser, -3))
                            {
                                var Plus = new PlusRSSTable();
                                Plus.Article = RSS.ID;
                                Plus.User = App.LoggedinUser.ID;
                                App.database.InsertPlus(Plus);
                                App.database.UpdateStats("PlusArticlesUnlocked");
                                await Navigation.PushAsync(new NewsPage(RSS));
                            }
                            else
                            {
                                await DisplayAlert("No Tokens", "Insufficent Tokens", "OK");
                            }
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Plus", "This is a Plus article that can be unlocked with Plustokens, please register to learn more about Plustokens", "OK");
                }
            }
            else
            {
                await Navigation.PushAsync(new NewsPage(RSS));
            }


        }

        public void LoadHistory()
        {
            DBLN = 2;
            AddNews(1);
        }

        public void LoadFavorites()
        {
            AddNews(2);
        }

        public void LoadLocalDB()
        {
            App.database.LoadRSS(Startnr, (Startnr + DBLN));
            Startnr += DBLN;
            
        }

        public void PrintNews()
        {


            /*
            if (App.Instanciated)
            {
                NWT = App.SideMenu.NWT.IsToggled;
                Mariestad = App.SideMenu.Mariestad.IsToggled;
                HJO = App.SideMenu.HJO.IsToggled;
                SLA = App.SideMenu.SLA.IsToggled;
            }


            foreach (var Article in ArticleList)
            {
                if ((Article.Source == "NWT" && NWT) ||
                   (Article.Source == "Mariestad" && Mariestad) ||
                   (Article.Source == "Hjo" && HJO) ||
                   (Article.Source == "SLA" && SLA))
                {
                    Article.Visibility(true);
                }
                else
                {
                    Article.Visibility(false);
                }
            }
            */

        }

        public void AddNews(int argc)
        {
            List<RSSTable> Rss = new List<RSSTable>();
            if(argc == 0)
            {
                Rss = App.database.GetRSS(Stopnr);
            }
            else if(argc == 1)
            {
                var RAL = App.database.GetHistory(DBLN);
                Console.WriteLine("History Gotten: " + RAL.Count());
                foreach(var RA in RAL)
                {
                    Rss.Add(App.database.GetServerRSS(RA.Article).First());
                    Console.WriteLine("Artikel Inlagd");
                }

            }
            else if (argc == 2)
            {
                var FAL = App.database.GetFavorites(App.LoggedinUser.ID);
                Console.WriteLine("Favorites Gotten.");
                Console.WriteLine("Favorites Gotten: " + FAL.Count());
                foreach (var FA in FAL)
                {
                    Rss.Add(App.database.GetServerRSS(FA.Article).First());
                    Console.WriteLine("Artikel Inlagd");
                }
            }
            else
            {
                Rss = App.database.GetRSS(Stopnr);
            }

            
            Console.WriteLine(Rss.Count);
            Console.WriteLine("Inladdning Klar");
            foreach (RSSTable RSS in Rss)
            {
                bool Exists = false;

                foreach (var Article in ArticleList)
                {
                    if (Article.ID == RSS.ID)
                    {
                        Exists = true;
                    }
                }
                Console.WriteLine(ArticleList.Count);
                Console.WriteLine("Jämnförelse ned ArtikelLista klar");
                if (!Exists)
                {
                    var Box = new Article(RSS);
                    Console.WriteLine("ArtikelObjekt Skapat");
                    ArticleList.Add(Box);
                    

                    //column (left) = 0, right = column + column span; 0 + 5 = 6.  row (top) = 1, bottom = row + row span; 1 + 1 = 2

                    if (Box.Full)
                    {
                        Console.WriteLine("Indelning av Fullt Artikel Objekt");

                        NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        NewsGrid.RowSpacing = 0;

                        NewsGrid.Children.Add(Box.Frame, 0, 3, Rownr, Rownr + 3); //Boxview
                        NewsGrid.Children.Add(Box.Box, 0, 3, Rownr, Rownr + 3); //Boxview
                        NewsGrid.Children.Add(Box.Image, 0, 3, Rownr + 1, Rownr + 2); //Image
                        NewsGrid.Children.Add(Box.Label, 0, 3, Rownr + 2, Rownr + 3); //Label

                        Console.WriteLine("Artikel Objekt Lagd i Grid");
                        int temp = 0;
                        if (!First)
                        {
                            
                            temp = NewsGrid.Children.IndexOf(Box.Frame);
                            NewsGrid.Children[temp - 1].HeightRequest = 10;
                            NewsGrid.Children[temp - 1].WidthRequest = 1000;
                        }
                        else
                        {
                            First = false;
                        }

                        if (Box.Plus)
                        {
                            NewsGrid.Children.Add(Box.CornerImage, 0, 3, Rownr + 1, Rownr + 2); //CornerImage
                            NewsGrid.Children.Add(Box.PlusImage, 0, 3, Rownr + 1, Rownr + 2); //PlusImage
                        }
                        
                        NewsGrid.Children.Add(Box.ArticleMargin, 0, 3, Rownr + 3, Rownr + 4); //Margin

                        Console.WriteLine("Val om Plus Artikel Klar");

                        temp = 0;
                        temp = NewsGrid.Children.IndexOf(Box.ArticleMargin);
                        NewsGrid.Children[temp].HeightRequest = 1;
                        NewsGrid.Children[temp].WidthRequest = 380;
                        Box.ArticleMargin.HorizontalOptions = LayoutOptions.Center;

                        Console.WriteLine("Margin Inlagd");

                        NewsGrid.RowDefinitions[Rownr].Height = 30;

                        NewsGrid.RowDefinitions[Rownr + 2].Height = 110;
                    }
                    else
                    {
                        NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
                        NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 120 });
                        NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
                        NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
                        NewsGrid.RowSpacing = 0;

                        NewsGrid.Children.Add(Box.Frame, 0, 3, Rownr, Rownr + 3); //Boxview
                        NewsGrid.Children.Add(Box.Box, 0, 3, Rownr, Rownr + 3); //Boxview
                        NewsGrid.Children.Add(Box.Image, 2, 3, Rownr, Rownr + 3); //Image
                        NewsGrid.Children.Add(Box.Label, 0, 2, Rownr, Rownr + 3); //Label

                        if (Box.Plus)
                        {
                            NewsGrid.Children.Add(Box.CornerImage, 2, 3, Rownr, Rownr + 3); //CornerImage
                            NewsGrid.Children.Add(Box.PlusImage, 2, 3, Rownr, Rownr + 3); //PlusImage
                        }

                        NewsGrid.Children.Add(Box.ArticleMargin, 0, 3, Rownr + 3, Rownr + 4); //Margin

                        int temp = 0;
                        temp = NewsGrid.Children.IndexOf(Box.ArticleMargin);
                        NewsGrid.Children[temp].HeightRequest = 1;
                        NewsGrid.Children[temp].WidthRequest = 380;

                        Box.ArticleMargin.HorizontalOptions = LayoutOptions.Center;
                    }
                    
                    Rownr++;
                    Rownr++;
                    Rownr++;
                    Rownr++;

                    NewsGrid.RowDefinitions[0].Height = 0;

                }
            }
            //NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //NewsGrid.Children.Add(LoadNewsButton, 0, Rownr);
            //Console.WriteLine("Nyheter inlagda i Grid");
            /*
            foreach (Article A in ArticleList)
            {
                if (App.database.GetReadArticle(A.ID).Count > 0)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        A.Frame.Color = Color.FromRgb(80, 210, 194);
                    });

                }
            }*/
            PrintNews();

        }

        


    }
}