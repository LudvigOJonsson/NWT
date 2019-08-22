using Newtonsoft.Json;
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

        public List<int> Inventory = new List<int>();
        public List<string> Avatar = new List<string>(); 

        public AvatarPage()
        {

            InitializeComponent();

            //Get the user's saved avatar indexes here
            //CODE
            //But right now these database variables do not exist. So instead, a local variable will be saved.


            var PP = (ProfilePage)App.Mainpage.Children[2];

            //Make the avatar picture into whatever pieces are saved on the user's account.


            if (App.LoggedinUser.TutorialProgress == 1)
            {
                App.LoggedinUser.TutorialProgress = 2;
            }
            Inventory = JsonConvert.DeserializeObject<List<int>>(App.LoggedinUser.Inventory);
            Avatar = JsonConvert.DeserializeObject<List<string>>(App.LoggedinUser.Avatar);

            ProfilePictureFace.Source = Avatar[0];
            ProfilePictureHair.Source = Avatar[1];
            ProfilePictureBody.Source = Avatar[2];


            var ItemList = App.database.GetAllItems();

            var Row = 3;
            var Column = 0;

            foreach(var Item in ItemList)
            {
                if(Column == 5)
                {
                    Column = 0;
                    Row++;
                }
                var IMG = new Image
                {
                    ClassId = Item.ImagePath,
                    Source = Item.ImagePath,
                    HorizontalOptions = LayoutOptions.CenterAndExpand, 
                    VerticalOptions = LayoutOptions.CenterAndExpand, 
                    BackgroundColor = Color.FromHex("#649FD4") ,
                    Margin = 5

                };

                var TGR = new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1
                };
                if (Item.InventorySlot == "Hair")
                {

                    TGR.Tapped += (s, e) => {
                        IsEnabled = false;
                        ChangeHair(s, e);
                        IsEnabled = true;
                    };
                }
                else if (Item.InventorySlot == "Body")
                {
                    TGR.Tapped += (s, e) => {
                        IsEnabled = false;
                        ChangeBody(s, e);
                        IsEnabled = true;
                    };
                }
                IMG.GestureRecognizers.Add(TGR);
                AvatarButtonsGrid.Children.Add(IMG, Column, Row);

                if (!Inventory.Contains(Item.ID))
                {
                    var TGR2 = new TapGestureRecognizer()
                    {
                        NumberOfTapsRequired = 1
                    };

                    var IMG2 = new Image
                    {
                        ClassId = Item.ID.ToString(),
                        Source = "keyhole.png",
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        BackgroundColor = Color.FromHex("#649FD4"),
                        Margin = 5

                    };

                    TGR2.Tapped += (s, e) => {
                        IsEnabled = false;
                        UnlockComponent(s, e);
                        IsEnabled = true;
                    };

                    IMG2.GestureRecognizers.Add(TGR2);
                    AvatarButtonsGrid.Children.Add(IMG2, Column, Row);
                }




                Column++;
            }




        }

        async void UnlockComponent(object sender, EventArgs e)
        {
            var Button = (Image)sender;
            var id = Convert.ToInt32(Button.ClassId);
            var item = App.database.GetItemFromID(id).First();
            int tokenNumber = item.Price;
            bool answer = await DisplayAlert("", "Vill du låsa upp "+ item.Descriptions+" för " + tokenNumber + " mynt?", "Nej", "Ja");
            if (!answer)
            {
                
                if (App.LoggedinUser.Plustokens >= tokenNumber)
                {
                    //Unlocks item
                    Button.IsEnabled = false;
                    Button.IsVisible = false;
                    App.database.Plustoken(App.LoggedinUser, -tokenNumber);
                    Inventory.Add(Convert.ToInt32(id));

                    App.LoggedinUser.Inventory = JsonConvert.SerializeObject(Inventory);
                    App.database.UpdateAvatarItems(App.LoggedinUser);


                } else
                {
                    await DisplayAlert("", "Inte tillräckligt mynt. Du har bara " + App.LoggedinUser.Plustokens + ". Du behöver " + (tokenNumber - App.LoggedinUser.Plustokens) + " mynt till.", "Okej.");
                }
            }
        }

        public void ChangeFace(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            ProfilePictureFace.Source = image.Source;
            var PP = (ProfilePage)App.Mainpage.Children[2];
            PP.updateAvatar(ProfilePictureHair.Source, ProfilePictureBody.Source, ProfilePictureFace.Source);

            Avatar[0] = image.ClassId;
            App.LoggedinUser.Avatar = JsonConvert.SerializeObject(Avatar);
            App.database.UpdateAvatarItems(App.LoggedinUser);
        }

        public void ChangeHair(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            ProfilePictureHair.Source = image.Source;
            var PP = (ProfilePage)App.Mainpage.Children[2];
            PP.updateAvatar(ProfilePictureHair.Source, ProfilePictureBody.Source, ProfilePictureFace.Source);

            Avatar[1] = image.ClassId;
            App.LoggedinUser.Avatar = JsonConvert.SerializeObject(Avatar);
            App.database.UpdateAvatarItems(App.LoggedinUser);
        }
        public void ChangeBody(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            ProfilePictureBody.Source = image.Source;
            var PP = (ProfilePage)App.Mainpage.Children[2];
            PP.updateAvatar(ProfilePictureHair.Source, ProfilePictureBody.Source, ProfilePictureFace.Source);

            Avatar[2] = image.ClassId;
            App.LoggedinUser.Avatar = JsonConvert.SerializeObject(Avatar);
            App.database.UpdateAvatarItems(App.LoggedinUser);
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