﻿using FFImageLoading.Forms;
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
    public partial class AvatarPopup : Rg.Plugins.Popup.Pages.PopupPage
    {

        public AvatarPage avatarPage;
        public CachedImage buttonImage;
        public int cost;
        public int idNumber;

        public AvatarPopup()
        {
            InitializeComponent();
        }

        public void UpdatePreview(CachedImage button, int coinAmount, AvatarPage ap, int id, string source)
        {
            YesButton.Text = "Köp för " + coinAmount + " mynt";
            NoButton.BackgroundColor = App.MC;
            YesButton.BackgroundColor = App.MC;
            PreviewImage.Source = source;
            avatarPage = ap;
            buttonImage = button;
            cost = coinAmount;
            idNumber = id;
            PreviewImage.BackgroundColor = App.MC;
        }
        public void Purchase(object sender, EventArgs e)
        {
            avatarPage.UnlockForReal(buttonImage, idNumber, cost);
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