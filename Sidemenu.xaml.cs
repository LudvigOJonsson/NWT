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

        public string Filter = "";
        public string Author = "";
        public string Tag = "";

        public Sidemenu ()
		{
			InitializeComponent ();
            
        }
        

        public void SetKategori(object sender, EventArgs e)
        {
            var Sender = (Button)sender;
            Filter = Sender.ClassId;
        }
        public void SetTag(object sender, EventArgs e)
        {
            var Sender = (Button)sender;
            Tag = Sender.ClassId;
        }
        public void SetAuthor(object sender, EventArgs e)
        {
            var Sender = (Button)sender;
            Author = Sender.ClassId;
        }


        public void Clear(object sender, EventArgs e)
        {
            Filter = "";
            Author = "";
            Tag = "";
        }


        public void PrintNews(object sender, EventArgs e)
        {

            ButtonLock();
            NewsGridPage Page = (NewsGridPage)App.Mainpage.Children[1];
            App.database.LocalExecute("DELETE FROM NF");

            Page.PREV = 0;
            Page.CURR = NewsGridPage.DBLN;
            Page.NEXT = NewsGridPage.DBLN * 2;
            Page.Loadnr = 1;
            Page.Filter = Filter;
            Page.Author = Author;
            Page.Tag = Tag;

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

            Tibro.IsEnabled = !Tibro.IsEnabled;
            Skövde.IsEnabled = !Skövde.IsEnabled;
            Falkköping.IsEnabled = !Falkköping.IsEnabled;
            Karlsborg.IsEnabled = !Karlsborg.IsEnabled;
            Rensa.IsEnabled = !Rensa.IsEnabled;
            Verkställ.IsEnabled = !Verkställ.IsEnabled;


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