using Rg.Plugins.Popup.Extensions;
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
    public partial class NumpadPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public NumpadPopup()
        {
            InitializeComponent();

            Num1.BackgroundColor = App.MC;
            Num2.BackgroundColor = App.MC;
            Num3.BackgroundColor = App.MC;
            Num4.BackgroundColor = App.MC;
            Num5.BackgroundColor = App.MC;
            Num6.BackgroundColor = App.MC;
            Num7.BackgroundColor = App.MC;
            Num8.BackgroundColor = App.MC;
            Num9.BackgroundColor = App.MC;
        }


        //Function that happens when you click on one of the numpads
        private void NumClicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            int IDnr = Convert.ToInt32(b.ClassId);
        }

        async void ClosePopup(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }
}