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
    public partial class ReactionPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        int ArticleID = -1;
        public ReactionPopUp(int ArticleID_)
        {
            ArticleID = ArticleID_;

            InitializeComponent();
        }

        public void SelectReaction(object sender, EventArgs e)
        {
            //What happens when selecting a reaction, accessing the server, 
            //creating new reaction on the table, 
            //changing grafiks on newspage and/or newsfeed!
            var BT = (Button)sender;
            if (ArticleID != -1)
            {
                var RT = new ReactionTable
                {
                    User = App.LoggedinUser.ID,
                    Reaktion = Convert.ToInt32(BT.ClassId),
                    Article = ArticleID
                };

            App.database.InsertReaction(RT);
                
            }
            


            //Then close window, as you can only select one reaction at a time
            ClosePopup(sender, e);
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