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
            if (App.Mainpage.Children[2].ToString() == "ProfilePage") {
                ProfilePage page = (ProfilePage)App.Mainpage.Children[2];
                page.Logout();
            }
        }
	}
}