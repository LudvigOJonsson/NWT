using FFImageLoading.Forms;
using Newtonsoft.Json;
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
    public partial class CustomNewsFeed : ContentPage
    {
        public int Loadnr = 1;
        public static int DBLN = 20;
        public int Rownr = 1;
        public static TapGestureRecognizer TGR;
        public List<Article> ArticleList = new List<Article>();
        public List<Article> ArticlePrintList = new List<Article>();
        public static string Defaultimage = "http://media2.hitzfm.nu/2016/11/Nyheter_3472x1074.jpg";
        public static Random rnd = new Random();
        public List<string> Filter = new List<string>();
        public List<string> Author = new List<string>();
        public List<string> Tag = new List<string>();
        public ListView ArticleListView = new ListView();
        public int PREV = 0;
        public int CURR = DBLN;
        public int NEXT = DBLN * 2;
        public bool ScrollLock = false;
        public bool TagsModified = false;
        

        public bool First = true;
        public int argc = 0;

        public int interval = 0;

        public class Article
        {
            public long ID { get; set; }
            public string Source { get; set; }
            public Color CategoryColor { get; set; }
            public string FirstTag { get; set; }
            public string Tag { get; set; }
            public bool TagVisible { get; set; }
            public int TagLength { get; set; }
            public string Header { get; set; }
            public string IMGSource { get; set; }
            public int HeaderLength { get; set; }
            public bool Plus { get; set; }
            public bool Full { get; set; }
            public bool AdVisibility { get; set; }
            public string AdText { get; set; }
            public string AdSource { get; set; }

            public int IHR { get; set; }
            public int BHR { get; set; }
            public int CBWR { get; set; }
            public int CBHR { get; set; }
            public LayoutOptions CBHO { get; set; }
            public LayoutOptions CBVO { get; set; }
            public Article(NewsfeedTable NF, int interval)
            {
                if (NF.Category.Contains("Sport"))
                {
                    CategoryColor = Color.FromHex("#6fb110");
                }
                else if (NF.Category.Contains("Ekonomi"))
                {
                    CategoryColor = Color.FromHex("#3b5e6a");
                }
                else if (NF.Category.Contains("Åsikter"))
                {
                    CategoryColor = Color.FromHex("#57bcbf");
                }
                else if(NF.Category.Contains("Nöje/Kultur"))
                {
                    CategoryColor = Color.FromHex("#bb0066");
                }
                else if (NF.Category.Contains("Familj"))
                {
                    CategoryColor = Color.FromHex("#e0d8b3");
                }
                else
                {
                    CategoryColor = Color.FromHex("#3b5e6a");
                }

                Tag = "";
                FirstTag = "";
                if (App.LoggedinUser != null)
                {
                    var TagList = JsonConvert.DeserializeObject<List<List<string>>>(App.LoggedinUser.TaggString);
                    //var UserCategories = TagList[0];
                    var UserTags = TagList[1];
                    //var UserAuthors = TagList[2];

                
                    var Firsttag = true;

                    foreach (var Tag_ in UserTags)
                    {
                        if (NF.Tag.Contains(Tag_))
                        {
                            if (Firsttag)
                            {
                                FirstTag = Tag_;
                                Firsttag = false;
                            }
                            else
                            {
                                Tag += ", ";
                            }
                            Tag += Tag_;
                            
                        }

                    }

                    if (Firsttag)
                    {
                        TagVisible = false;
                    }
                    else
                    {
                        TagVisible = true;
                    }
                }

                TagLength = (FirstTag.Length*7);



                ID = NF.Article;
                Header = NF.Header;
                IMGSource = NF.Image;
                Full = true;
                

                Plus = Convert.ToBoolean(NF.Plus);

                int BL = 50;
                int BH = 30;

                if (Header.Length < BL)
                {
                    HeaderLength = BH;
                }
                else if (Header.Length < BL * 2)
                {
                    HeaderLength = BH * 2;
                }
                else if (Header.Length < BL * 3)
                {
                    HeaderLength = BH * 3;
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
                    IHR = 220;
                    BHR = 200 + HeaderLength;
                    CBWR = 200;
                    CBHR = 7;
                    CBHO = LayoutOptions.Fill;
                    CBVO = LayoutOptions.StartAndExpand;
                }


                if (interval % 5 == 0 && Full)
                {
                    AdVisibility = true;

                    //Randomizing the content of ads and social media posts
                    
                    int content = rnd.Next(1, 4);  // creates a number between 1 and 20

                    if (content == 1)
                    {
                        //a 1 in 3 chance to become an ad

                        AdText = "Reklam";
                        AdSource = "Commercial_Hamburger.jpg";

                    }
                    else if (content == 2)
                    {
                        //a 1 in 3 chance to become a social media post

                        AdText = "Användarbilder";
                        AdSource = "Commercial_Social.png";

                    }
                    else if (content == 3)
                    {
                        //a 1 in 3 chance to become an event

                        AdText = "Evenemang";
                        AdSource = "Commercial_HollidaySpecial.jpg";

                    }
                    else
                    {
                        //else there is no extra post
                        AdVisibility = false;
                    }

                } else
                {
                    AdVisibility = false;
                }

                

                

                Console.WriteLine("Artikel Klar");
            }







        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.LoggedinUser != null)
            {
                Console.WriteLine("OnAppearing");
                if (TagsModified)
                {
                    Console.WriteLine("Tags modifing, reloading feed.");
                    TagUpdate();
                    TagsModified = false;
                }
            }           
        }

        public CustomNewsFeed()
        {
            InitializeComponent();
            TGR = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1
            };


            Console.WriteLine("NewsGrid");
            TGR.Tapped += (s, e) => {
                IsEnabled = false;
                LoadNews(s, e);
                IsEnabled = true;
            };
        }

        public void CreateFeed()
        {

            LoadLocalDB();

            AddNews();
        }




        public void ArtikelClicked(object sender, EventArgs e)
        {
            var Header = (View)sender;
            Console.WriteLine(Header.ClassId);
            Console.WriteLine(App.Mainpage.Children[0].Navigation.NavigationStack[1].GetType());
            App.Mainpage.Children[0].Navigation.NavigationStack[1].ClassId = Header.ClassId;

            Navigation.PopAsync();
        }
        /*async public void AdClicked(object sender, EventArgs e)
        {
            await DisplayAlert("WIP", "Work in progress.", "Okej");
        }*/

        public async void TagUpdate()
        {
            App.database.LocalExecute("DELETE FROM CNF");

            PREV = 0;
            CURR = NewsGridPage.DBLN;
            NEXT = NewsGridPage.DBLN * 2;
            Loadnr = 1;
            var TagList = JsonConvert.DeserializeObject<List<List<string>>>(App.LoggedinUser.TaggString);
            Console.WriteLine("CNF Taggupdate, Deserializing Taglist.");

            if(TagList != null)
            {
                Filter = TagList[0];
                Tag = TagList[1];
                Author = TagList[2];
                ArticleList.Clear();
                //Console.WriteLine("CNF Taggupdate, Starting Task. Filter: " + Filter.First());
                await System.Threading.Tasks.Task.Run(async () =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        //ArticleListView.IsRefreshing = true;
                        App.LS.loadingAnimation.Play();
                        await Navigation.PushAsync(App.LS);

                        App.LS.LoadingText.Text = "Uppdaterar Nyhetsflödet för nya taggar.";

                    });

                    Console.WriteLine("CNF Taggupdate, Popup Made.");
                    LoadLocalDB();
                    Console.WriteLine("CNF Taggupdate, Database Loaded.");
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        AddNews();

                        Console.WriteLine("CNF Taggupdate, Listview Skapad.");
                        ArticleListView.ItemsSource = null;
                        ArticleListView.ItemsSource = ArticleList;

                        Console.WriteLine("CNF Taggupdate, ArticleList Refreshad.");

                    });


                    GC.Collect();


                    await System.Threading.Tasks.Task.Delay(1000);
                    Console.WriteLine("CNF Taggupdate, Uhhhhh.");
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Console.WriteLine("Initiering Klar");

                        await Navigation.PopAsync();
                        //App.Mainpage.CurrentPage = App.Mainpage.Children[1];
                        //await NewsSV.ScrollToAsync(0, ArticleListView.Height - 10, false);
                        //ArticleListView.IsRefreshing = false;

                    });

                });
            }
            else
            {

                

            }


        }




        async void LoadNews(object sender, EventArgs e)
        {

            var Header = (Button)sender;
            Header.IsEnabled = false;
            var id = Int32.Parse(Header.ClassId);
            var RSSTable = App.database.GetServerRSS(id);
            if (RSSTable != null)
            {
                RSSTable RSS = RSSTable.First();
                await Navigation.PushAsync(new NewsPage(RSS, argc));
            }
            else
            {
                await DisplayAlert("Article Load Failure", "Artikeln misslyckades att laddas in, vänligen försök igen.", "OK");
            }

            Header.IsEnabled = true;

        }



        public void LoadLocalDB()
        {
            App.database.LoadCNF(Loadnr, (Loadnr + DBLN), Filter, Author, Tag);
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
            
            

            Console.WriteLine("Grid Rensat.");


            ArticleListView = new ListView
            {
                // Source of data items.

                ItemsSource = ArticlePrintList,
                HasUnevenRows = true,
                
                SeparatorVisibility = SeparatorVisibility.None,


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
                        FontSize = 18,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,

                        TextColor = Color.Black,
                        //ClassId = NF.Article.ToString(),
                        InputTransparent = true,
                        Margin = new Thickness(15, 5, 15, 0),
                    };

                    Label.SetBinding(HeightRequestProperty, "HeaderLength");

                    var Image = new CachedImage()
                    {


                        //Source = NF.Image,
                        WidthRequest = IMGXC,
                        //HeightRequest = IMGYC,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Aspect = Aspect.AspectFill,
                        InputTransparent = true,
                        CacheDuration = TimeSpan.FromDays(14),
                        DownsampleToViewSize = true,
                        RetryCount = 1,
                        RetryDelay = 250,
                        BitmapOptimizations = false,
                        LoadingPlaceholder = "",
                        ErrorPlaceholder = "failed_load.png",
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
                    Image AdShadow = new Image
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        InputTransparent = true,
                        Source = "shadow.png"
                    };
                    BoxView AdArticleMargin = new BoxView
                    {
                        Color = Color.FromHex("#f2f2f2"),
                        WidthRequest = IMGXC,
                        HeightRequest = 10,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        InputTransparent = true,
                        //ClassId = NF.Article.ToString()
                    };

                    Label Tag = new Label
                    {
                        //Text = NF.Header,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.End,
                        FontSize = 12,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.End,

                        TextColor = Color.White,
                        //ClassId = NF.Article.ToString(),
                        InputTransparent = true,
                        Margin = new Thickness(15, 5, 15, 5),
                    };

                    Button TagBox = new Button
                    {


                        BackgroundColor = App.MC,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.End,
                        HeightRequest = 16,
                        //InputTransparent = true,
                        Margin = new Thickness(13, 5, 13, 5),
                    };

                    TagBox.Clicked += TagPopup;
                    //Label.GestureRecognizers.Add(TGR);
                    //Image.GestureRecognizers.Add(TGR);

                    var AdImage = new CachedImage
                    {

                        BackgroundColor = Color.White,
                        //Source = NF.Image,
                        WidthRequest = IMGXC,
                        //HeightRequest = IMGYC,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        Aspect = Aspect.AspectFill,
                        InputTransparent = true,
                        Margin = 50,
                        CacheDuration = TimeSpan.FromDays(14),
                        DownsampleToViewSize = true,
                        RetryCount = 1,
                        RetryDelay = 250,
                        BitmapOptimizations = false,
                        LoadingPlaceholder = "",
                        ErrorPlaceholder = "failed_load.png",
                        // ClassId = NF.Article.ToString()
                    };
                    BoxView AdImageOutline = new BoxView
                    {
                        BackgroundColor = Color.White,
                        WidthRequest = IMGXC,
                        //HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        //ClassId = NF.Article.ToString(),
                        Margin = 35,

                    };

                    AdImage.SetBinding(HeightRequestProperty, "IHR");

                    BoxView AdBox = new BoxView
                    {
                        BackgroundColor = Color.FromHex("#fffef2"),
                        WidthRequest = IMGXC,
                        //HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        //ClassId = NF.Article.ToString(),
                        Margin = 0

                    };
                    /*Button AdButton = new Button
                    {
                        BackgroundColor = Color.Transparent,
                        WidthRequest = IMGXC,
                        //HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        //ClassId = NF.Article.ToString(),
                        Margin = 0

                    };
                    AdButton.Clicked += AdClicked;*/

                    Label AdLabel = new Label
                    {
                        Text = "Reklam",
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 12,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.End,

                        TextColor = Color.LightGray,
                        //ClassId = NF.Article.ToString(),
                        InputTransparent = true,
                        Margin = new Thickness(5, 5, 5, 5),
                    };


                    AdBox.SetBinding(HeightRequestProperty, "BHR");

                    CategoryBox.SetBinding(HeightRequestProperty, "CBHR");
                    CategoryBox.SetBinding(WidthRequestProperty, "CBWR");
                    CategoryBox.SetBinding(BoxView.VerticalOptionsProperty, "CBVO");
                    CategoryBox.SetBinding(BoxView.HorizontalOptionsProperty, "CBHO");
                    CategoryBox.SetBinding(BoxView.BackgroundColorProperty, "CategoryColor");

                    Label.SetBinding(Label.TextProperty, "Header");
                    Image.SetBinding(CachedImage.SourceProperty, "IMGSource");

                    Tag.SetBinding(Label.TextProperty, "FirstTag");
                    Tag.SetBinding(Label.IsVisibleProperty, "TagVisible");

                    TagBox.SetBinding(Button.IsVisibleProperty, "TagVisible");
                    TagBox.SetBinding(Button.WidthRequestProperty, "TagLength");
                    TagBox.SetBinding(Button.ClassIdProperty, "Tag");

                    Label.SetBinding(Label.ClassIdProperty, "ID");
                    Image.SetBinding(CachedImage.ClassIdProperty, "ID");

                    Box.SetBinding(Button.ClassIdProperty, "ID");
                    
                    ArticleMargin.SetBinding(BoxView.ClassIdProperty, "ID");

                    AdArticleMargin.SetBinding(BoxView.IsVisibleProperty, "AdVisibility");
                    AdShadow.SetBinding(BoxView.IsVisibleProperty, "AdVisibility");
                    AdBox.SetBinding(BoxView.IsVisibleProperty, "AdVisibility");
                    //AdButton.SetBinding(BoxView.IsVisibleProperty, "AdVisibility");
                    AdImageOutline.SetBinding(BoxView.IsVisibleProperty, "AdVisibility");
                    AdImage.SetBinding(CachedImage.IsVisibleProperty, "AdVisibility");
                    AdLabel.SetBinding(Label.IsVisibleProperty, "AdVisibility");
                    AdLabel.SetBinding(Label.TextProperty, "AdText");
                    AdImage.SetBinding(CachedImage.SourceProperty, "AdSource");



                    var Grid = new Grid
                    {

                        RowDefinitions = {
                    new RowDefinition { Height = 0 },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = 10 },
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

                    Grid.Children.Add(TagBox, 1, 2, 1, 2); //Tag
                    Grid.Children.Add(Tag, 1, 2, 1, 2); //Tag   


                    //Grid.Children.Add(AdArticleMargin, 1, 2, 4, 5); //Boxview
                    Grid.Children.Add(AdBox, 1, 2, 5, 6); //Boxview
                    //Grid.Children.Add(AdButton, 1, 2, 5, 6); //Boxview
                    Grid.Children.Add(AdImage, 1, 2, 5, 6); //Boxview
                    Grid.Children.Add(AdLabel, 1, 2, 5, 6); //Label
                    Grid.Children.Add(AdShadow, 1, 2, 6, 7);

                    /*Grid.Children.Add(AdBox, 1, 2, 4, 5); //Boxview
                    Grid.Children.Add(AdImageOutline, 1, 2, 4, 5); //Image 
                    Grid.Children.Add(AdImage, 1, 2, 4, 5); //Image     
                    Grid.Children.Add(AdLabel, 1, 2, 4, 5); //Label   

                    Grid.Children.Add(AdShadow, 0, 3, 5, 6); //ShadowBelowAd*/


                    Console.WriteLine("Utdata: " + Label.Text);



                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = Grid
                    };
                })

            };





            NewsGrid.Children.Add(ArticleListView, 0, 3, 1, 2);
            NewsGrid.Children.Add(Down, 0, 3, 2, 3);
            First = false;
        }

        async void TagPopup(object sender, EventArgs e)
        {
            var Header = (Button)sender;
            Header.IsEnabled = false;
            String tagstring = Header.ClassId;

            await DisplayAlert("Tags", "Tags in this Article that you Follow: \n" + tagstring, "Okay");
        }

        private void Box_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void AddNews()
        {

            List<NewsfeedTable> Rss = App.database.GetCNF(Loadnr);
            
            Console.WriteLine(Rss.Count);
            Console.WriteLine("Inladdning Klar");
            int i = 1;

            foreach (NewsfeedTable NF in Rss)
            {
                bool Exists = false;
                App.Online = true;
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
                    var Box = new Article(NF, interval);

                    interval++;

                    Console.WriteLine("ArtikelObjekt Skapat");
                    ArticleList.Add(Box);
                }
                i++;
            }


            ArticlePrintList.Clear();
            for (int j = PREV; j < CURR; j++)
            {
                ArticlePrintList.Add(ArticleList[j]);
            }
            if(First && App.Online)
            {
                PrintNews();
            }
            
        }


        public async void ListViewScroll(object sender, EventArgs e)
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //IsBusy = true;
                    //ArticleListView.IsRefreshing = true;
                    App.LS.loadingAnimation.Play();
                    await Navigation.PushAsync(App.LS);
                    App.LS.LoadingText.Text = "Laddar in mera artiklar.";

                });

                if (argc == 0)
                {
                    PREV = 0;
                    CURR = NEXT;
                    NEXT += DBLN;

                    Console.WriteLine("PREV: " + PREV + " CURR: " + CURR + " NEXT: " + NEXT);

                    double height = NewsSV.ContentSize.Height - 10;

                    LoadLocalDB();
                    





                }
                Device.BeginInvokeOnMainThread( () =>
                {
                    AddNews();
                    ArticleListView.ItemsSource = null;
                    ArticleListView.ItemsSource = ArticleList;



                });


                GC.Collect();


                await System.Threading.Tasks.Task.Delay(1000);
                Device.BeginInvokeOnMainThread(async () =>
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