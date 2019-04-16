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
        public static int DBLN = 10;
        public int Rownr = 1;
        public static TapGestureRecognizer TGR;
        public List<Article> ArticleList = new List<Article>();
        public static string Defaultimage = "http://media2.hitzfm.nu/2016/11/Nyheter_3472x1074.jpg";
        public static Random rnd = new Random();

        public int PREV = 0;
        public int CURR = DBLN;
        public int NEXT = DBLN*2;
        public bool ScrollLock = false;



        public bool First = true;
        public int argc = 0;

        public class Article
        {
            public long ID = 0;
            public string Source = "";
            public string Tag = "";
            public bool Plus = false;
            public bool Full = true;
            public Button Box = new Button { };
            public BoxView Frame = new BoxView { };
            public BoxView ArticleMargin = new BoxView { };
            public BoxView CategoryBox = new BoxView { };
            public Label Label = new Label { };
            public Label NrLabel = new Label { };
            public Image Image = new Image { };
            public Image PlusImage = new Image { };
            public Image CornerImage = new Image { };
            public Image CheckImage = new Image { };

            public Article(NewsfeedTable NF)
            {


                Tag = NF.Category;
                ID = NF.Article;
                Plus = Convert.ToBoolean(NF.Plus);
                
                int IMGXC = 200;
                int IMGYC = 250;
                /*
                if (RSS.NewsScore > 3 && RSS.ImgSource != "http://media2.hitzfm.nu/2016/11/Nyheter_3472x1074.jpg")
                {
                    Console.WriteLine("Full Artikel");
                    Full = true;
                }
                else
                {
                    Console.WriteLine("Halv Artikel");
                }
                */


                if (Full) {

                    //RSS.Title = RSS.Title.Replace("\"", "'");
                    



                    Label = new Label
                    {
                        Text = NF.Header,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Start,
                        FontSize = 25,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        //HeightRequest = ((NF.Header.Length/30))*50,
                        TextColor = Color.Black,
                        ClassId = NF.Article.ToString(),
                        Margin = 15,
                    };

                    Label.GestureRecognizers.Add(TGR);

                    NrLabel = new Label
                    {
                        
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Start,
                        FontSize = 25,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        //HeightRequest = ((NF.Header.Length/30))*50,
                        TextColor = Color.Red,
                        ClassId = NF.Article.ToString(),
                        Margin = 15,
                    };

                    NrLabel.GestureRecognizers.Add(TGR);


                    if (NF.Image == null) { NF.Image = Defaultimage; }
                    Image = new Image
                    {
                    
                        Source = NF.Image,
                        WidthRequest = IMGXC,
                        HeightRequest = IMGYC,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        Aspect = Aspect.AspectFill,
                        Margin = 0,
                        ClassId = NF.Article.ToString()

                    };

                    Box = new Button
                    {
                        BackgroundColor = Color.White,
                        WidthRequest = IMGXC,
                        HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        ClassId = NF.Article.ToString(),
                        Margin = 0,
                        CornerRadius = 0,
                        BorderWidth = 1,
                        BorderColor = Color.FromHex("#f0f0f0"),
                    };

                    Box.GestureRecognizers.Add(TGR);

                    Frame = new BoxView
                    {
                        Color = Color.White,
                        WidthRequest = IMGXC,
                        HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        ClassId = NF.Article.ToString()
                    };


                    Image.GestureRecognizers.Add(TGR);
                    //Image.IsVisible = false;
                    PlusImage = new Image
                    {
                        Source = "plus.png",
                        WidthRequest = 40,
                        HeightRequest = 40 ,
                        Margin = 15,
                        Aspect = Aspect.AspectFill,
                        ClassId = NF.Article.ToString(),
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.Start,

                    };

                    CornerImage = new Image
                    {

                        Source = "plusBackground.png",
                        WidthRequest = 40,
                        HeightRequest = 40,
                        Margin = 15,
                        Aspect = Aspect.AspectFill,
                        ClassId = NF.Article.ToString(),
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.Start,

                    };

                    CheckImage = new Image
                    {

                        
                        WidthRequest = 40,
                        HeightRequest = 40,
                        Margin = 15,
                        Aspect = Aspect.AspectFill,
                        ClassId = NF.Article.ToString(),
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.Start,

                    };

                    ArticleMargin = new BoxView
                    {
                        Color = Color.White,
                        WidthRequest = IMGXC,
                        HeightRequest = 20,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        ClassId = NF.Article.ToString()
                    };
                    CategoryBox = new BoxView
                    {
                        BackgroundColor = Color.FromHex("#2f6e83"),
                        WidthRequest = Label.WidthRequest,
                        HeightRequest = 3,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Start,
                        Margin = 0,
                    };
                }
                else
                {

                    Label = new Label
                    {
                        Text = NF.Header,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 15,
                        FontAttributes = FontAttributes.Bold,
                        WidthRequest = IMGXC/2,
                        HeightRequest = ((NF.Header.Length / 15)) * 10,
                        TextColor = Color.Black,
                        ClassId = NF.Article.ToString(),
                        Margin = 12,
                    };

                    Label.GestureRecognizers.Add(TGR);
                    if (NF.Image == null) { NF.Image = Defaultimage; }
                    Image = new Image
                    {

                        Source = NF.Image,
                        WidthRequest = IMGXC/2,
                        HeightRequest = IMGYC/2,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        Aspect = Aspect.AspectFit,
                        Margin = 25,
                        ClassId = NF.Article.ToString()

                    };

                    Box = new Button
                    {
                        BackgroundColor = Color.Transparent,
                        WidthRequest = IMGXC,
                        //HeightRequest = ((NF.Header.Length / 15)+1) * 50,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        ClassId = NF.Article.ToString(),
                        Margin = 0,
                    };

                    Frame = new BoxView
                    {
                        Color = Color.White,
                        WidthRequest = IMGXC,
                        HeightRequest = ((NF.Header.Length / 15)+1) * 50,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        ClassId = NF.Article.ToString()
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
                        ClassId = NF.Article.ToString(),
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.End,

                    };

                    CornerImage = new Image
                    {

                        Source = "plusBackground.png",
                        WidthRequest = 30,
                        HeightRequest = 30,
                        Margin = 20,
                        Aspect = Aspect.AspectFill,                        
                        ClassId = NF.Article.ToString(),
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
                        ClassId = NF.Article.ToString()
                    };
                }

                if (NF.Image == null || NF.Image == Defaultimage)
                {

                    Image.Source = "";
                    CategoryBox.HeightRequest = Label.Height;
                    CornerImage.HeightRequest = 0;
                    PlusImage.HeightRequest = 0;
                    Box.HeightRequest = Label.Height;
                    Image.HeightRequest = 1;

                    CategoryBox.WidthRequest = 7;
                    CategoryBox.HeightRequest = Label.HeightRequest;
                    CategoryBox.HorizontalOptions = LayoutOptions.Start;
                    CategoryBox.VerticalOptions = LayoutOptions.FillAndExpand;
                }


                Console.WriteLine("Artikel Klar");
            }

            public void Visibility(bool State)
            {

                Image.IsVisible = State;
                Label.IsVisible = State;
                NrLabel.IsVisible = State;
                Box.IsVisible = State;
                Frame.IsVisible = State;
                CornerImage.IsVisible = State;
                PlusImage.IsVisible = State;
                ArticleMargin.IsVisible = State;
                
            }




        }


        public NewsGridPage(int Argc)
        {
            InitializeComponent();
            NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            for (int i = 0; i< DBLN; i++)
            {
                NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }
            NewsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

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
            if (RSS.Plus == 1)
            {
                if (App.LoggedinUser != null)
                {
                    if (App.database.CheckPlus(RSS.ID))
                    {
                        await Navigation.PushAsync(new NewsPage(RSS,argc));
                    }
                    else
                    {
                        var answer = await DisplayAlert("Plus", "This is a Plus Article. You have to spend 3 Plustokens to gain access to it. Spend a token? (You have " + App.LoggedinUser.Plustokens + " Tokens left.)", "Yes", "No");
                        if (answer)
                        {
                            if (App.database.Plustoken(App.LoggedinUser, -3))
                            {
                                var Plus = new PlusRSSTable();
                                Plus.Article = RSS.ID;
                                Plus.User = App.LoggedinUser.ID;
                                App.database.InsertPlus(Plus);
                                await Navigation.PushAsync(new NewsPage(RSS,argc));
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
                await Navigation.PushAsync(new NewsPage(RSS,argc));
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

            if(PREV > 0)
            {
                NewsGrid.Children.Add(Up, 0,3,0,1);
            }

            for (int i = Start; i < End; i++)
            {
                Article Box = ArticleList[i];
                if (Box.Full)
                {
                    Console.WriteLine("Indelning av Fullt Artikel Objekt");


                    NewsGrid.RowSpacing = 0;

                    Box.Label.WidthRequest = Box.Label.Width - 25;

                    //NewsGrid.Children.Add(Box.Frame, 0, 3, Rownr, Rownr + 3); //Boxview
                    NewsGrid.Children.Add(Box.ArticleMargin, 1, 2, Rownr, Rownr + 1); //Boxview
                    NewsGrid.Children.Add(Box.Box, 1, 2, Rownr + 1, Rownr + 4); //Boxview
                    NewsGrid.Children.Add(Box.Image, 1, 2, Rownr + 2, Rownr + 3); //Image
                    NewsGrid.Children.Add(Box.Label, 1, 2, Rownr + 3, Rownr + 4); //Label
                    NewsGrid.Children.Add(Box.CategoryBox, 1, 2, Rownr + 3, Rownr + 4); //Label
                    //NewsGrid.Children.Add(Box.NrLabel, 0, 1, Rownr + 1, Rownr + 2);
                    //NewsGrid.RowDefinitions[Rownr + 2].Height = ((Box.Label.Text.Length / 10) * );



                    Console.WriteLine("Artikel Objekt Lagd i Grid");
                    /*int temp = 0;
                    if (!First)
                    {

                        temp = NewsGrid.Children.IndexOf(Box.Frame);
                        NewsGrid.Children[temp - 1].HeightRequest = 10;
                        NewsGrid.Children[temp - 1].WidthRequest = 1000;
                    }
                    else
                    {
                        First = false;
                    }*/

                    if (Box.Plus)
                    {
                        NewsGrid.Children.Add(Box.CornerImage, 1, 2, Rownr + 2, Rownr + 3); //CornerImage
                        NewsGrid.Children.Add(Box.PlusImage, 1, 2, Rownr + 2, Rownr + 3); //PlusImage
                        NewsGrid.Children.Add(Box.CheckImage, 1, 2, Rownr + 2, Rownr + 3); //CheckImage
                        Box.CheckImage.IsVisible = false;
                    }

                    /*NewsGrid.Children.Add(Box.ArticleMargin, 0, 3, Rownr + 3, Rownr + 4); //Margin

                    Console.WriteLine("Val om Plus Artikel Klar");

                    temp = 0;
                    temp = NewsGrid.Children.IndexOf(Box.ArticleMargin);
                    NewsGrid.Children[temp].HeightRequest = 1;
                    NewsGrid.Children[temp].WidthRequest = 380;
                    Box.ArticleMargin.HorizontalOptions = LayoutOptions.Center;

                    Console.WriteLine("Margin Inlagd");

                    NewsGrid.RowDefinitions[Rownr].Height = 30;

                    NewsGrid.RowDefinitions[Rownr + 2].Height = 110;*/
                }
                else
                {
                    /*NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
                    NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 120 });
                    NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
                    NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });*/
                    NewsGrid.RowSpacing = 0;

                    NewsGrid.Children.Add(Box.Frame, 1, 2, Rownr + 1, Rownr + 4); //Boxview
                    NewsGrid.Children.Add(Box.Box, 1, 2, Rownr + 1, Rownr + 4); //Boxview
                    NewsGrid.Children.Add(Box.Image, 1, 2, Rownr + 1, Rownr + 4); //Image
                    NewsGrid.Children.Add(Box.Label, 1, 2, Rownr + 1, Rownr + 4); //Label
                    // NewsGrid.Children.Add(Box.NrLabel, 0, 1, Rownr + 1, Rownr + 2);

                    if (Box.Plus)
                    {
                        NewsGrid.Children.Add(Box.CornerImage, 2, 3, Rownr + 1, Rownr + 4); //CornerImage
                        NewsGrid.Children.Add(Box.PlusImage, 2, 3, Rownr + 1, Rownr + 4); //PlusImage
                    }

                    /*int temp = 0;
                    temp = NewsGrid.Children.IndexOf(Box.ArticleMargin);
                    NewsGrid.Children[temp].HeightRequest = 1;
                    NewsGrid.Children[temp].WidthRequest = 380;

                    Box.ArticleMargin.HorizontalOptions = LayoutOptions.Center;*/
                }

                

                Rownr++;
                Rownr++;
                Rownr++;
                Rownr++;
            }
            NewsGrid.Children.Add(Down, 0,3, Rownr + 1,Rownr +2);





            if (App.Instanciated)
            {
                int i = 0;
                foreach (var Article in ArticleList)
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
            if(argc == 0)
            {
                Rss = App.database.GetNF(Loadnr);
            }
            else if(argc == 1)
            {
                Down.IsVisible = false;
                int j = 0;
                var RAL = App.database.GetHistory(App.LoggedinUser.ID);
                Console.WriteLine("History Gotten: " + RAL.Count());
                foreach(var RA in RAL)
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
                    Box.NrLabel.Text = i.ToString();
                    Console.WriteLine("ArtikelObjekt Skapat");
                    ArticleList.Add(Box);
                }
                i++;
            }
            PrintNews();
        }

        public void Scrollup(object sender, EventArgs e)
        {

            if (argc == 0)
            {

                NEXT = CURR;
                CURR = PREV;
                PREV -= DBLN;
                if(PREV <= 0)
                {
                    PREV = 0;
                    CURR = DBLN;
                    NEXT = DBLN * 2;
                }



                Console.WriteLine("PREV: " + PREV + " CURR: " + CURR + " NEXT: " + NEXT);

                Device.BeginInvokeOnMainThread(() =>
                {
                    LoadLocalDB();
                    AddNews(argc);
                    NewsSV.ScrollToAsync(0, NewsSV.ContentSize.Height - 10, false);
                });
                GC.Collect();

            }
        }

        public void Scrolldown(object sender, EventArgs e)
        {

            if (argc == 0)
            {
                PREV = CURR;
                CURR = NEXT;
                NEXT += DBLN;

                Console.WriteLine("PREV: " + PREV + " CURR: " + CURR + " NEXT: " + NEXT);

                Device.BeginInvokeOnMainThread(() =>
                {
                    LoadLocalDB();
                    AddNews(argc);
                    NewsSV.ScrollToAsync(0, 10, false);
                });
                GC.Collect();

            }
        }
    }
}