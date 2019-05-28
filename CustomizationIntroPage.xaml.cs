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
	public partial class CustomizationIntroPage : ContentPage
	{
		public CustomizationIntroPage()
		{
			InitializeComponent();
            
		}

        async void CustomizationPage(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            /*await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);*/
            await Navigation.PushAsync(new PlayPage());
        }
    }
}