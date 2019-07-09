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
	public partial class UserSettingsPage : ContentPage
	{
        public UserSettingsPage()
		{
			InitializeComponent ();
            UpdateInfoButton.BackgroundColor = App.MC;
        }
    }
}