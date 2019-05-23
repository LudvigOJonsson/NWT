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
	public partial class HubbPage : ContentPage
	{
		public HubbPage()
		{
			InitializeComponent ();
            
		}

        public void ButtonLock()
        {
            InsandareButton.IsEnabled = !InsandareButton.IsEnabled;
            MakeInsandareButton.IsEnabled = !MakeInsandareButton.IsEnabled;
            EvenemangsButton.IsEnabled = !EvenemangsButton.IsEnabled;
            MakeEvenemangsButton.IsEnabled = !MakeEvenemangsButton.IsEnabled;
            VoteButton.IsEnabled = !VoteButton.IsEnabled;
            PlayButton.IsEnabled = !PlayButton.IsEnabled;
        }


        async void Insandare(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            /*await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);*/
            await Navigation.PushAsync(new UserNewsGridPage());
            ButtonLock();
        }
        async void MakeInsandare(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            /*await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);*/
            await Navigation.PushAsync(new UserSubmissionPage());
            ButtonLock();
        }

        async void VotePage(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            /*await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);*/
            await Navigation.PushAsync(new VotePage());
            ButtonLock();
        }

        async void GamePage(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            /*await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);*/
            await Navigation.PushAsync(new PlayPage());
            ButtonLock();
        }
        async void WIP(object sender, EventArgs e)
        {
            await DisplayAlert("WIP", "Work in progress.", "Okay");
        }

    }
}