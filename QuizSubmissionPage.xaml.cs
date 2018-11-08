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
	public partial class QuizSubmissionPage : ContentPage
	{
        public string CorAnsw = "";
        public string Categ = "Kategori 1";

        public Button ChoiceA = new Button { };
        public Button ChoiceB = new Button { };
        public Button ChoiceC = new Button { };
        public Button ChoiceD = new Button { };

        public QuizSubmissionPage()
		{
			InitializeComponent();
		}

        public void CatPop(object sender, EventArgs e)
        {

        }

        public async void AnswerPop(object sender, EventArgs e)
        {
            //Create `ContentPage` with padding and transparent background
            ContentPage popup = new ContentPage
            {
                BackgroundColor = Color.FromHex("#D9000000"),
                Padding = new Thickness(20, 20, 20, 20)
            };

            // Create Children
            
            ChoiceA = new Button
            {
                HeightRequest = 80,
                WidthRequest = 80,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.White,
                Margin = 10,
                Text = "A",
                ClassId = "A"
            };
            ChoiceB = new Button
            {
                HeightRequest = 80,
                WidthRequest = 80,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.White,
                Margin = 10,
                Text = "B",
                ClassId = "B"

            };
            ChoiceC = new Button
            {
                HeightRequest = 80,
                WidthRequest = 80,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.White,
                Margin = 10,
                Text = "C",
                ClassId = "C"
            };
            ChoiceD = new Button
            {
                HeightRequest = 80,
                WidthRequest = 80,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.White,
                Margin = 10,
                Text = "D",
                ClassId = "D"
            };


            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { });
            grid.RowDefinitions.Add(new RowDefinition { });
            
            grid.ColumnDefinitions.Add(new ColumnDefinition { });
            grid.ColumnDefinitions.Add(new ColumnDefinition { });


            grid.Children.Add(ChoiceA, 0, 0);
            grid.Children.Add(ChoiceB, 1, 0);
            grid.Children.Add(ChoiceC, 0, 1);
            grid.Children.Add(ChoiceD, 1, 1);

            //Create desired layout to be a content of your popup page. 
            var contentLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
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

            ChoiceA.Clicked += (s, c) => {
                CorAnsw = ChoiceA.ClassId;
                OnDismissButtonClicked(s, c);
            };
            ChoiceB.Clicked += (s, c) => {
                CorAnsw = ChoiceB.ClassId;
                OnDismissButtonClicked(s, c);
            };
            ChoiceC.Clicked += (s, c) => {
                CorAnsw = ChoiceC.ClassId;
                OnDismissButtonClicked(s, c);
            };
            ChoiceD.Clicked += (s, c) => {
                CorAnsw = ChoiceD.ClassId;
                OnDismissButtonClicked(s, c);
            };
        }


        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        public async void SubmitQ(object sender, EventArgs e)
        {
            if (CorAnsw != "" && Categ != "")
            {
                var Q = new QuizTable();
                Q.QuestionText = QuestionText.Text;
                Q.ChoiceA = A.Text;
                Q.ChoiceB = B.Text;
                Q.ChoiceC = C.Text;
                Q.ChoiceD = D.Text;
                Q.Category = Categ;
                Q.CorrectAnswer = CorAnsw;
                App.database.InsertQuestion(Q);
            }
            await DisplayAlert("Submission Successful", "Question Submitted", "OK");
        }

        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }
    }
}