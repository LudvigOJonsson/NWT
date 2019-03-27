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
	public partial class AvatarPage : ContentPage
    {
        public int AvatarHairIndex;
        public int AvatarBodyIndex;


        public AvatarPage()
        {

            InitializeComponent();

            //Get the user's saved avatar indexes here
            //CODE
            //But right now these database variables do not exist. So instead, a local variable will be saved.

            //If the index is not saved to anything, then set it to the first objects.
            if (AvatarBodyIndex == 0)
                AvatarBodyIndex = 1;
            if (AvatarHairIndex == 0)
                AvatarHairIndex = 1;

            //Make the avatar picture into whatever pieces are saved on the user's account.
            ProfilePictureHair.Source = "avatar_hair" + AvatarHairIndex + ".png";
            ProfilePictureBody.Source = "avatar_body" + AvatarHairIndex + ".png";
        }

        public void ChangeHair(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            ProfilePictureHair.Source = image.Source;
            AvatarHairIndex = image.Source.ToString()[12];
        }
        public void ChangeBody(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            ProfilePictureBody.Source = image.Source;
            AvatarBodyIndex = image.Source.ToString()[12];
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }
    }
}