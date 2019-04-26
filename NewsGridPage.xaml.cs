using Rg.Plugins.Popup.Services;
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
        public int Loadnr = 1;
        public static int DBLN = 30;
        public int Rownr = 1;
        public static TapGestureRecognizer TGR;
        public List<Article> ArticleList = new List<Article>();
        public List<Article> ArticlePrintList = new List<Article>();
        public static string Defaultimage = "http://media2.hitzfm.nu/2016/11/Nyheter_3472x1074.jpg";
        public static Random rnd = new Random();

        public int PREV = 0;
        public int CURR = DBLN;
        public int NEXT = DBLN * 2;
        public bool ScrollLock = false;


        public bool First = true;
        public int argc = 0;

        public class Article
        {
            public long ID { get; set; }
            public string Source { get; set; }
            public string Tag { get; set; }
            public string Header { get; set; }
            public string IMGSource { get; set; }
            public int HeaderLength { get; set; }
            public bool Plus = false;
            public bool Full = true;
            public Button Box = new Button { };
            public BoxView Frame = new BoxView { };
            public BoxView ArticleMargin = new BoxView { };
            public BoxView CategoryBox = new BoxView { };
            public Label Label = new Label { };
            public Image Image = new Image { };


            public Article(NewsfeedTable NF)
            {


                Tag = NF.Category;
                ID = NF.Article;
                Header = NF.Header;
                IMGSource = NF.Image;
                Plus = Convert.ToBoolean(NF.Plus);

                HeaderLength = Header.Length;

                Console.WriteLine("Artikel Klar");
            }

            public void Visibility(bool State)
            {

                Image.IsVisible = State;
                Label.IsVisible = State;
                Box.IsVisible = State;
                Frame.IsVisible = State;
                ArticleMargin.IsVisible = State;

            }




        }


        public NewsGridPage(int Argc)
        {
            InitializeComponent();


            for (int i = 0; i < DBLN; i++)
            {

            }
 

            TGR = new TapGestureRecognizer();
            TGR.NumberOfTapsRequired = 1;



            if (Argc == 0)
            {
                Console.WriteLine("NewsGrid");
                TGR.Tapped += (s, e) => {
                    IsEnabled = false;
                    LoadNews(s, e);
                    IsEnabled = true;
                };
                LoadLocalDB();

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
            var RSS = App.database.GetServerRSS(id).First();
            await Navigation.PushAsync(new NewsPage(RSS, argc));



        }

        public void LoadHistory()
        {
            argc = 1;
            AddNews(1);
        }

        public void LoadFavorites()
        {
            argc = 2;
            AddNews(2);
        }

        public void LoadLocalDB()
        {
            App.database.LoadNF(Loadnr, (Loadnr + DBLN));
            Loadnr += DBLN;

        }

        public void PrintNews()
        {
            int Start = 0;
            int End = 0;
            Rownr = 1;


            Start = PREV;
            End = CURR;

            NewsGrid.Children.Clear();

            if (PREV > 0)
            {
                NewsGrid.Children.Add(Up, 0, 3, 0, 1);
            }

            ListView listView = new ListView
            {
                // Source of data items.
                
                ItemsSource = ArticlePrintList,
                RowHeight = 400,
                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    int IMGXC = 200;
                    int IMGYC = 250;

                    Label Label = new Label
                    {
                        //Text = NF.Header,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Start,
                        FontSize = 25,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        //HeightRequest = ((NF.Header.Length/30))*50,
                        TextColor = Color.Black,
                        //ClassId = NF.Article.ToString(),
                        Margin = 15,
                    };

                    Label.SetBinding(HeightRequestProperty,"HeaderLength");

                    Image Image = new Image
                    {


                        //Source = NF.Image,
                        WidthRequest = IMGXC,
                        HeightRequest = IMGYC,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Aspect = Aspect.AspectFill,
                        Margin = 0,
                        // ClassId = NF.Article.ToString()


                    };

                    Button Box = new Button
                    {
                        BackgroundColor = Color.White,
                        WidthRequest = IMGXC,
                        HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        //ClassId = NF.Article.ToString(),
                        Margin = 0,
                        CornerRadius = 0,
                        BorderWidth = 1,
                        BorderColor = Color.FromHex("#f0f0f0"),
                    };

                    BoxView Frame = new BoxView
                    {
                        Color = Color.White,
                        WidthRequest = IMGXC,
                        HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        //ClassId = NF.Article.ToString()
                    };

                    BoxView ArticleMargin = new BoxView
                    {
                        Color = Color.White,
                        WidthRequest = IMGXC,
                        HeightRequest = 20,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        //ClassId = NF.Article.ToString()
                    };

                    BoxView CategoryBox = new BoxView
                    {
                        BackgroundColor = Color.FromHex("#2f6e83"),
                        WidthRequest = Label.WidthRequest,
                        HeightRequest = 3,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Start,
                        Margin = 0,
                    };

                    Label.GestureRecognizers.Add(TGR);
                    Image.GestureRecognizers.Add(TGR);
                    Box.GestureRecognizers.Add(TGR);
                    /*
                    if (Image.Source.ToString() == Defaultimage)
                    {
                        Image.Source = "";
                        CategoryBox.HeightRequest = Label.Height;

                        Box.HeightRequest = Label.Height;
                        Image.HeightRequest = 1;

                        CategoryBox.WidthRequest = 7;
                        CategoryBox.HeightRequest = Label.HeightRequest;
                        CategoryBox.HorizontalOptions = LayoutOptions.Start;
                        CategoryBox.VerticalOptions = LayoutOptions.FillAndExpand;
                    }*/

                    Label.SetBinding(Label.TextProperty, "Header");
                    Image.SetBinding(Image.SourceProperty, "IMGSource");

                    Label.SetBinding(Label.ClassIdProperty, "ID");
                    Image.SetBinding(Image.ClassIdProperty, "ID");

                    Box.SetBinding(Button.ClassIdProperty, "ID");
                    Frame.SetBinding(BoxView.ClassIdProperty, "ID");
                    ArticleMargin.SetBinding(BoxView.ClassIdProperty, "ID");

                    var Grid = new Grid
                    {

                        RowDefinitions = {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    },

                        ColumnDefinitions = {
                    new ColumnDefinition { Width = 1 },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = 1 },
                    },

                        ColumnSpacing = 14,
                        BackgroundColor = Color.White



                    };

                    Label.WidthRequest = Label.Width - 25;

                    //Grid.Children.Add(Frame, 0, 3, 0, 0 + 3); //Boxview
                    Grid.Children.Add(ArticleMargin, 1, 2, 0, 1); //Boxview
                    Grid.Children.Add(Box, 1, 2, 1, 4); //Boxview
                    Grid.Children.Add(Image, 1, 2, 2, 3); //Image
                    Grid.Children.Add(Label, 1, 2, 3, 4); //Label
                    Grid.Children.Add(CategoryBox, 1, 2, 3, 4); //Label

                    Console.WriteLine("Utdata: " + Label.Text);

                    

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = Grid
                    };
                })
                
            };

            NewsGrid.Children.Add(listView, 0, 3, 1, 2);

            /*
                for (int i = Start; i < End; i++)
                {
                    Article Box = ArticleList[i];



                    Console.WriteLine("Indelning av Fullt Artikel Objekt");


                    NewsGrid.RowSpacing = 0;

                    Box.Label.WidthRequest = Box.Label.Width - 25;

                    NewsGrid.Children.Add(Box.Frame, 0, 3, Rownr, Rownr + 3); //Boxview
                    NewsGrid.Children.Add(Box.ArticleMargin, 1, 2, Rownr, Rownr + 1); //Boxview
                    NewsGrid.Children.Add(Box.Box, 1, 2, Rownr + 1, Rownr + 4); //Boxview
                    NewsGrid.Children.Add(Box.Image, 1, 2, Rownr + 2, Rownr + 3); //Image
                    NewsGrid.Children.Add(Box.Label, 1, 2, Rownr + 3, Rownr + 4); //Label
                    NewsGrid.Children.Add(Box.CategoryBox, 1, 2, Rownr + 3, Rownr + 4); //Label
                    NewsGrid.RowDefinitions[Rownr + 2].Height = ((Box.Label.Text.Length / 10) * );



                    Console.WriteLine("Artikel Objekt Lagd i Grid");


  






                    Rownr++;
                    Rownr++;
                    Rownr++;
                    Rownr++;
                }
            
            */
            NewsGrid.Children.Add(Down, 0, 3, 2, 3);






            if (App.Instanciated && false)
            {
                int i = 0;
                foreach (var Article in ArticlePrintList)
                {

                    if ((Article.Tag.Contains("Nyheter") && App.SideMenu.Nyheter.IsToggled) ||
                       (Article.Tag.Contains("Brott och Blåljus") && App.SideMenu.BrottochBlåljus.IsToggled) ||
                       (Article.Tag.Contains("Vård och Omsorg") && App.SideMenu.VårdochOmsorg.IsToggled) ||
                       (Article.Tag.Contains("Miljö") && App.SideMenu.Miljö.IsToggled) ||
                       (Article.Tag.Contains("Skola och Utbildning") && App.SideMenu.SkolaochUtbildning.IsToggled) ||
                       (Article.Tag.Contains("Mat och Dryck") && App.SideMenu.MatochDryck.IsToggled) ||
                       (Article.Tag.Contains("Bostad") && App.SideMenu.Bostad.IsToggled) ||
                       (Article.Tag.Contains("Trafik") && App.SideMenu.Trafik.IsToggled) ||
                       (Article.Tag.Contains("Politik") && App.SideMenu.Politik.IsToggled) ||
                       (Article.Tag.Contains("Sport") && App.SideMenu.Sport.IsToggled) ||
                       (Article.Tag.Contains("Ekonomi") && App.SideMenu.Ekonomi.IsToggled) ||
                       (Article.Tag.Contains("Åsikter") && App.SideMenu.Åsikter.IsToggled) ||
                       (Article.Tag.Contains("Nöje och Kultur") && App.SideMenu.NöjeochKultur.IsToggled) ||
                       (Article.Tag.Contains("Familj") && App.SideMenu.Familj.IsToggled) ||
                        Article.Tag.Contains("N/A"))
                    {
                        Article.Visibility(true);
                        Console.WriteLine("Article: " + i + " ;True");
                    }
                    else
                    {
                        Article.Visibility(false);
                        Console.WriteLine("Article: " + i + " ;False");
                    }
                    i++;
                }
            }
        }

        public void AddNews(int argc)
        {

            List<NewsfeedTable> Rss = new List<NewsfeedTable>();
            if (argc == 0)
            {
                Rss = App.database.GetNF(Loadnr);
            }
            else if (argc == 1)
            {
                Down.IsVisible = false;
                int j = 0;
                var RAL = App.database.GetHistory(App.LoggedinUser.ID);
                Console.WriteLine("History Gotten: " + RAL.Count());
                foreach (var RA in RAL)
                {
                    var NF = new NewsfeedTable();
                    NF.ID = 0;
                    NF.NewsScore = 5;
                    NF.Image = RA.Image;
                    NF.Article = RA.Article;
                    NF.Category = "N/A";
                    NF.Header = RA.Header;
                    NF.Plus = 0;
                    Rss.Add(NF);
                    j++;
                    Console.WriteLine("Artikel Inlagd");
                }
                CURR = j;

            }
            else if (argc == 2)
            {
                Down.IsVisible = false;
                int j = 0;
                var FAL = App.database.GetFavorites(App.LoggedinUser.ID);
                Console.WriteLine("Favorites Gotten: " + FAL.Count());
                foreach (var FA in FAL)
                {
                    var NF = new NewsfeedTable();
                    NF.ID = 0;
                    NF.NewsScore = 5;
                    NF.Image = FA.Image;
                    NF.Article = FA.Article;
                    NF.Category = "N/A";
                    NF.Header = FA.Header;
                    NF.Plus = 0;
                    Rss.Add(NF);
                    j++;
                    Console.WriteLine("Artikel Inlagd");
                }
                CURR = j;
            }
            else
            {
                Rss = App.database.GetNF(Loadnr);
            }



            Console.WriteLine(Rss.Count);
            Console.WriteLine("Inladdning Klar");
            int i = 1;

            foreach (NewsfeedTable NF in Rss)
            {
                bool Exists = false;

                foreach (var Article in ArticleList)
                {
                    if (Article.ID == NF.Article)
                    {
                        Exists = true;
                    }
                }
                //Console.WriteLine(ArticleList.Count);
                Console.WriteLine("Jämnförelse ned ArtikelLista klar");
                if (!Exists)
                {
                    var Box = new Article(NF);

                    Console.WriteLine("ArtikelObjekt Skapat");
                    ArticleList.Add(Box);
                }
                i++;
            }


            ArticlePrintList.Clear();
            for(int j = PREV; j < CURR; j++)
            {
                ArticlePrintList.Add(ArticleList[j]);
            }
            
            PrintNews();
        }

        public async void Scrollup(object sender, EventArgs e)
        {
            IsBusy = true;
            if (argc == 0)
            {

                NEXT = CURR;
                CURR = PREV;
                PREV -= DBLN;
                if (PREV <= 0)
                {
                    PREV = 0;
                    CURR = DBLN;
                    NEXT = DBLN * 2;
                }



                Console.WriteLine("PREV: " + PREV + " CURR: " + CURR + " NEXT: " + NEXT);


                LoadLocalDB();
                AddNews(argc);
                await NewsSV.ScrollToAsync(0, NewsSV.ContentSize.Height - 10, false);

                GC.Collect();

            }
            IsBusy = false;
        }

        public async void Scrolldown(object sender, EventArgs e)
        {
            IsBusy = true;
            if (argc == 0)
            {
                PREV = CURR;
                CURR = NEXT;
                NEXT += DBLN;

                Console.WriteLine("PREV: " + PREV + " CURR: " + CURR + " NEXT: " + NEXT);


                LoadLocalDB();
                AddNews(argc);
                await NewsSV.ScrollToAsync(0, 10, false);

                GC.Collect();

            }
            IsBusy = false;
        }
    }
}