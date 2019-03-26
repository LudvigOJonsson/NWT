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
            var Stats = new StatsTable();
            Stats.User = -1;
            Stats.Logins = 69;
            Stats.UseTime = 420;
            Stats.ArticlesRead = 27;
            Stats.PlusArticlesUnlocked = 12;
            Stats.InsandareSubmitted = 12;
            Stats.InsandareRead = 53;
            Stats.GameFinished = 4;
            Stats.QuestionSubmitted = 1;
            Stats.QuestionAnswered = 5;
            Stats.VoteQuestionSubmitted = 1;
            Stats.VoteSubmitted = 13;
            Stats.CommentsPosted = 17;
            Stats.TokensCollected = 123;

            UpdateAchivements(Stats);
        }

        public void UpdateAchivements(StatsTable Stats)
        {
            int articleReadGoal = 0;
            if (Stats.ArticlesRead < 5)
            {
                articleReadGoal = 5;
            }
            else if (Stats.ArticlesRead < 10)
            {
                articleReadGoal = 10;
            }
            else if (Stats.ArticlesRead < 25)
            {
                articleReadGoal = 25;
            }
            else if (Stats.ArticlesRead < 50)
            {
                articleReadGoal = 50;
            }
            else if (Stats.ArticlesRead < 100)
            {
                articleReadGoal = 100;
            }

            articleReadText.Text = Stats.ArticlesRead.ToString() + " Artiklar Lästa";
            articleReadNumber.Text = Stats.ArticlesRead.ToString() + "/" + articleReadGoal;
            double percentCompleted = (double)Stats.ArticlesRead / (double)articleReadGoal;
            articleReadProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int tokensCollectedGoal = 0;
            if (Stats.TokensCollected < 5)
            {
                tokensCollectedGoal = 5;
            }
            else if (Stats.TokensCollected < 10)
            {
                tokensCollectedGoal = 10;
            }
            else if (Stats.TokensCollected < 25)
            {
                tokensCollectedGoal = 25;
            }
            else if (Stats.TokensCollected < 50)
            {
                tokensCollectedGoal = 50;
            }
            else if (Stats.TokensCollected < 100)
            {
                tokensCollectedGoal = 100;
            }

            tokensCollectedText.Text = Stats.TokensCollected.ToString() + " Tokens Insamlade";
            tokensCollectedNumber.Text = Stats.TokensCollected.ToString() + "/" + articleReadGoal;
            percentCompleted = (double)Stats.TokensCollected / (double)tokensCollectedGoal;
            tokensCollectedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int insandareReadGoal = 0;
            if (Stats.InsandareRead < 5)
            {
                insandareReadGoal = 5;
            }
            else if (Stats.InsandareRead < 10)
            {
                insandareReadGoal = 10;
            }
            else if (Stats.InsandareRead < 25)
            {
                insandareReadGoal = 25;
            }
            else if (Stats.InsandareRead < 50)
            {
                insandareReadGoal = 50;
            }
            else if (Stats.InsandareRead < 100)
            {
                insandareReadGoal = 100;
            }

            insandareReadText.Text = Stats.InsandareRead.ToString() + " Insändare Lästa";
            insandareReadNumber.Text = Stats.InsandareRead.ToString() + "/" + insandareReadGoal;
            percentCompleted = (double)Stats.InsandareRead / (double)insandareReadGoal;
            tokensCollectedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int commentsPostedGoal = 0;
            if (Stats.CommentsPosted < 5)
            {
                commentsPostedGoal = 1;
            }
            else if (Stats.CommentsPosted < 10)
            {
                commentsPostedGoal = 5;
            }
            else if (Stats.CommentsPosted < 25)
            {
                commentsPostedGoal = 10;
            }
            else if (Stats.CommentsPosted < 50)
            {
                commentsPostedGoal = 25;
            }
            else if (Stats.CommentsPosted < 100)
            {
                commentsPostedGoal = 50;
            }

            commentsPostedText.Text = Stats.CommentsPosted.ToString() + " Kommentarer Skrivna";
            commentsPostedNumber.Text = Stats.CommentsPosted.ToString() + "/" + commentsPostedGoal;
            percentCompleted = (double)Stats.CommentsPosted / (double)commentsPostedGoal;
            commentsPostedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int insandarePostedGoal = 0;
            if (Stats.InsandareSubmitted < 5)
            {
                insandarePostedGoal = 1;
            }
            else if (Stats.InsandareSubmitted < 10)
            {
                insandarePostedGoal = 5;
            }
            else if (Stats.InsandareSubmitted < 25)
            {
                insandarePostedGoal = 10;
            }
            else if (Stats.InsandareSubmitted < 50)
            {
                insandarePostedGoal = 25;
            }
            else if (Stats.InsandareSubmitted < 100)
            {
                insandarePostedGoal = 50;
            }

            insandarePostedText.Text = Stats.InsandareSubmitted.ToString() + " Insändare Skrivna";
            insandarePostedNumber.Text = Stats.InsandareSubmitted.ToString() + "/" + insandarePostedGoal;
            percentCompleted = (double)Stats.InsandareSubmitted / (double)insandarePostedGoal;
            insandarePostedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int questionPostedGoal = 0;
            if (Stats.VoteQuestionSubmitted < 5)
            {
                questionPostedGoal = 1;
            }
            else if (Stats.VoteQuestionSubmitted < 10)
            {
                questionPostedGoal = 5;
            }
            else if (Stats.VoteQuestionSubmitted < 25)
            {
                questionPostedGoal = 10;
            }
            else if (Stats.VoteQuestionSubmitted < 50)
            {
                questionPostedGoal = 25;
            }
            else if (Stats.VoteQuestionSubmitted < 100)
            {
                questionPostedGoal = 50;
            }

            questionPostedText.Text = Stats.VoteQuestionSubmitted.ToString() + " Lokalfråga Skapta";
            questionPostedNumber.Text = Stats.VoteQuestionSubmitted.ToString() + "/" + questionPostedGoal;
            percentCompleted = (double)Stats.VoteQuestionSubmitted / (double)questionPostedGoal;
            questionPostedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int quizPostedGoal = 0;
            if (Stats.QuestionSubmitted < 5)
            {
                quizPostedGoal = 1;
            }
            else if (Stats.QuestionSubmitted < 10)
            {
                quizPostedGoal = 5;
            }
            else if (Stats.QuestionSubmitted < 25)
            {
                quizPostedGoal = 10;
            }
            else if (Stats.QuestionSubmitted < 50)
            {
                quizPostedGoal = 25;
            }
            else if (Stats.QuestionSubmitted < 100)
            {
                quizPostedGoal = 50;
            }

            quizPostedText.Text = Stats.QuestionSubmitted.ToString() + " Quizfrågor Skapta";
            quizPostedNumber.Text = Stats.QuestionSubmitted.ToString() + "/" + quizPostedGoal;
            percentCompleted = (double)Stats.QuestionSubmitted / (double)quizPostedGoal;
            quizPostedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int gamesFinishedGoal = 0;
            if (Stats.GameFinished < 5)
            {
                gamesFinishedGoal = 1;
            }
            else if (Stats.GameFinished < 10)
            {
                gamesFinishedGoal = 3;
            }
            else if (Stats.GameFinished < 25)
            {
                gamesFinishedGoal = 10;
            }
            else if (Stats.GameFinished < 50)
            {
                gamesFinishedGoal = 20;
            }

            gamesFinishedText.Text = Stats.GameFinished.ToString() + " Spel Avklarade";
            gamesFinishedNumber.Text = Stats.GameFinished.ToString() + "/" + gamesFinishedGoal;
            percentCompleted = (double)Stats.GameFinished / (double)gamesFinishedGoal;
            gamesFinishedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int questionAnsweredGoal = 0;
            if (Stats.VoteSubmitted < 5)
            {
                questionAnsweredGoal = 1;
            }
            else if (Stats.VoteSubmitted < 10)
            {
                questionAnsweredGoal = 5;
            }
            else if (Stats.VoteSubmitted < 25)
            {
                questionAnsweredGoal = 10;
            }
            else if (Stats.VoteSubmitted < 50)
            {
                questionAnsweredGoal = 15;
            }
            else if (Stats.VoteSubmitted < 100)
            {
                questionAnsweredGoal = 25;
            }

            gamesFinishedText.Text = Stats.VoteSubmitted.ToString() + " Lokalfråga Svarade";
            gamesFinishedNumber.Text = Stats.VoteSubmitted.ToString() + "/" + questionAnsweredGoal;
            percentCompleted = (double)Stats.VoteSubmitted / (double)questionAnsweredGoal;
            questionAnsweredProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int quizAnsweredGoal = 0;
            if (Stats.QuestionAnswered < 5)
            {
                quizAnsweredGoal = 1;
            }
            else if (Stats.QuestionAnswered < 10)
            {
                quizAnsweredGoal = 5;
            }
            else if (Stats.QuestionAnswered < 25)
            {
                quizAnsweredGoal = 10;
            }
            else if (Stats.QuestionAnswered < 50)
            {
                quizAnsweredGoal = 15;
            }
            else if (Stats.QuestionAnswered < 100)
            {
                quizAnsweredGoal = 25;
            }

            gamesFinishedText.Text = Stats.QuestionAnswered.ToString() + " Quiz Svarade";
            gamesFinishedNumber.Text = Stats.QuestionAnswered.ToString() + "/" + quizAnsweredGoal;
            percentCompleted = (double)Stats.QuestionAnswered / (double)quizAnsweredGoal;
            quizAnsweredProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

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

            var x = new Lottie.Forms.AnimationView
            {
                Animation = "confetti.json",
                Loop = false,
                AutoPlay = true
            };


            grid.Children.Add(Image, 0, 0);
            grid.Children.Add(Image2, 0, 0);
            grid.Children.Add(Label, 0, 1);
            grid.Children.Add(x, 0, 1, 0, 3);
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