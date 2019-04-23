using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
// await DisplayAlert("Task", "PING","OK");

namespace NWT
{
	public partial class MainPage : Xamarin.Forms.TabbedPage
    {



        public MainPage()
        {
            
            InitializeComponent();
            
            //On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarItemColor(Color.FromHex("FFFFFF"));
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(Color.FromHex("FFFFFF"));
            
            ToolbarItems.Add(new ToolbarItem("Search", "search.png", async () => { var page = new ContentPage(); var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");
            Debug.WriteLine("success: {0}", result); }));
            App.database.LoadUserRSS(1, (10));
            Children[1] = new NewsGridPage(0);
            Children[0] = new CommunityPage();

            Children[3] = new ProfilePage();
            Children[2] = new HubbPage();                      
        }
  /*
        void Community(object sender, EventArgs e)
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[0];
        }
        void News(object sender, EventArgs e)
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[1];
        }
        void Profile(object sender, EventArgs e)
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[2];
        }
        void Games(object sender, EventArgs e)
        {
            App.Mainpage.CurrentPage = App.Mainpage.Children[3];
        }
      */

    }
}
