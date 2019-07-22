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

        public int PREV = 0;
        public int CURR = DBLN;
        public int NEXT = DBLN * 2;
        public bool ScrollLock = false;


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

                TagLength = FirstTag.Length*7;



                ID = NF.Article;
                Header = NF.Header;
                IMGSource = NF.Image;
                Full = true;
                

                Plus = Convert.ToBoolean(NF.Plus);

                int BL = 33;
                int BH = 35;

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


                if (interval % 5 == 0)
                {
                    AdVisibility = true;

                    //Randomizing the content of ads and social media posts
                    
                    int content = rnd.Next(1, 4);  // creates a number between 1 and 20

                    if (content == 1)
                    {
                        //a 1 in 3 chance to become an ad
                        AdText = "REKLAM";
                    }
                    else if (content == 2)
                    {
                        //a 1 in 3 chance to become a social media post
                        AdText = "SOCIALA MEDIER";
                    }
                    else if (content == 3)
                    {
                        //a 1 in 3 chance to become an event
                        AdText = "EVENEMANG";
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

        public void TagUpdate()
        {

            
            
            App.database.LocalExecute("DELETE FROM CNF");

            PREV = 0;
            CURR = NewsGridPage.DBLN;
            NEXT = NewsGridPage.DBLN * 2;
            Loadnr = 1;
            var TagList = JsonConvert.DeserializeObject<List<List<string>>>(App.LoggedinUser.TaggString);
            Filter = TagList[0];
            Tag = TagList[1];
            Author = TagList[2];


            ArticleList.Clear();
            LoadLocalDB();
            AddNews();
            

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

                    Label.SetBinding(HeightRequestProperty, "HeaderLength");

                    Image Image = new Image
                    {

                        BackgroundColor = Color.White,
                        //Source = NF.Image,
                        WidthRequest = IMGXC,
                        //HeightRequest = IMGYC,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        Aspect = Aspect.AspectFill,
                        InputTransparent = true,
                        Margin = 0
                        // ClassId = NF.Article.ToString()


                    };



                    Image.SetBinding(HeightRequestProperty, "IHR");

                    Button Box = new Button
                    {
                        BackgroundColor = Color.White,
                        WidthRequest = IMGXC,
                        //HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        //ClassId = NF.Article.ToString(),
                        BorderColor = Color.FromHex("#f0f0f0"),
                        Margin = 0

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
                        Color = Color.White,
                        WidthRequest = IMGXC,
                        HeightRequest = 20,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        InputTransparent = true,
                        //ClassId = NF.Article.ToString()
                    };

                    BoxView CategoryBox = new BoxView
                    {

                        
                        ///WidthRequest = Label.WidthRequest,
                        //HeightRequest = 3,
                        //HorizontalOptions =,
                        //VerticalOptions = LayoutOptions.Start,
                        InputTransparent = true,

                    };

                    Label Tag = new Label
                    {
                        //Text = NF.Header,
                        HorizontalTextAlignment = TextAlignment.End,
                        VerticalTextAlignment = TextAlignment.End,
                        FontSize = 12,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.End,

                        TextColor = Color.White,
                        //ClassId = NF.Article.ToString(),
                        InputTransparent = true,
                        Margin = new Thickness(15, 5, 15, 0),
                    };

                    Button TagBox = new Button
                    {

                        BackgroundColor = Color.Black,
                        VerticalOptions = LayoutOptions.End,
                        HorizontalOptions = LayoutOptions.End,
                        HeightRequest = 16,
                        //InputTransparent = true,
                        
                    };

                    TagBox.Clicked += TagPopup;
                    //Label.GestureRecognizers.Add(TGR);
                    //Image.GestureRecognizers.Add(TGR);

                    Image AdImage = new Image
                    {

                        BackgroundColor = Color.White,
                        //Source = NF.Image,
                        WidthRequest = IMGXC,
                        //HeightRequest = IMGYC,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        Aspect = Aspect.AspectFill,
                        InputTransparent = true,
                        Margin = 0
                        // ClassId = NF.Article.ToString()


                    };

                    AdImage.SetBinding(HeightRequestProperty, "IHR");

                    Button AdBox = new Button
                    {
                        BackgroundColor = Color.White,
                        WidthRequest = IMGXC,
                        Text = "",
                        //HeightRequest = Image.HeightRequest + Label.HeightRequest,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        //ClassId = NF.Article.ToString(),
                        BorderColor = Color.FromHex("#f0f0f0"),
                        Margin = 0

                    };

                    Label AdLabel = new Label
                    {
                        Text = "REKLAM",
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Start,
                        FontSize = 25,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,

                        TextColor = Color.Black,
                        //ClassId = NF.Article.ToString(),
                        InputTransparent = true,
                        Margin = new Thickness(15, 5, 15, 0),
                    };


                    AdBox.SetBinding(HeightRequestProperty, "BHR");

                    CategoryBox.SetBinding(HeightRequestProperty, "CBHR");
                    CategoryBox.SetBinding(WidthRequestProperty, "CBWR");
                    CategoryBox.SetBinding(BoxView.VerticalOptionsProperty, "CBVO");
                    CategoryBox.SetBinding(BoxView.HorizontalOptionsProperty, "CBHO");
                    CategoryBox.SetBinding(BoxView.BackgroundColorProperty, "CategoryColor");

                    Label.SetBinding(Label.TextProperty, "Header");
                    Image.SetBinding(Image.SourceProperty, "IMGSource");

                    Tag.SetBinding(Label.TextProperty, "FirstTag");
                    Tag.SetBinding(Label.IsVisibleProperty, "TagVisible");

                    TagBox.SetBinding(Button.IsVisibleProperty, "TagVisible");
                    TagBox.SetBinding(Button.WidthRequestProperty, "TagLength");
                    TagBox.SetBinding(Button.ClassIdProperty, "Tag");

                    Label.SetBinding(Label.ClassIdProperty, "ID");
                    Image.SetBinding(Image.ClassIdProperty, "ID");

                    Box.SetBinding(Button.ClassIdProperty, "ID");
                    
                    ArticleMargin.SetBinding(BoxView.ClassIdProperty, "ID");

                    AdBox.SetBinding(Button.IsVisibleProperty, "adVisibility");
                    AdImage.SetBinding(Image.IsVisibleProperty, "adVisibility");
                    AdLabel.SetBinding(Label.IsVisibleProperty, "adVisibility");
                    AdLabel.SetBinding(Label.TextProperty, "adText");



                    var Grid = new Grid
                    {
                        RowSpacing = 0,
                        RowDefinitions = {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },

                    },

                        ColumnDefinitions = {
                    new ColumnDefinition { Width = 1 },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = 1 },
                    },

                        //ColumnSpacing = 14,
                        BackgroundColor = Color.White



                    };





                    //Label.HeightRequest = ((Label.Text.Length / 30)) * 50;
                    Label.WidthRequest = Label.Width - 25;


                    Grid.Children.Add(ArticleMargin, 1, 2, 0, 1); //Boxview
                    Grid.Children.Add(Box, 1, 2, 1, 3); //Boxview
                    Grid.Children.Add(Image, 1, 2, 1, 2); //Image   
                     
                    Grid.Children.Add(CategoryBox, 1, 2, 2, 3); //Label
                    Grid.Children.Add(Label, 1, 2, 2, 3); //Label
                    Grid.Children.Add(TagBox, 1, 2, 2, 3); //Tag
                    Grid.Children.Add(Tag, 1, 2, 2, 3); //Tag   
                    
                    Grid.Children.Add(ArticleMargin, 1, 2, 3, 4); //Boxview
                    Grid.Children.Add(AdBox, 1, 2, 4, 5); //Boxview
                    Grid.Children.Add(AdImage, 1, 2, 4, 5); //Image   
                    Grid.Children.Add(AdLabel, 1, 2, 4, 5); //Image   



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

            PrintNews();
        }

        public async void Scrollup(object sender, EventArgs e)
        {
            IsBusy = true;

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
            AddNews();
            await NewsSV.ScrollToAsync(0, NewsSV.ContentSize.Height - 10, false);

            GC.Collect();

            
            IsBusy = false;
        }

        public async void Scrolldown(object sender, EventArgs e)
        {
            IsBusy = true;

            PREV = CURR;
            CURR = NEXT;
            NEXT += DBLN;

            Console.WriteLine("PREV: " + PREV + " CURR: " + CURR + " NEXT: " + NEXT);


            LoadLocalDB();
            AddNews();
            await NewsSV.ScrollToAsync(0, 10, false);

            GC.Collect();

            
            IsBusy = false;
        }
    }
}