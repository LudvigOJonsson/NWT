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
	public partial class Sidemenu : ContentPage
	{
     
        public Sidemenu ()
		{
			InitializeComponent ();
            
        }

        public void PrintNews()
        {

            //NewsGridPage Page = (NewsGridPage)App.Mainpage.Children[1];
            //Page.PrintNews();
        }
        public void Logout()
        {
            if (App.LoggedinUser != null)
            {
                ProfilePage page = (ProfilePage)App.Mainpage.Children[2];
                page.Logout();
            }
        }
        async void Logout(object sender, EventArgs e)
        {
            ProfilePage page = (ProfilePage)App.Mainpage.Children[2];
            page.Logout();
            Button button = (Button)sender;
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
        }
        async void About(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new AboutPage());
        }
        async void Settings(object sender, EventArgs e)
        {
            if (App.LoggedinUser != null)
            {
                Button button = (Button)sender;
                await button.RotateTo(-5, 80, Easing.BounceOut);
                await button.RotateTo(5, 120, Easing.BounceOut);
                await button.RotateTo(0, 80, Easing.BounceOut);
                await Navigation.PushAsync(new SettingsPage());
            }
        }

    }
}