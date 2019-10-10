using FFImageLoading.Forms;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
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

        public Image selectedImage;

        public AvatarPage()
        {

            InitializeComponent();
            /*
            Avatar.Add("");
            Avatar.Add("");
            Avatar.Add("");

            SetDefaultAvatar();

            */

            var PP = (ProfilePage)App.Mainpage.Children[2];

            //Make the avatar picture into whatever pieces are saved on the user's account.


            if (App.LoggedinUser.TutorialProgress == 1)
            {
                App.LoggedinUser.TutorialProgress = 2;
                App.database.UpdateTutorialProgress(App.LoggedinUser);
            }
            Inventory = JsonConvert.DeserializeObject<List<int>>(App.LoggedinUser.Inventory);
            Avatar = JsonConvert.DeserializeObject<List<string>>(App.LoggedinUser.Avatar);

            if(Avatar.Count != 5)
            {
                SetDefaultAvatar();
            }

            UpdateAvatar();
            Face.BackgroundColor = App.MC;
            Hair.BackgroundColor = App.MC;
            Body.BackgroundColor = App.MC;
            Nr4.BackgroundColor = App.MC;
            Option1.BackgroundColor = App.MC;
            Option2.BackgroundColor = App.MC;
            Option3.BackgroundColor = App.MC;
            Option4.BackgroundColor = App.MC;

            var ItemList = App.database.GetAllItems();

            var Row = 3;
            var Column = 0;
            /*
            foreach(var Item in ItemList)
            {
                if(Item.InventorySlot != "Style" && Item.InventorySlot != "Expr" && Item.InventorySlot != "Beard")
                { 
                    if(Column == 5)
                    {
                        Column = 0;
                        Row++;
                        
                    }
                    var IMG = new CachedImage
                    {
                        ClassId = Item.ImagePath,
                        Source = Item.ImagePath,
                        HorizontalOptions = LayoutOptions.CenterAndExpand, 
                        VerticalOptions = LayoutOptions.CenterAndExpand, 
                        BackgroundColor = Color.FromHex("#649FD4") ,
                        Margin = 5,
                        CacheDuration = TimeSpan.FromDays(14),
                        DownsampleToViewSize = false,
                        RetryCount = 1,
                        RetryDelay = 250,
                        BitmapOptimizations = false,
                        LoadingPlaceholder = "",
                        ErrorPlaceholder = "failed_load.png",
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

                    if (!Inventory.Contains(Item.ID) && Column != 0)
                    {
                        var TGR3 = new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1
                        };

                        var TGR2 = new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1
                        };

                        var IMG3 = new Image
                        {
                            ClassId = Item.ID.ToString(),
                            Source = "",
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = Color.Transparent,
                            Margin = 0

                        };

                        TGR3.Tapped += (s, e) => {
                            IsEnabled = false;
                            UnlockComponent(s, e);
                            IsEnabled = true;
                        };

                        IMG3.GestureRecognizers.Add(TGR2);

                        var IMG2 = new Image
                        {
                            ClassId = Item.ID.ToString(),
                            Source = "Icon_Keyhole2.png",
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = Color.Transparent,
                            Margin = 0

                        };

                        TGR2.Tapped += (s, e) => {
                            IsEnabled = false;
                            UnlockComponent(s, e);
                            IsEnabled = true;
                        };

                        IMG2.GestureRecognizers.Add(TGR2);

                        AvatarButtonsGrid.Children.Add(IMG3, Column, Row);
                        AvatarButtonsGrid.Children.Add(IMG2, Column, Row);
                    }




                    Column++;
                }
            }
            */



        }

        void OpenMenu(object sender, EventArgs e)
        {
            UpdateAvatar();
            var Image = (Image)sender;
            var id = Image.ClassId;
            string Op1 = "";
            string Op2 = "";
            string Op3 = "";
            string Op4 = "";
            string Op1Source = "";
            string Op2Source = "";
            string Op3Source = "";
            string Op4Source = "";
            Color DC = App.MC;

            switch (id)
            {
                case "Face":
                    Op1 = "Face";
                    Op2 = "Expr";
                    Op3 = "FacialFeats";
                    Op4 = "Undef";
                    Op1Source = Avatar[0];
                    Op2Source = "icon_face";
                    Op3Source = "";
                    Op4Source = "";
                    DC = App.MC;
                    break;
                case "Hair":
                    Op1 = "Hair";
                    Op2 = "Moustach";
                    Op3 = "Beard";
                    Op4 = "Hat";
                    Op1Source = "icon_hair";
                    Op2Source = "icon_moustaches";
                    Op3Source = "icon_beard";
                    Op4Source = "icon_hats";
                    DC = App.MC;
                    break;
                case "Body":
                    Op1 = "Ct1";
                    Op2 = "Ct2";
                    Op3 = "Ct3";
                    Op4 = "Ct4";
                    Op1Source = "icon_clothing4";
                    Op2Source = "icon_clothing2";
                    Op3Source = "icon_clothing3";
                    Op4Source = "icon_clothing";
                     DC = App.MC;
                    break;
                case "Nr4":
                    DC = App.MC;
                    break;
               
            }

            if (selectedImage != Image)
            {
                ResetRowOneButtons();
                ResetRowTwoButtons();
                ItemsGrid.Children.Clear();
                //ItemsGrid.IsVisible = false;
                Image.BackgroundColor = Color.Gray;

                Option1.ClassId = Op1;
                Option2.ClassId = Op2;
                Option3.ClassId = Op3;
                Option4.ClassId = Op4;
                Option1.Source = Op1Source;
                Option2.Source = Op2Source;
                Option3.Source = Op3Source;
                Option4.Source = Op4Source;
                Option1.IsVisible = true;
                Option2.IsVisible = true;
                Option3.IsVisible = true;
                Option4.IsVisible = true;

                selectedImage = Image;
            }
            else
            {
                Image.BackgroundColor = App.MC;
                Option1.ClassId = "";
                Option2.ClassId = "";
                Option3.ClassId = "";
                Option4.ClassId = "";
                Option1.IsVisible = false;
                Option2.IsVisible = false;
                Option3.IsVisible = false;
                Option4.IsVisible = false;
                ItemsGrid.Children.Clear();
                //ItemsGrid.IsVisible = false;

                selectedImage = null;
            }
        }

        void OpenSubMenu(object sender, EventArgs e)
        {
            UpdateAvatar();
            var Image = (Image)sender;
            var id = Image.ClassId;

            Color DC = App.MC;
            ItemsGrid.Children.Clear();
            switch (id)
            {
                case "Face":                  
                    LoadFace();
                    DC = App.MC;
                    break;
                case "Expr":
                    LoadCategory("Expr");
                    DC = App.MC;
                    break;
                case "FacialFeats":

                    DC = App.MC;
                    break;
                case "Undef":
                    DC = App.MC;
                    break;
                case "Hair":
                    LoadCategory("Hair");
                    DC = App.MC;
                    break;
                case "Moustach":

                    DC = App.MC;
                    break;
                case "Beard":
                    LoadCategory("Beard");
                    DC = App.MC;
                    break;
                case "Hat":
                    LoadCategory("Hat");
                    DC = App.MC;
                    break;
                case "Ct1":
                    LoadCategory("Ct1");
                    DC = App.MC;
                    break;
                case "Ct2":
                    LoadCategory("Ct2");
                    DC = App.MC;
                    break;
                case "Ct3":
                    LoadCategory("Ct3");
                    DC = App.MC;
                    break;
                case "Ct4":
                    LoadCategory("Ct4");
                    DC = App.MC;
                    break;


            }


            if (selectedImage != Image)
            {
                ResetRowTwoButtons();
                Image.BackgroundColor = Color.Gray;
                Console.WriteLine("ItemGrid Visible");
                //ItemsGrid.IsVisible = true;
                Console.WriteLine(ItemsGrid.Children.Count);

                selectedImage = Image;
            }
            else
            {
                Image.BackgroundColor = App.MC;
                Console.WriteLine("ItemGrid Not Visible");
                ItemsGrid.Children.Clear();
                //ItemsGrid.IsVisible = false;

                selectedImage = null;
            }

        }



        void ResetRowOneButtons()
        {
            Face.BackgroundColor = App.MC;
            Hair.BackgroundColor = App.MC;
            Body.BackgroundColor = App.MC;
            Nr4.BackgroundColor = App.MC;
        }
        void ResetRowTwoButtons()
        {
            Option1.BackgroundColor = App.MC;
            Option2.BackgroundColor = App.MC;
            Option3.BackgroundColor = App.MC;
            Option4.BackgroundColor = App.MC;
            Console.WriteLine("Row 2 Reset.");
        }

        void LoadFace()
        {
            
            Console.WriteLine("Loading Face");
            var TGR = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1
                
            };
            
            TGR.Tapped += (s, e) => {
                IsEnabled = false;
                ChangeFace(s, e);
                IsEnabled = true;
            };
            


            var face1 = new Image
            {
                ClassId = "avatar_face1.png",
                Source = "avatar_face1.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = App.MC,
                Margin = 5,
            };
            var face2 = new Image
            {
                ClassId = "avatar_face2.png",
                Source = "avatar_face2.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = App.MC,
                Margin = 5,
            };
            var face3 = new Image
            {
                ClassId = "avatar_face3.png",
                Source = "avatar_face3.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = App.MC,
                Margin = 5,
            };
            var face4 = new Image
            {
                ClassId = "avatar_face4.png",
                Source = "avatar_face4.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = App.MC,
                Margin = 5,
            };
            var face5 = new Image
            {
                ClassId = "avatar_face5.png",
                Source = "avatar_face5.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = App.MC,
                Margin = 5,
            };


            face1.GestureRecognizers.Add(TGR);
            face2.GestureRecognizers.Add(TGR);
            face3.GestureRecognizers.Add(TGR);
            face4.GestureRecognizers.Add(TGR);
            face5.GestureRecognizers.Add(TGR);

            ItemsGrid.Children.Add(face1, 0, 0);
            ItemsGrid.Children.Add(face2, 1, 0);
            ItemsGrid.Children.Add(face3, 2, 0);
            ItemsGrid.Children.Add(face4, 3, 0);
            ItemsGrid.Children.Add(face5, 4, 0);
            ItemsGrid.IsVisible = true;
            Console.WriteLine("Face Loaded");
        }

        void LoadCategory(string cat)
        {
            var Row = 0;
            var Column = 0;

            var ItemList = App.database.GetItemFromCategory(cat);
            ItemsGrid.RowDefinitions.Clear();
            ItemsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            foreach (var Item in ItemList)
            {
                if(Item.InventorySlot != "Style")
                { 
                    if(Column == 5)
                    {
                        Column = 0;
                        ItemsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star});
                        Row++;
                        
                    }
                    var IMG = new CachedImage
                    {
                        ClassId = Item.ImagePath,
                        Source = Item.ImagePath,
                        HorizontalOptions = LayoutOptions.CenterAndExpand, 
                        VerticalOptions = LayoutOptions.CenterAndExpand, 
                        BackgroundColor = App.MC,
                        Margin = 5,
                        CacheDuration = TimeSpan.FromDays(14),
                        DownsampleToViewSize = false,
                        RetryCount = 1,
                        RetryDelay = 250,
                        BitmapOptimizations = false,
                        LoadingPlaceholder = "",
                        ErrorPlaceholder = "failed_load.png",
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
                    else if (Item.InventorySlot == "Expr")
                    {
                        TGR.Tapped += (s, e) => {
                            IsEnabled = false;
                            ChangeExpr(s, e);
                            IsEnabled = true;
                        };
                    }
                    else if (Item.InventorySlot == "Beard")
                    {
                        TGR.Tapped += (s, e) => {
                            IsEnabled = false;
                            ChangeBeard(s, e);
                            IsEnabled = true;
                        };
                    }
                    IMG.GestureRecognizers.Add(TGR);
                    ItemsGrid.Children.Add(IMG, Column, Row);

                    if (!Inventory.Contains(Item.ID) && Column != 0 && Row != 0 && Row != 1)
                    {
                        var TGR3 = new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1
                        };

                        var TGR2 = new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1
                        };

                        var IMG3 = new Image
                        {
                            ClassId = Item.ID.ToString(),
                            Source = "",
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,  
                            BackgroundColor = Color.Transparent,
                            Margin = 0

                        };

                        TGR3.Tapped += (s, e) => {
                            IsEnabled = false;
                            UnlockComponent(s, e);
                            IsEnabled = true;
                        };

                        IMG3.GestureRecognizers.Add(TGR2);

                        var IMG2 = new Image
                        {
                            ClassId = Item.ID.ToString(),
                            Source = "Icon_Keyhole2.png",
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = Color.Transparent,
                            Margin = 0

                        };

                        TGR2.Tapped += (s, e) => {
                            IsEnabled = false;
                            UnlockComponent(s, e);
                            IsEnabled = true;
                        };

                        IMG2.GestureRecognizers.Add(TGR2);

                        ItemsGrid.Children.Add(IMG3, Column, Row);
                        ItemsGrid.Children.Add(IMG2, Column, Row);
                    }




                    Column++;
                }
            }
        }

        public void UpdateAvatar()
        {
            //Updating the display avatar
            ProfilePictureFace.Source = Avatar[0];
            ProfilePictureHair.Source = Avatar[1];
            ProfilePictureBody.Source = Avatar[2];
            ProfilePictureExpr.Source = Avatar[3];
            ProfilePictureBeard.Source = Avatar[4];

            //Updating the category buttons
            /*Face.Source = Avatar[0];
            Hair.Source = Avatar[1];
            Body.Source = Avatar[2];
            Nr4.Source = "";*/
        }

        void SetDefaultAvatar()
        {
            while(Avatar.Count < 5)
            {
                Avatar.Add("");
            }



            Avatar[0] = "avatar_face1.png";
            Avatar[1] = "avatar_hair1.png";
            Avatar[2] = "avatar_body1.png";
            Avatar[3] = "avatar_expr4.png";
            Avatar[4] = "nothing.png";
                
            App.LoggedinUser.Avatar = JsonConvert.SerializeObject(Avatar);
            App.database.UpdateAvatarItems(App.LoggedinUser);
        }


        async void UnlockComponent(object sender, EventArgs e)
        {
            var Button = (Image)sender;
            var id = Convert.ToInt32(Button.ClassId);
            var item = App.database.GetItemFromID(id).First();
            int tokenNumber = item.Price;

            AvatarPopup ap = new AvatarPopup();
            ap.UpdatePreview(Button, tokenNumber, this, id, item.ImagePath);
            await PopupNavigation.Instance.PushAsync(ap);

            /*bool answer = await DisplayAlert("", "Vill du låsa upp "+ item.Descriptions+" för " + tokenNumber + " mynt?", "Nej", "Ja");
            if (!answer)
            {
                
                if (App.LoggedinUser.Plustokens >= tokenNumber)
                {
                    


                } else
                {
                    await DisplayAlert("", "Inte tillräckligt mynt. Du har bara " + App.LoggedinUser.Plustokens + ". Du behöver " + (tokenNumber - App.LoggedinUser.Plustokens) + " mynt till.", "Okej.");
                }
            }*/
        }
        public void UnlockForReal(Image button, int id, int cost)
        {
            
            var item = App.database.GetItemFromID(id).First();

            //Unlocks item
            button.IsEnabled = false;
            button.IsVisible = false;
            App.database.Plustoken(App.LoggedinUser, -cost);
            Inventory.Add(Convert.ToInt32(id));

            App.LoggedinUser.Inventory = JsonConvert.SerializeObject(Inventory);
            App.database.UpdateAvatarItems(App.LoggedinUser);
        }

        public void ChangeFace(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            ProfilePictureFace.Source = image.Source;
            var PP = (ProfilePage)App.Mainpage.Children[2];
            PP.updateAvatar(ProfilePictureHair.Source, ProfilePictureBody.Source, ProfilePictureFace.Source, ProfilePictureExpr.Source, ProfilePictureBeard.Source);

            Avatar[0] = image.ClassId;
            App.LoggedinUser.Avatar = JsonConvert.SerializeObject(Avatar);
            App.database.UpdateAvatarItems(App.LoggedinUser);
        }

        public void ChangeHair(object sender, EventArgs e)
        {
            CachedImage image = (CachedImage)sender;
            ProfilePictureHair.Source = image.Source;
            var PP = (ProfilePage)App.Mainpage.Children[2];
            PP.updateAvatar(ProfilePictureHair.Source, ProfilePictureBody.Source, ProfilePictureFace.Source, ProfilePictureExpr.Source, ProfilePictureBeard.Source);

            Avatar[1] = image.ClassId;
            App.LoggedinUser.Avatar = JsonConvert.SerializeObject(Avatar);
            App.database.UpdateAvatarItems(App.LoggedinUser);
        }
        public void ChangeBody(object sender, EventArgs e)
        {
            CachedImage image = (CachedImage)sender;
            ProfilePictureBody.Source = image.Source;
            var PP = (ProfilePage)App.Mainpage.Children[2];
            PP.updateAvatar(ProfilePictureHair.Source, ProfilePictureBody.Source, ProfilePictureFace.Source, ProfilePictureExpr.Source, ProfilePictureBeard.Source);

            Avatar[2] = image.ClassId;
            App.LoggedinUser.Avatar = JsonConvert.SerializeObject(Avatar);
            App.database.UpdateAvatarItems(App.LoggedinUser);
        }
        public void ChangeExpr(object sender, EventArgs e)
        {
            CachedImage image = (CachedImage)sender;
            ProfilePictureExpr.Source = image.Source;
            var PP = (ProfilePage)App.Mainpage.Children[2];
            PP.updateAvatar(ProfilePictureHair.Source, ProfilePictureBody.Source, ProfilePictureFace.Source, ProfilePictureExpr.Source, ProfilePictureBeard.Source);

            Avatar[3] = image.ClassId;
            App.LoggedinUser.Avatar = JsonConvert.SerializeObject(Avatar);
            App.database.UpdateAvatarItems(App.LoggedinUser);
        }
        public void ChangeBeard(object sender, EventArgs e)
        {
            CachedImage image = (CachedImage)sender;
            ProfilePictureBeard.Source = image.Source;
            var PP = (ProfilePage)App.Mainpage.Children[2];
            PP.updateAvatar(ProfilePictureHair.Source, ProfilePictureBody.Source, ProfilePictureFace.Source, ProfilePictureExpr.Source, ProfilePictureBeard.Source);

            Avatar[4] = image.ClassId;
            App.LoggedinUser.Avatar = JsonConvert.SerializeObject(Avatar);
            App.database.UpdateAvatarItems(App.LoggedinUser);
        }
        public async void Question(object sender, EventArgs e)
        {
            await DisplayAlert("Din Avatar", "Här kan du köpa utstyrsal till din avatar, eller pröva olika ansiksuttryck och hårstilar!", "Okay");
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