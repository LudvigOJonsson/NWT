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
            UnselectedTabColor = Color.FromHex("FFFFFF");
            SelectedTabColor = Color.FromHex("FFFFFF");

            ToolbarItems.Add(new ToolbarItem("Search", "Icon_Search_white.png", async () => { var page = new ContentPage(); var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");
            
            Debug.WriteLine("success: {0}", result); }));


            Children[3] = new HubbPage();
            Children[2] = new ProfilePage();
            Children[1] = new NewsGridPage(0);
            Children[0] = new CustomNewsFeed();

    }

        /*
        protected override void OnCurrentPageChanged()
        {
            if (App.Instanciated)
            {
                base.OnCurrentPageChanged();

                if (CurrentPage == Children[1])
                {
                    App.SideMenu.ToggleFeed(1);
                }
                else if (CurrentPage == Children[0])
                {
                    App.SideMenu.ToggleFeed(0);
                }
            }



        }*/
    }
}
