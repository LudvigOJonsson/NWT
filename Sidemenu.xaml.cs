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
        public Button previousButton;
     
        public Sidemenu ()
		{
			InitializeComponent ();
            
        }

        public void PrintNews(object sender, EventArgs e)
        {
            if (previousButton != null)
            {
                previousButton.BorderWidth = 0;
            }

            var Filter = (Button)sender;
            Filter.BorderWidth = 3;
            Filter.BorderColor = Color.Black;
            previousButton = Filter;
            ButtonLock();
            NewsGridPage Page = (NewsGridPage)App.Mainpage.Children[1];
            App.database.LocalExecute("DELETE FROM NF");

            Page.PREV = 0;
            Page.CURR = NewsGridPage.DBLN;
            Page.NEXT = NewsGridPage.DBLN * 2;
            Page.Loadnr = 1;
            Page.Filter = Filter.ClassId;
            Page.ArticleList.Clear();
            Page.LoadLocalDB();
            Page.AddNews(0);
            ButtonLock();
            
        }

        public void ButtonLock()
        {
            Nyheter.IsEnabled = !Nyheter.IsEnabled;
            Sport.IsEnabled = !Sport.IsEnabled;
            Ekonomi.IsEnabled = !Ekonomi.IsEnabled;
            NöjeochKultur.IsEnabled = !NöjeochKultur.IsEnabled; 
            Åsikter.IsEnabled = !Åsikter.IsEnabled;
            Familj.IsEnabled = !Familj.IsEnabled;
            UserSettingsB.IsEnabled = !UserSettingsB.IsEnabled;
            AboutB.IsEnabled = !AboutB.IsEnabled;
            LogoutB.IsEnabled = !LogoutB.IsEnabled;
        }


        async void Logout(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            ProfilePage page = (ProfilePage)App.Mainpage.Children[2];
            page.Logout();            
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            ButtonLock();
        }
        async void About(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new AboutPage());
            ButtonLock();
        }
        async void UserSettings(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new UserSettingsPage());
            ButtonLock();
        }

    }
}