using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NWT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesPage : ContentPage
    {

        public Button Box = new Button { };
        public Label Label = new Label { };
        public Image Image = new Image { };

        public FavoritesPage()
        {
            InitializeComponent();
        }

        public void TempCreateFavoriteBox()
        {

            int IMGXC = 200;
            int IMGYC = 300;


            Label = new Label
            {
                Text = "19-01-03" + " Name of insändare" + "read",
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                WidthRequest = IMGXC,
                //HeightRequest = ((RSS.Title.Length / 30)) * 50,
                TextColor = Color.Black,
                //ClassId = RSS.ID.ToString(),
                Margin = 10,
            };

            //Label.GestureRecognizers.Add();

            Box = new Button
            {
                BackgroundColor = Color.White,
                WidthRequest = IMGXC,
                HeightRequest = Label.HeightRequest,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End,
                //ClassId = RSS.ID.ToString(),
                Margin = 0,
                BorderWidth = 2,
                BorderColor = Color.Black,
            };

            //Box.GestureRecognizers.Add();

            Image = new Image
            {

                //Source = RSS.ImgSource,
                WidthRequest = IMGXC / 2,
                HeightRequest = IMGYC / 2,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Aspect = Aspect.AspectFit,
                Margin = 25,
                //ClassId = RSS.ID.ToString()

            };
            //Image.GestureRecognizers.Add();
        }
        public void AddBox()
        {
            //Basic code for adding the boxes as children to the newsgrid.

            /*NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
            NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 120 });
            NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
            NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
            NewsGrid.RowSpacing = 0;
            
            NewsGrid.Children.Add(Box.Box, 0, 3, Rownr, Rownr + 3); //Boxview
            NewsGrid.Children.Add(Box.Image, 2, 3, Rownr, Rownr + 3); //Image
            NewsGrid.Children.Add(Box.Label, 0, 2, Rownr, Rownr + 3); //Label

            Rownr++;
            Rownr++;
            Rownr++;
            Rownr++;*/
        }
    }
}