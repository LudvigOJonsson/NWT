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
        async void PostPage()
        {
            await Navigation.PushAsync(new PostPage(0));
        }
    }
}