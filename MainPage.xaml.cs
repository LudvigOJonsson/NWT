using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

// await DisplayAlert("Task", "PING","OK");

namespace NWT
{
	public partial class MainPage : TabbedPage
	{



        public MainPage()
        {
            
            InitializeComponent();
            Children[1] = new NewsGridPage(0);
            Children[0] = new CommunityPage();
            Children[2] = new LoginPage();
            Children[3] = new PlayPage();                      
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
