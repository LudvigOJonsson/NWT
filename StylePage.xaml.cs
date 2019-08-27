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
    public partial class StylePage : ContentPage
    {
        //int Rownr = 1;

        public Label Label = new Label { };
        public Image Image = new Image { };
        public Button Button = new Button { };

        public List<int> StylesInventory = new List<int>();
        public string ActiveStyle;

        public StylePage()
        {

            InitializeComponent();

            if (App.LoggedinUser.TutorialProgress == 3)
            {
                App.LoggedinUser.TutorialProgress = 4;
                App.database.UpdateTutorialProgress(App.LoggedinUser);
            }


            /*StylesInventory = JsonConvert.DeserializeObject<List<int>>(App.LoggedinUser.Inventory);
            ActiveStyle = JsonConvert.DeserializeObject<string>(App.LoggedinUser.Avatar);

            var StylesList = App.database.GetAllStyles();

            var Row = 1;

            foreach (var Style in StylesList)
            {
                var Button = new Button
                {
                    ClassId = Style.ID,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    BackgroundColor = Style.Color,
                    Margin = 5,
                };

                var TGR = new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1
                };
                TGR.Tapped += (s, e) => {
                    IsEnabled = false;
                    ColorFunction(Style.Color);
                    IsEnabled = true;
                };
                Button.GestureRecognizers.Add(TGR);
                StyleGrid.Children.Add(Button, 0, 3, Row, Row + 1);

                if (!StylesInventory.Contains(Style.ID))
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
                        ClassId = Style.ID.ToString(),
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
                        ClassId = Style.ID.ToString(),
                        Source = "keyhole.png",
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        BackgroundColor = Color.Transparent,
                        Margin = 60

                    };

                    TGR2.Tapped += (s, e) => {
                        IsEnabled = false;
                        UnlockComponent(s, e);
                        IsEnabled = true;
                    };

                    IMG2.GestureRecognizers.Add(TGR2);

                    StyleGrid.Children.Add(IMG3, Column, Row);
                    StyleGrid.Children.Add(IMG2, Column, Row);
                }




                Row++;
            }*/
        }

        /*async void UnlockComponent(object sender, EventArgs e)
        {
            var Button = (Image)sender;
            var id = Convert.ToInt32(Button.ClassId);
            var Style = App.database.GetStyleFromID(id).First();
            int tokenNumber = Style.Price;
            bool answer = await DisplayAlert("", "Vill du låsa upp " + Style.Descriptions + " för " + tokenNumber + " mynt?", "Nej", "Ja");
            if (!answer)
            {

                if (App.LoggedinUser.Plustokens >= tokenNumber)
                {
                    //Unlocks item
                    Button.IsEnabled = false;
                    Button.IsVisible = false;
                    App.database.Plustoken(App.LoggedinUser, -tokenNumber);
                    StylesInventory.Add(Convert.ToInt32(id));

                    App.LoggedinUser.Inventory = JsonConvert.SerializeObject(StylesInventory);
                    App.database.UpdateAvatarItems(App.LoggedinUser);


                }
                else
                {
                    await DisplayAlert("", "Inte tillräckligt mynt. Du har bara " + App.LoggedinUser.Plustokens + ". Du behöver " + (tokenNumber - App.LoggedinUser.Plustokens) + " mynt till.", "Okej.");
                }
            }
        }*/

        public void SelectStyle(object sender, EventArgs e)
        {

            var Sender = (Button)sender;
            var Style = Sender.ClassId;

            if(Style == "Blue")
            {
                ColorFunction(Color.FromHex("#649FD4"));
            }
            else if (Style == "Green")
            {
                ColorFunction(Color.FromHex("#6fb110"));
            }
            else if (Style == "LightBlue")
            {
                ColorFunction(Color.FromHex("#649FD4"));
            }
            else if (Style == "Purple")
            {
                ColorFunction(Color.FromHex("#bb0066"));
            }
            else if (Style == "White")
            {
                ColorFunction(Color.FromHex("#e0d8b3"));
            }
            else
            {
                ColorFunction(Color.FromHex(Sender.ClassId));
            }



            //NEW STYLE SELECTED

            //UPDATE STYLE ON LEAVE PAGE, or straight away?

            //LOADING animation?
        }

        public void ColorFunction(Color BGC)
        {

            var BC = BGC;
            var CM = App.SideMenu;
            var MP = App.Mainpage;
            var SP = App.Startpage;
            var CNF = (CustomNewsFeed)MP.Children[0];
            var NF = (NewsGridPage)MP.Children[1];
            var PP = (ProfilePage)MP.Children[2];

            CM.DownButton.BackgroundColor = BC;
            CM.DownButton.Color = BC;
            MP.BackgroundColor = BC;
            MP.BarBackgroundColor = BC;
            SP.Detail.BackgroundColor = BC;


            CNF.Down.BackgroundColor = BC;


            NF.Down.BackgroundColor = BC;

            SP.Master.BackgroundColor = BC;
            SP.Detail.BackgroundColor = BC;

            PP.PE1.BackgroundColor = BC;
            PP.PE1.Color = BC;
            // PP.PE2.TextColor = BC;
            //PP.PE3.TextColor = BC;
            //PP.PE4.TextColor = BC;
            //PP.PE5.TextColor = BC;
            //PP.PE6.TextColor = BC;
            PP.FavoritesButton.BackgroundColor = BC;
           // PP.AvatarButton.BackgroundColor = BC;
            PP.HistoryButton.BackgroundColor = BC;
            PP.StyleButton.BackgroundColor = BC;
            PP.AchievementsButton.BackgroundColor = BC;
            PP.ProfileSettingsButton.BackgroundColor = BC;
            PP.m1bx.BackgroundColor = BC;
            PP.m2bx.BackgroundColor = BC;
            PP.m3bx.BackgroundColor = BC;

            App.MC = BC;

            

            NF.PrintNews();
            App.Startpage.Detail = new NavigationPage(App.Mainpage) { BarBackgroundColor = BC, BarTextColor = Color.FromHex("#FFFFFF"), };
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
        /*
        public void MakeButton(string s, int Type)
        {
            var Button = new StyleButton();


            StyleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            StyleGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });

            StyleGrid.Children.Add(Button.Label, 2, 7, Rownr, Rownr + 1); //Boxview
            StyleGrid.Children.Add(Button.Button, 1, 7, Rownr, Rownr + 1); //Label
            StyleGrid.Children.Add(Button.Image, 1, 2, Rownr, Rownr + 1); //Label
        }

        public class StyleButton
        {
            public Button Button = new Button { };
            public Label Label = new Label { };
            public Image Image = new Image { };

            public string style = "";

            public StyleButton()
            {
                Label = new Label
                {
                    Text = "",
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Start,
                    FontSize = 25,
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Color.Black,
                    Margin = 15,
                    ClassId = style,
                };

                //Label.GestureRecognizers.Add(TGR);

                Image = new Image
                {

                    Source = "",
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    Aspect = Aspect.AspectFill,
                    Margin = 0,
                    ClassId = style,

                };

                //Image.GestureRecognizers.Add(TGR);

                Button = new Button
                {
                    BackgroundColor = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Margin = 0,
                    ClassId = style,
                };

                //Button.GestureRecognizers.Add(TGR);
            }
        }*/

    }
}