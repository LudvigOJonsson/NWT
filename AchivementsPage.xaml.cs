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
        public Label Label2 = new Label { };
        public Image Image = new Image { };
        public Image Image2 = new Image { };
        public Button Button = new Button { };

        public AchivementsPage()
		{
			InitializeComponent ();
        }
        private async void ShowPopup(object sender, EventArgs ev)
        {
            //Create `ContentPage` with padding and transparent background
            ContentPage popup = new ContentPage
            {
                BackgroundColor = Color.FromHex("#D9000000"),
                Padding = new Thickness(20, 20, 20, 20)
            };

            // Create Children
            Label = new Label
            {
                Text = "Read 5 Articles",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HeightRequest = 80,
                TextColor = Color.White,
                FontSize = 25,
                Margin = 10
            };
            // Create Children
            Label2 = new Label
            {
                Text = "Hello!Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.White,
                FontSize = 18,
                Margin = 30
            };
            Button = new Button
            {
                HeightRequest = 80,
                WidthRequest = 80,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.White,
                Margin = 10
                
            };

            Image = new Image
            {
                Source = "knapp.png",
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            Image2 = new Image
            {
                Source = "articleIcon.png",
                WidthRequest = 80,
                HeightRequest = 80,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { });
            grid.RowDefinitions.Add(new RowDefinition { });
            grid.RowDefinitions.Add(new RowDefinition { });
            grid.ColumnDefinitions.Add(new ColumnDefinition { });

            grid.Children.Add(Image, 0, 0);
            grid.Children.Add(Image2, 0, 0);
            grid.Children.Add(Label, 0, 1);
            grid.Children.Add(Button, 0, 2);

            //Create desired layout to be a content of your popup page. 
            var contentLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children =
          {
              // Add children
              grid
          }
        };

            //set popup page content:
            popup.Content = contentLayout;

            //Show Popup
            await Navigation.PushModalAsync(popup, false);

            Button.Clicked += (s, e) => {
                OnDismissButtonClicked(s,e);
            };

        }
        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }
    }
    
}