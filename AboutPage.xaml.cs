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
	public partial class AboutPage : ContentPage
	{
        public AboutPage()
		{
			InitializeComponent ();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Uri siteUri = new Uri("https://forms.gle/P8b4CbjRwXKPNXtL9");

            Device.OpenUri(siteUri);
        }
    }
}