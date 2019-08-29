using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public ListView ArticleListView;
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

                int BL = 28;
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

            NewsSV.ClassId = null;


            

            TGR = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };



            if (Argc == 0)
            {
            }
            else 
            {
                EmptyText.Text = "";
                EmptyText.IsEnabled = false;
                EmptyText.IsVisible = false;
                CreateFeed(Argc);
            }

            




            //NewsButtonN.Image = ImageSource.FromFile("newsfeed.png");
        }

        public void CreateListView()
        {
            ArticleListView = new ListView
            {
                // Source of data items.
                
                ItemsSource = ArticleList,
                HasUnevenRows = true,
                SeparatorVisibility = SeparatorVisibility.None,
                BackgroundColor = Color.FromHex("#FFFFFF"),
                //IsPullToRefreshEnabled = true,
                Footer = Down,

                




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
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 25,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,

                        TextColor = Color.Black,
                        //ClassId = NF.Article.ToString(),
                        InputTransparent = true,
                        Margin = new Thickness(15, 5, 15, 0),
                    };

                    Label.SetBinding(HeightRequestProperty, "HeaderLength");

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
                        HeightRequest = 10,
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
                    new RowDefinition { Height = 0 },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = 10 },

                    },

                        ColumnDefinitions = {
                    new ColumnDefinition { Width = 0 },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = 0 },
                    },
                        RowSpacing = 0,
                        ColumnSpacing = 0,
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
            bool LoadFailure = false;
            var Header = (Button)sender;
            Header.IsEnabled = false;
            var id = Int32.Parse(Header.ClassId);

            await System.Threading.Tasks.Task.Run(async () =>
            {
                var NP = new ContentPage();
                
                bool OK = false;
                
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //IsBusy = true;
                    
                    LoadingPopUp x = new LoadingPopUp();
                    x.loadingAnimation.Play();
                    await Navigation.PushAsync(x);
                    
                    
                });


                //ANVÄND KOD HÄR
                var RSSTable = App.database.GetServerRSS(id);
                if (RSSTable != null)
                {
                    RSSTable RSS = RSSTable.First();
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        NP = new NewsPage(RSS, argc);
                        OK = true;
                    });
                    
                }
                else
                {
                    LoadFailure = true;
                }

                




                await System.Threading.Tasks.Task.Delay(1000);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Console.WriteLine("Initiering Klar");                   
                    
                    await Navigation.PopAsync();
                    if(OK)
                    {
                        await Navigation.PushAsync(NP);
                    }
                    
                    //IsBusy = false;
                });

            });
            Header.IsEnabled = true;
            if (LoadFailure)
            {
                await DisplayAlert("Article Load Failure", "Artikeln misslyckades att laddas in, vänligen försök igen.", "OK");
            }

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

            Rownr = 1;


            NewsGrid.Children.Clear();


            CreateListView();

            NewsGrid.Children.Add(ArticleListView, 0, 3, 1, 2);
            //NewsGrid.Children.Add(Down, 0, 3, 2, 3);

            First = false;

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

                if(RAL != null)
                {
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

                

            }
            else if (argc == 2)
            {
                Down.IsVisible = false;
                int j = 0;
                var FAL = App.database.GetFavorites(App.LoggedinUser.ID);

                if(FAL != null)
                {
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

            /*
            ArticlePrintList.Clear();

            if(ArticleList.Count < 20)
            {
                CURR = ArticleList.Count;
            }


            for(int j = PREV; j < CURR; j++)
            {
                ArticlePrintList.Add(ArticleList[j]);
            }*/
            if (First)
            {
                PrintNews();
            }
            
        }



        public async void ListViewScroll(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                Device.BeginInvokeOnMainThread(async() =>
                {
                    //IsBusy = true;
                    //ArticleListView.IsRefreshing = true;
                    LoadingPopUp x = new LoadingPopUp();
                    x.loadingAnimation.Play();
                    await Navigation.PushAsync(x);
                    
                });
                
                if (argc == 0)
                {
                    PREV = 0;
                    CURR = NEXT;
                    NEXT += DBLN;

                    Console.WriteLine("PREV: " + PREV + " CURR: " + CURR + " NEXT: " + NEXT);

                    double height = NewsSV.ContentSize.Height - 10;

                    LoadLocalDB();
                    AddNews(argc);

                    

                    

                }
                Device.BeginInvokeOnMainThread(async() =>
                {

                    ArticleListView.ItemsSource = null;
                    ArticleListView.ItemsSource = ArticleList;


                   
                });


                GC.Collect();

                
                await System.Threading.Tasks.Task.Delay(1000);
                Device.BeginInvokeOnMainThread(async() =>
                {
                    Console.WriteLine("Initiering Klar");

                    await Navigation.PopAsync();
                    //App.Mainpage.CurrentPage = App.Mainpage.Children[1];
                    await NewsSV.ScrollToAsync(0, ArticleListView.Height - 10, false);
                    //ArticleListView.IsRefreshing = false;
                    //IsBusy = false;
                });

            });






        }

    }
}