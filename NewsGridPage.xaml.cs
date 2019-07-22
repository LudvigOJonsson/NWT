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
        public static int DBLN = 20;
        public int Rownr = 1;
        public static TapGestureRecognizer TGR;
        public List<Article> ArticleList = new List<Article>();
        public List<Article> ArticlePrintList = new List<Article>();
        public static string Defaultimage = "http://media2.hitzfm.nu/2016/11/Nyheter_3472x1074.jpg";
        public static Random rnd = new Random();
        public string Filter = "All";
        public string Author = "";
        public string Tag = "";

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
            public bool Plus { get; set; }
            public bool Full { get; set; }
            public int IHR { get; set; }          
            public int BHR { get; set; }
            public int CBWR { get; set; }
            public int CBHR { get; set; }
            public LayoutOptions CBHO { get; set; }
            public LayoutOptions CBVO { get; set; }
            public Article(NewsfeedTable NF)
            {
                

                Tag = NF.Category;
                ID = NF.Article;
                Header = NF.Header;
                IMGSource = NF.Image;
                Full = true;


                Plus = Convert.ToBoolean(NF.Plus);

                int BL = 33;
                int BH = 35;

                if(Header.Length < BL)
                {
                    HeaderLength = BH;
                }
                else if (Header.Length < BL*2)
                {
                    HeaderLength = BH*2;
                }
                else if (Header.Length < BL*3)
                {
                    HeaderLength = BH*3;
                }

                if (IMGSource.ToString() == Defaultimage)
                {
                    IMGSource = "";
                    IHR = 1;
                    CBWR = 7;
                    CBHR = HeaderLength;
                    BHR = HeaderLength;
                    CBHO = LayoutOptions.Start;
                    CBVO = LayoutOptions.FillAndExpand;
                    Full = false;
                }
                else
                {
                    IHR = 200;
                    BHR = 200 + HeaderLength;
                    CBWR = 200;
                    CBHR = 7;
                    CBHO = LayoutOptions.FillAndExpand;
                    CBVO = LayoutOptions.Start;
                }


                Console.WriteLine("Artikel Klar");
            }







        }


        public NewsGridPage(int Argc)
        {
            InitializeComponent();

            




            TGR = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };



            if (Argc == 0)
            {
            }
            else 
            {
                CreateFeed(Argc);
            }

            




            //NewsButtonN.Image = ImageSource.FromFile("newsfeed.png");
        }

        public void CreateFeed(int Argc)
        {
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
        }



        public void ArtikelClicked(object sender, EventArgs e)
        {
            var Header = (View)sender;
            Console.WriteLine(Header.ClassId);
            Console.WriteLine(App.Mainpage.Children[0].Navigation.NavigationStack[1].GetType());
            App.Mainpage.Children[0].Navigation.NavigationStack[1].ClassId = Header.ClassId;

            Navigation.PopAsync();
        }


        async void LoadNews(object sender, EventArgs e)
        {

            var Header = (Button)sender;
            Header.IsEnabled = false;
            var id = Int32.Parse(Header.ClassId);
            var RSS = App.database.GetServerRSS(id).First();
            await Navigation.PushAsync(new NewsPage(RSS, argc));

            Header.IsEnabled = true;

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
            App.database.LoadNF(Loadnr, (Loadnr + DBLN),Filter, Author , Tag);
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
                HasUnevenRows = true,
                SeparatorVisibility = SeparatorVisibility.None,
                BackgroundColor = Color.FromHex("#FFFFFF"),

            // Define template for displaying each item.
            // (Argument of DataTemplate constructor is called for 
            //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    int IMGXC = 200;
                    //int IMGYC = 250;

                    Label Label = new Label
                    {
                        //Text = NF.Header,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Start,
                        FontSize = 25,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        
                        TextColor = Color.Black,
                        //ClassId = NF.Article.ToString(),
                        InputTransparent = true,
                        Margin = new Thickness(15, 5, 15, 0),
                    };

                    Label.SetBinding(HeightRequestProperty,"HeaderLength");

                    Image Image = new Image
                    {


                        //Source = NF.Image,
                        WidthRequest = IMGXC,
                        //HeightRequest = IMGYC,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Aspect = Aspect.AspectFill,
                        InputTransparent = true,
                        
                        // ClassId = NF.Article.ToString()


                    };

                    Image.SetBinding(HeightRequestProperty, "IHR");

                    Button Box = new Button
                    {
                        BackgroundColor = Color.White,
                        WidthRequest = IMGXC,
                        //HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        //ClassId = NF.Article.ToString(),
                        BorderColor = Color.FromHex("#f0f0f0"),
                        
                        
                    };

                    Box.SetBinding(HeightRequestProperty, "BHR");

                    if (argc == 1)
                    {
                        Box.Clicked += ArtikelClicked;
                    }
                    else
                    {
                        Box.Clicked += LoadNews;
                    }


                    BoxView ArticleMargin = new BoxView
                    {
                        Color = Color.FromHex("#f2f2f2"),
                        WidthRequest = IMGXC,
                        HeightRequest = 20,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        InputTransparent = true,
                        //ClassId = NF.Article.ToString()
                    };

                    BoxView CategoryBox = new BoxView
                    {
                        
                        BackgroundColor = App.MC,
                        ///WidthRequest = Label.WidthRequest,
                        //HeightRequest = 3,
                        //HorizontalOptions =,
                        //VerticalOptions = LayoutOptions.Start,
                        InputTransparent = true,
                        
                    };

                    Image Shadow = new Image
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        InputTransparent = true,
                        Source = "shadow.png"
                    };

                    //Label.GestureRecognizers.Add(TGR);
                    //Image.GestureRecognizers.Add(TGR);

                    CategoryBox.SetBinding(HeightRequestProperty, "CBHR");
                    CategoryBox.SetBinding(WidthRequestProperty, "CBWR");
                    CategoryBox.SetBinding(BoxView.VerticalOptionsProperty, "CBVO");
                    CategoryBox.SetBinding(BoxView.HorizontalOptionsProperty, "CBHO");

                    Label.SetBinding(Label.TextProperty, "Header");
                    Image.SetBinding(Image.SourceProperty, "IMGSource");

                    Label.SetBinding(Label.ClassIdProperty, "ID");
                    Image.SetBinding(Image.ClassIdProperty, "ID");

                    Box.SetBinding(Button.ClassIdProperty, "ID");
        
                    ArticleMargin.SetBinding(BoxView.ClassIdProperty, "ID");

                    var Grid = new Grid
                    {
                        
                        RowDefinitions = {
                    new RowDefinition { Height = 20 },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },

                    },

                        ColumnDefinitions = {
                    new ColumnDefinition { Width = 1 },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = 1 },
                    },
                        RowSpacing = 0,
                        ColumnSpacing = 14,
                        BackgroundColor = Color.FromHex("#f2f2f2")



                    };





                    //Label.HeightRequest = ((Label.Text.Length / 30)) * 50;
                    Label.WidthRequest = Label.Width - 25;


                    Grid.Children.Add(ArticleMargin, 1, 2, 0, 1); //Boxview
                    Grid.Children.Add(Box, 1, 2, 1, 3); //Boxview
                    Grid.Children.Add(Image, 1, 2, 1, 2); //Image   
                    Grid.Children.Add(CategoryBox, 1, 2, 2, 3); //Label
                    Grid.Children.Add(Label, 1, 2, 2, 3); //Label
                    Grid.Children.Add(Shadow, 1, 2, 3, 4);


                    Console.WriteLine("Utdata: " + Label.Text);

                    

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = Grid
                    };
                })
                
            };
           
            NewsGrid.Children.Add(listView, 0, 3, 1, 2);
            NewsGrid.Children.Add(Down, 0, 3, 2, 3);



        }

        private void Box_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
                    var NF = new NewsfeedTable
                    {
                        ID = 0,
                        NewsScore = 5,
                        Image = RA.Image,
                        Article = RA.Article,
                        Category = "N/A",
                        Header = RA.Header,
                        Plus = 0
                    };
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
                    var NF = new NewsfeedTable
                    {
                        ID = 0,
                        NewsScore = 5,
                        Image = FA.Image,
                        Article = FA.Article,
                        Category = "N/A",
                        Header = FA.Header,
                        Plus = 0
                    };
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