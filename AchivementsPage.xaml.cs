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

        public AchivementsPage(StatsTable Stats)
		{
			InitializeComponent ();

            Trophylabel.TextColor = App.MC;
            PE1.BackgroundColor = App.MC;
            PE2.BackgroundColor = App.MC;
            PE3.BackgroundColor = App.MC;
            PE4.BackgroundColor = App.MC;
            PE5.BackgroundColor = App.MC;
            PE6.BackgroundColor = App.MC;
            PE7.BackgroundColor = App.MC;
            PE8.BackgroundColor = App.MC;
            PE9.BackgroundColor = App.MC;
            PE10.BackgroundColor = App.MC;


            
            UpdateAchivements(Stats);
        }

        public void UpdateAchivements(StatsTable Stats)
        {
            if (Stats == null && !App.Online)
            {
                return;
            }

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
            else
            {
                for (int i = 0; i < Stats.ArticlesRead+100; i += 100)
                {
                    if (Stats.ArticlesRead < i)
                    {
                        articleReadGoal = i;
                    }
                }
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
            else
            {
                for (int i = 0; i < Stats.TokensCollected + 100; i += 100)
                {
                    tokensCollectedGoal = i;
                }
            }

            tokensCollectedText.Text = Stats.TokensCollected.ToString() + " Tokens Insamlade";
            tokensCollectedNumber.Text = Stats.TokensCollected.ToString() + "/" + tokensCollectedGoal;
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
            else
            {
                for (int i = 0; i < Stats.InsandareRead + 100; i += 100)
                {
                    insandareReadGoal = i;
                }
            }

            insandareReadText.Text = Stats.InsandareRead.ToString() + " Insändare Lästa";
            insandareReadNumber.Text = Stats.InsandareRead.ToString() + "/" + insandareReadGoal;
            percentCompleted = (double)Stats.InsandareRead / (double)insandareReadGoal;
            insandareReadProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int commentsPostedGoal = 0;
            if (Stats.CommentsPosted < 1)
            {
                commentsPostedGoal = 1;
            }
            else if (Stats.CommentsPosted < 5)
            {
                commentsPostedGoal = 5;
            }
            else if (Stats.CommentsPosted < 10)
            {
                commentsPostedGoal = 10;
            }
            else if (Stats.CommentsPosted < 25)
            {
                commentsPostedGoal = 25;
            }
            else if (Stats.CommentsPosted < 50)
            {
                commentsPostedGoal = 50;
            }
            else
            {
                for (int i = 0; i < Stats.CommentsPosted + 100; i += 100)
                {
                    commentsPostedGoal = i;
                }
            }

            commentsPostedText.Text = Stats.CommentsPosted.ToString() + " Kommentarer Skrivna";
            commentsPostedNumber.Text = Stats.CommentsPosted.ToString() + "/" + commentsPostedGoal;
            percentCompleted = (double)Stats.CommentsPosted / (double)commentsPostedGoal;
            commentsPostedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int insandarePostedGoal = 0;
            if (Stats.InsandareSubmitted < 1)
            {
                insandarePostedGoal = 1;
            }
            else if (Stats.InsandareSubmitted < 5)
            {
                insandarePostedGoal = 5;
            }
            else if (Stats.InsandareSubmitted < 10)
            {
                insandarePostedGoal = 10;
            }
            else if (Stats.InsandareSubmitted < 25)
            {
                insandarePostedGoal = 25;
            }
            else if (Stats.InsandareSubmitted < 50)
            {
                insandarePostedGoal = 50;
            }
            else
            {
                for (int i = 0; i < Stats.InsandareSubmitted + 100; i += 100)
                {
                    insandarePostedGoal = i;
                }
            }
            insandarePostedText.Text = Stats.InsandareSubmitted.ToString() + " Insändare Skrivna";
            insandarePostedNumber.Text = Stats.InsandareSubmitted.ToString() + "/" + insandarePostedGoal;
            percentCompleted = (double)Stats.InsandareSubmitted / (double)insandarePostedGoal;
            insandarePostedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int questionPostedGoal = 0;
            if (Stats.VoteQuestionSubmitted < 1)
            {
                questionPostedGoal = 1;
            }
            else if (Stats.VoteQuestionSubmitted < 5)
            {
                questionPostedGoal = 5;
            }
            else if (Stats.VoteQuestionSubmitted < 10)
            {
                questionPostedGoal = 10;
            }
            else if (Stats.VoteQuestionSubmitted < 25)
            {
                questionPostedGoal = 25;
            }
            else if (Stats.VoteQuestionSubmitted < 50)
            {
                questionPostedGoal = 50;
            }
            else
            {
                for (int i = 0; i < Stats.VoteQuestionSubmitted + 100; i += 100)
                {
                    questionPostedGoal = i;
                }
            }
            questionPostedText.Text = Stats.VoteQuestionSubmitted.ToString() + " Lokalfråga Skapta";
            questionPostedNumber.Text = Stats.VoteQuestionSubmitted.ToString() + "/" + questionPostedGoal;
            percentCompleted = (double)Stats.VoteQuestionSubmitted / (double)questionPostedGoal;
            questionPostedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int quizPostedGoal = 0;
            if (Stats.QuestionSubmitted < 5)
            {
                quizPostedGoal = 5;
            }
            else if (Stats.QuestionSubmitted < 10)
            {
                quizPostedGoal = 10;
            }
            else if (Stats.QuestionSubmitted < 25)
            {
                quizPostedGoal = 25;
            }
            else if (Stats.QuestionSubmitted < 50)
            {
                quizPostedGoal = 50;
            }
            else
            {
                for (int i = 0; i < Stats.QuestionSubmitted + 100; i += 100)
                {
                    quizPostedGoal = i;
                }
            }

            quizPostedText.Text = Stats.QuestionSubmitted.ToString() + " Quizfrågor Skapta";
            quizPostedNumber.Text = Stats.QuestionSubmitted.ToString() + "/" + quizPostedGoal;
            percentCompleted = (double)Stats.QuestionSubmitted / (double)quizPostedGoal;
            quizPostedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int gamesFinishedGoal = 0;
            if (Stats.GameFinished < 1)
            {
                gamesFinishedGoal = 1;
            }
            else if (Stats.GameFinished < 3)
            {
                gamesFinishedGoal = 3;
            }
            else if (Stats.GameFinished < 10)
            {
                gamesFinishedGoal = 10;
            }
            else
            {
                for (int i = 0; i < Stats.GameFinished + 20; i += 20)
                {
                    gamesFinishedGoal = i;
                }
            }

            gamesFinishedText.Text = Stats.GameFinished.ToString() + " Spel Avklarade";
            gamesFinishedNumber.Text = Stats.GameFinished.ToString() + "/" + gamesFinishedGoal;
            percentCompleted = (double)Stats.GameFinished / (double)gamesFinishedGoal;
            gamesFinishedProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int questionAnsweredGoal = 0;
            if (Stats.VoteSubmitted < 1)
            {
                questionAnsweredGoal = 1;
            }
            else if (Stats.VoteSubmitted < 5)
            {
                questionAnsweredGoal = 5;
            }
            else if (Stats.VoteSubmitted < 10)
            {
                questionAnsweredGoal = 10;
            }
            else if (Stats.VoteSubmitted < 15)
            {
                questionAnsweredGoal = 15;
            }
            else
            {
                for (int i = 0; i < Stats.VoteSubmitted + 25; i += 25)
                {
                    questionAnsweredGoal = i;
                }
            }

            questionAnsweredText.Text = Stats.VoteSubmitted.ToString() + " Lokalfråga Svarade";
            questionAnsweredNumber.Text = Stats.VoteSubmitted.ToString() + "/" + questionAnsweredGoal;
            percentCompleted = (double)Stats.VoteSubmitted / (double)questionAnsweredGoal;
            questionAnsweredProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

            int quizAnsweredGoal = 0;
            if (Stats.QuestionAnswered < 1)
            {
                quizAnsweredGoal = 1;
            }
            else if (Stats.QuestionAnswered < 5)
            {
                quizAnsweredGoal = 5;
            }
            else if (Stats.QuestionAnswered < 10)
            {
                quizAnsweredGoal = 10;
            }
            else if (Stats.QuestionAnswered < 15)
            {
                quizAnsweredGoal = 15;
            }
            else
            {
                for (int i = 0; i < Stats.QuestionAnswered + 25; i += 25)
                {
                    quizAnsweredGoal = i;
                }
            }

            quizAnsweredText.Text = Stats.QuestionAnswered.ToString() + " Quiz Svarade";
            quizAnsweredNumber.Text = Stats.QuestionAnswered.ToString() + "/" + quizAnsweredGoal;
            percentCompleted = (double)Stats.QuestionAnswered / (double)quizAnsweredGoal;
            quizAnsweredProgressBar.WidthRequest = Application.Current.MainPage.Width * percentCompleted;

        }

        private async void ShowPopup(object sender, EventArgs ev)
        {
            //No popups atm
            return;
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
                IsPlaying = true
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