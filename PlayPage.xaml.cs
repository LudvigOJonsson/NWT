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
	public partial class PlayPage : ContentPage
	{
		public PlayPage ()
		{
			InitializeComponent ();

            BackgroundColor = App.MC;

            //  SPEL.TextColor = App.MC;
            // SudokuButton.BackgroundColor = App.MC;
            // QuizButton.BackgroundColor = App.MC;
            // PicrossButton.BackgroundColor = App.MC;

        }

        async void PlaySudoku(object sender, EventArgs e)
        {
            SudokuButton.IsEnabled = false;
            if (App.Online)
            {
                await Navigation.PushAsync(new SudokuPage());
            }
            else
            {
                await DisplayAlert("Offline", "The Server is currently Offline. Please try again later.", "OK");
            }
            SudokuButton.IsEnabled = true;
        }
        async void PlayQuiz(object sender, EventArgs e)
        {
            QuizButton.IsEnabled = false;
            if (App.Online)
            {
                await Navigation.PushAsync(new QuizPage());
            }
            else
            {
                await DisplayAlert("Offline", "The Server is currently Offline. Please try again later.", "OK");
            }
            QuizButton.IsEnabled = true;
        }

        async void PlayPicross(object sender, EventArgs e)
        {
            PicrossButton.IsEnabled = false;
            if (App.Online)
            {
                await Navigation.PushAsync(new PicrossPage());
            }
            else
            {
                await DisplayAlert("Offline", "The Server is currently Offline. Please try again later.", "OK");
            }
            PicrossButton.IsEnabled = true;
        }

    }
}