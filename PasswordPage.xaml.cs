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

            ChangePasswordButton.BackgroundColor = App.MC;
        }

        async public void ChangePassword(object sender, EventArgs e)
        {
            if(App.database.TokenCheck() && NPass.Text != null && RPass.Text != null)
            {
                App.database.ChangePassword(NPass.Text,RPass.Text);
                await DisplayAlert("Lösenord ändrat", "Ditt lösenord är ändrat.", "Okej");
            }
            else
            {
                await DisplayAlert("Något blev fel", "Kolla så att båda rutorna är ifyllda. Pröva sedan igen.", "Okej");
            }
        }

        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }
    }
}