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
    public partial class GSampleNewsGrid : ContentPage
    {
        
        public static int DBLN = 20; // Antal artiklar som laddas in
        public List<Article> ArticlePrintList = new List<Article>();
        public static string Defaultimage = "https://i.gyazo.com/c2611a5a601ebff05e9e84f0be555900.png";

        public class Article
        {
            public long ID { get; set; }
            public string Header { get; set; }
            public string IMGSource { get; set; }
            public int HeaderHeight { get; set; }
            public bool Full { get; set; }
            public int IHR { get; set; }
            public int BHR { get; set; }
            public int CBWR { get; set; }
            public int CBHR { get; set; }
            public LayoutOptions CBHO { get; set; }
            public LayoutOptions CBVO { get; set; }
            public Article(int i)
            {
                
                ID = -1;
                Header = "En MCFisker, En NiP med DiP och en Cola ILight.";
                IMGSource = Defaultimage;
                Full = true;

                int BL = 33; // Box Length
                int BH = 35; // Box Height

                if (Header.Length < BL)
                {
                    HeaderHeight = BH;
                }
                else if (Header.Length < BL * 2)
                {
                    HeaderHeight = BH * 2;
                }
                else if (Header.Length < BL * 3)
                {
                    HeaderHeight = BH * 3;
                }

                if (i % 2 == 0)
                {// Small
                    IMGSource = "";
                    IHR = 1;  //Image Height Request
                    CBWR = 7; //Category Box Width Request
                    CBHR = HeaderHeight; // Category Box Height Request
                    BHR = HeaderHeight; // Box Height Request
                    CBHO = LayoutOptions.Start; //Category Box Horizontal Options
                    CBVO = LayoutOptions.FillAndExpand; //Category Box Horizontal Options
                    Full = false;
                }
                else
                {// Big
                    IHR = 200;
                    BHR = 200 + HeaderHeight;
                    CBWR = 200;
                    CBHR = 7;
                    CBHO = LayoutOptions.FillAndExpand;
                    CBVO = LayoutOptions.Start;
                }

                Console.WriteLine("Artikel Klar");
            }
        }

        public GSampleNewsGrid()
        {
            InitializeComponent();
            CreateFeed();          
        }

        public void CreateFeed()
        {
            for(int i = 0; i < DBLN; i++)
            {
                ArticlePrintList.Add(new Article(i));
            }

            PrintNews();
        }

        public void PrintNews()
        {

            NewsGrid.Children.Clear();

            ListView listView = new ListView
            {
                // Source of data items.

                ItemsSource = ArticlePrintList,
                HasUnevenRows = true,
                SeparatorVisibility = SeparatorVisibility.None,
                BackgroundColor = Color.FromHex("#f2f2f2"),

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.

                    int IMGXC = 200; // Default Width 
                    Label Header = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Start,
                        FontSize = 25,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        TextColor = Color.Black,
                        InputTransparent = true,
                        Margin = new Thickness(15, 5, 15, 0),
                    };

                    Header.SetBinding(HeightRequestProperty, "HeaderHeight");

                    Image Image = new Image
                    {
                        WidthRequest = IMGXC,      
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Aspect = Aspect.AspectFill,
                        InputTransparent = true,
                    };

                    Image.SetBinding(HeightRequestProperty, "IHR");

                    Button Box = new Button
                    {
                        BackgroundColor = Color.White,
                        WidthRequest = IMGXC,
    
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        BorderColor = Color.FromHex("#f0f0f0"),


                    };

                    Box.SetBinding(HeightRequestProperty, "BHR");
                    Box.Clicked += LoadNews;
                   
                    BoxView ArticleMargin = new BoxView
                    {
                        Color = Color.FromHex("#f2f2f2"),
                        WidthRequest = IMGXC,
                        HeightRequest = 20,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        InputTransparent = true,
                    };

                    BoxView CategoryBox = new BoxView
                    {

                        BackgroundColor = App.MC,
                        InputTransparent = true,

                    };

                    Image Shadow = new Image
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Fill,
                        InputTransparent = true,
                        Source = "shadow.png"
                    };

                    CategoryBox.SetBinding(HeightRequestProperty, "CBHR");
                    CategoryBox.SetBinding(WidthRequestProperty, "CBWR");
                    CategoryBox.SetBinding(BoxView.VerticalOptionsProperty, "CBVO");
                    CategoryBox.SetBinding(BoxView.HorizontalOptionsProperty, "CBHO");

                    Header.SetBinding(Label.TextProperty, "Header");
                    Image.SetBinding(Image.SourceProperty, "IMGSource");


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

                    Header.WidthRequest = Header.Width - 25;

                    Grid.Children.Add(ArticleMargin, 1, 2, 0, 1); //Boxview
                    Grid.Children.Add(Box, 1, 2, 1, 3); //Boxview
                    Grid.Children.Add(Image, 1, 2, 1, 2); //Image   
                    Grid.Children.Add(CategoryBox, 1, 2, 2, 3); //Label
                    Grid.Children.Add(Header, 1, 2, 2, 3); //Label
                    Grid.Children.Add(Shadow, 1, 2, 3, 4);

                    Console.WriteLine("Utdata: " + Header.Text);

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = Grid
                    };
                })

            };

            NewsGrid.Children.Add(listView, 0, 3, 1, 2);

        }

        async void LoadNews(object sender, EventArgs e)
        {

            var Header = (Button)sender;
            Header.IsEnabled = false;
 
            await Navigation.PushAsync(new GSampleNewsPage());

            Header.IsEnabled = true;

        }
    }
}