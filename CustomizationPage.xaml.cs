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
	public partial class CustomizationPage : ContentPage
	{
		public CustomizationPage()
		{
			InitializeComponent();
            
		}

        public void FollowButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "Follow")
            {
                button.Text = "Unfollow";
            } else if (button.Text == "Unfollow")
            {
                button.Text = "Unfollow";
            }
        }
    }
}