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

        public string saveString = "kek";


        public AvatarPage()
        {

            InitializeComponent();

            //Get the user's saved avatar indexes here
            //CODE
            //But right now these database variables do not exist. So instead, a local variable will be saved.


            var PP = (ProfilePage)App.Mainpage.Children[3];
            
            //Make the avatar picture into whatever pieces are saved on the user's account.
            ProfilePictureHair.Source = PP.avatarHairPic.Source;
            ProfilePictureBody.Source = PP.avatarBodyPic.Source;

            if (App.LoggedinUser.TutorialProgress == 1)
            {
                App.LoggedinUser.TutorialProgress = 2;
            }

        }

        async void UnlockComponent(object sender, EventArgs e)
        {
            var Button = (Image)sender;
            var id = Button.ClassId;
            int tokenNumber = CostParser(id);
            bool answer = await DisplayAlert("", "Vill du låsa upp utstyrseln för " + tokenNumber + " mynt?", "Nej", "Ja");
            if (!answer)
            {
                
                if (App.LoggedinUser.Plustokens >= tokenNumber)
                {
                    //Unlocks item
                    Button.IsEnabled = false;
                    Button.IsVisible = false;
                    App.database.Plustoken(App.LoggedinUser, -tokenNumber);
                } else
                {
                    await DisplayAlert("", "Inte tillräckligt mynt. Du har bara " + App.LoggedinUser.Plustokens + ". Du behöver " + (tokenNumber - App.LoggedinUser.Plustokens) + " mynt till.", "Okej.");
                }
            }
        }

        public int CostParser(string itemId)
        {
            if (itemId == "hair")
            {
                return 10;
            }
            else
            {
                return 0;
            }
        }

        public void ChangeHair(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            ProfilePictureHair.Source = image.Source;
            var PP = (ProfilePage)App.Mainpage.Children[3];
            PP.updateAvatar(ProfilePictureHair.Source, ProfilePictureBody.Source, ProfilePictureFace.Source);
        }
        public void ChangeBody(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            ProfilePictureBody.Source = image.Source;
            var PP = (ProfilePage)App.Mainpage.Children[3];
            PP.updateAvatar(ProfilePictureHair.Source, ProfilePictureBody.Source, ProfilePictureFace.Source);
        }
        public void ChangeFace(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            ProfilePictureFace.Source = image.Source;
            var PP = (ProfilePage)App.Mainpage.Children[3];
            PP.updateAvatar(ProfilePictureHair.Source, ProfilePictureBody.Source, ProfilePictureFace.Source);
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