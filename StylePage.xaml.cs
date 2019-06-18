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
    public partial class StylePage : ContentPage
    {
        public static TapGestureRecognizer TGR;
        int Rownr = 1;

        public Label Label = new Label { };
        public Image Image = new Image { };
        public Button Button = new Button { };

        public StylePage()
        {
            InitializeComponent();
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

        public void MakeButton(string s, int Type)
        {
            var Button = new StyleButton();


            StyleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            StyleGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });

            StyleGrid.Children.Add(Button.Label, 2, 7, Rownr, Rownr + 1); //Boxview
            StyleGrid.Children.Add(Button.Button, 1, 7, Rownr, Rownr + 1); //Label
            StyleGrid.Children.Add(Button.Image, 1, 2, Rownr, Rownr + 1); //Label
        }

        public class StyleButton
        {
            public Button Button = new Button { };
            public Label Label = new Label { };
            public Image Image = new Image { };

            public StyleButton()
            {
                Label = new Label
                {
                    Text = "",
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Start,
                    FontSize = 25,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Color.Black,
                    Margin = 15,
                };

                Label.GestureRecognizers.Add(TGR);

                Image = new Image
                {

                    Source = "",
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    Aspect = Aspect.AspectFill,
                    Margin = 0,

                };

                Image.GestureRecognizers.Add(TGR);

                Button = new Button
                {
                    BackgroundColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Margin = 0,
                };

                Button.GestureRecognizers.Add(TGR);
            }
        }

    }
}