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
	public partial class AchivementsPage : ContentPage
	{

        public Label Label = new Label { };
        public Image Image = new Image { };
        public Button Button = new Button { };

        public AchivementsPage()
		{
			InitializeComponent ();
        }
        private async void ShowPopup()
        {
            //Create `ContentPage` with padding and transparent background
            ContentPage loginPage = new ContentPage
            {
                BackgroundColor = Color.FromHex("#D9000000"),
                Padding = new Thickness(20, 20, 20, 20)
            };

            // Create Children
            Label = new Label
            {
                Text = "",
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.End,
                HeightRequest = 80,
                TextColor = Color.Black,
                FontSize = 25,
                Margin = 10
            };

            Image = new Image
            {
                Source = "https://cdn.discordapp.com/attachments/318470280622374912/478947159685988362/eipugIu.jpg",
                WidthRequest = 200,
                HeightRequest = 300,
                Aspect = Aspect.AspectFill
            };

            //Create desired layout to be a content of your popup page. 
            var contentLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children =
          {
              // Add children
              Image,
              Label
          }
            };

            //set popup page content:
            loginPage.Content = contentLayout;

            //Show Popup
            await Navigation.PushModalAsync(loginPage, false);
        }
    }
    
}