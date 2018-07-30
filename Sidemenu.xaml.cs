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
            
            App.Mainpage.PrintNews();
            
        }
	}
}