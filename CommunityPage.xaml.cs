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
	public partial class CommunityPage : ContentPage
	{
		public CommunityPage()
		{
			InitializeComponent ();
            
		}

        void Community()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[0];
        }
        void News()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[1];
        }
        void Profile()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[2];
        }
        void Games()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[3];
        }
        async void Insandare(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new UserNewsGridPage());
        }
        async void MakeInsandare(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new UserSubmissionPage());
        }

        async void VotePage(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new VotePage());
        }

    }
}