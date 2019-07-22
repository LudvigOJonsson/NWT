using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace NWT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GSampleNewsPage : ContentPage
    {

        public int red = (int)App.MC.R;
        public int green = (int)App.MC.G;
        public int blue = (int)App.MC.B;
        public System.Timers.Timer Timer;
        public static int ArticleNR;
        public int CC = 8;
        public bool Read = false;
        public int Row = 7;
        public bool Topimg = true;
        public bool Favorited = false;

        public GSampleNewsPage()
        {
            InitializeComponent();

         


            LoadNews();


        }



        void LoadNews()
        {
            
            Rubrik.Text = "Nu måste Kommunismen komma på hur man får fram lite pengar.";
            //Dot.Text = "⚫";
            Ingress.Text = "";
            Top.Text = "Publicerad: " + DateTime.Now;
            Author.Text = "För Fattare";
            Category.Text = "-Det Samlade Verken";


 
          

            ArticleImage.Source = "https://i.gyazo.com/2a102b448a1261f6e7fe6dc2b5c867d8.png";


            var Order = new List<int>
            {
                0,
                1,
                0,
                1,
                0,
                1,
                0,
                0
            };
            var Text = new List<string>
            {
                "Daily Reminder that Alaskan snakes fears groups of lowpoly FAMASes because their military supervisiors doesnt give them a fucking .45",
                "We're limited by the meme-technology of our time",
                "Nej, brödet finns alltid med, annars är det ju bara en hambörgare",
                "A person (usually a man) who does not cook, but likes to stir what you're cooking. The spoonster wants to feel part of the process. Spoonsters stir for an average of 23 seconds before they get bored or complain about back pain. ",
                "You know what they say: The dog's in the cards."
            };
            var Images = new List<string>
            {
                "https://i.gyazo.com/2e5c8c70a654cf4d8736a946fb021f66.png",
                "https://i.gyazo.com/f495d95a612f1ebfa0128b3850b89aaa.jpg",
                "https://i.gyazo.com/a5faf7c93be616e02c519a762c9bbb11.jpg"
            };
            var ImageText = new List<string>
            {
                "Thanks, I love being stabbed, it's my favourite pastime.",
                "You know i need my Payday.... 2...",
                "Remember that its faster to switch to your secondary than it is to reload"
            };

            int Count = 0;
            int TextCount = 0;
            int ImageCount = 0;




            foreach (int Type in Order)
            {


                if (Type == 0)
                {
                    ArticleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });



                    var Label = new Label
                    {
                        Text = Text[TextCount],
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Start,
                        FontSize = 14,
                        TextColor = Color.Black,
                        Margin = new Thickness(0, 0, 10, 10)
                    };
                    ArticleGrid.Children.Add(Label, 1, 5, Row, Row + 1);
                    Row++;
                    Count++;
                    TextCount++;
                }
                else if (Type == 1)
                {
                    var IMGText = new Label
                    {
                        Text = ImageText[ImageCount],
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Start,
                        FontSize = 12,
                        TextColor = Color.Gray,
                        Margin = new Thickness(0, 0, 10, 10)
                    };

                    if (Topimg == true)
                    {
                        Image img = new Image { Source = Images[ImageCount] };


                        ArticleImage.Source = Images[ImageCount];
                        ArticleImage.HeightRequest = img.Height;
                        ArticleImage.WidthRequest = 300;
                        ArticleGrid.Children.Add(IMGText, 1, 5, 5, 6);
                        Topimg = false;
                    }
                    else
                    {

                        var Image = new Image
                        {
                            WidthRequest = 200,
                            HeightRequest = 300,
                            Aspect = Aspect.AspectFill,
                            Margin = 5,
                        };

                        ArticleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        ArticleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        ArticleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        Image.Source = Images[ImageCount];
                        ArticleGrid.Children.Add(Image, 0, 6, Row, Row + 1);
                        ArticleGrid.Children.Add(IMGText, 1, 5, Row + 1, Row + 2);
                        Row++;
                        Row++;
                        Row++;
                    }
                    Count++;
                    ImageCount++;
                }
            }
            string[] Categories = new string[]
            {
                "Kategori1",
                "Kategori2",
                "Kategori3"
            };
            string[] Tags = new string[]
            {
                "Tag1",
                "Tag2",
                "Tag3"
            };
            int TagRow = 0;

            foreach (String Category in Categories)
            {
                var Box = new Button
                {
                    CornerRadius = 10,
                    Margin = 2,
                    BackgroundColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    ClassId = Category,

                };
                

                var Comment = new Label
                {
                    Text = Category,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    TextColor = Color.Black,
                    FontSize = 16,
                    WidthRequest = 290,
                    Margin = 20,
                    InputTransparent = true,
                };
                TagGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                TagGrid.Children.Add(Box, 0, 6, TagRow, TagRow + 1);
                TagGrid.Children.Add(Comment, 0, 6, TagRow, TagRow + 1);
                TagRow++;

                if (App.SideMenu.Categories.Contains(Category))
                {
                    Box.IsEnabled = false;
                }


            }



            foreach (String Tag in Tags)
            {
                var Box = new Button
                {
                    CornerRadius = 10,
                    Margin = 2,
                    BackgroundColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    ClassId = Tag
                };
                
                var Comment = new Label
                {
                    Text = Tag,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    TextColor = Color.Black,
                    FontSize = 16,
                    WidthRequest = 290,
                    Margin = 20,
                    InputTransparent = true,
                };
                TagGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                TagGrid.Children.Add(Box, 0, 6, TagRow, TagRow + 1);
                TagGrid.Children.Add(Comment, 0, 6, TagRow, TagRow + 1);
                TagRow++;


                if (App.SideMenu.Tags.Contains(Tag))
                {
                    Box.IsEnabled = false;
                }
            }



            ArticleGrid.Children.Add(BG, 0, 6, 0, Row);
            ArticleGrid.Children.Add(BackGround, 0, 6, Row + 1, Row + 4);
            ArticleGrid.Children.Add(TimerButton, 1, 4, Row + 1, Row + 2);
            ArticleGrid.Children.Add(TimerIcon, 2, Row + 1);
            //ArticleGrid.Children.Add(tokenAnimation, 2, Row + 1);
            //ArticleGrid.Children.Add(FavButton, 5, 6, Row + 1, Row + 2);
            ArticleGrid.Children.Add(FavIcon, 4, Row + 1);
            ArticleGrid.Children.Add(TagGrid, 0, 6, Row + 2, Row + 3);
            ArticleGrid.Children.Add(Comment, 0, 6, Row + 3, Row + 4);
            ArticleGrid.Children.Add(CommentButton, 0, 6, Row + 3, Row + 4);
            ArticleGrid.Children.Add(CommentGrid, 0, 6, Row + 4, Row + 5);






          

        }




       

        
        

        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }

    }
}