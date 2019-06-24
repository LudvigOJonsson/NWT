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
        List<NewsfeedTable> Rss = new List<NewsfeedTable>();
        List<NewsfeedTable> UserRss = new List<NewsfeedTable>();
        public CommunityPage()
		{
			InitializeComponent ();
            Rss = App.database.GetNF(4);

            CPMNAP.Source = Rss[0].Image;
            CPMNAT.Text = Rss[0].Header;
            CPNA1T.Text = Rss[1].Header;
            CPNA2T.Text = Rss[2].Header;
            CPNA3T.Text = Rss[3].Header;
            CPMNAB.ClassId = Rss[0].Article.ToString();
            CPNA1B.ClassId = Rss[1].Article.ToString();
            CPNA2B.ClassId = Rss[2].Article.ToString();
            CPNA3B.ClassId = Rss[3].Article.ToString();

            UserRss = App.database.GetCNF(4);

  
            CPMIAT.Text = UserRss[0].Header;
            CPIA1T.Text = UserRss[1].Header;
            CPIA2T.Text = UserRss[2].Header;
            CPIA3T.Text = UserRss[3].Header;
            CPMIAB.ClassId = UserRss[0].ID.ToString();
            CPIA1B.ClassId = UserRss[1].ID.ToString();
            CPIA2B.ClassId = UserRss[2].ID.ToString();
            CPIA3B.ClassId = UserRss[3].ID.ToString();

        }

        async void LoadNews(object sender, EventArgs e)
        {

            var Header = (View)sender;
            var id = Int32.Parse(Header.ClassId);
            var RSS = App.database.GetServerRSS(id).First();
           
            await Navigation.PushAsync(new NewsPage(RSS, 0));           
        }

        async void LoadUserNews(object sender, EventArgs e)
        {
            var Header = ((View)sender);

            var id = Int32.Parse(Header.ClassId);
            var RSS = App.database.GetServerRSS(id).First();

            //await Navigation.PushAsync(new UserNewsPage(RSS));
        }

        void Community()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[0];
        }
        void News(object sender, EventArgs e)
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[1];
        }
        void Hubb(object sender, EventArgs e)
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[2];
        }
        void Profile()
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[3];
        }
        async void Insandare(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            //await Navigation.PushAsync(new UserNewsGridPage());
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