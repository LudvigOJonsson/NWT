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
	public partial class PasswordPage : ContentPage
	{
        public PasswordPage()
		{
			InitializeComponent ();
        }

        public void ChangePassword(object sender, EventArgs e)
        {
            if(App.database.TokenCheck() && NPass.Text != null && RPass.Text != null)
            {
                App.database.ChangePassword(NPass.Text,RPass.Text);
            }
        }

        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }
    }
}