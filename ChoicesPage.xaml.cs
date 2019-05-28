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
    public partial class ChoicesPage : ContentPage
    {

        public string Filter = "";
        public string Author = "";
        public string Tag = "";
        List<string> tempList = new List<string>();
        int Rownr = 1;

        public ChoicesPage()
        {

            InitializeComponent();

            //-------------------------------------------------------------

            //adding to list.
            tempList.Add("Brått");
            tempList.Add("Mat");
            tempList.Add("Fika");
            tempList.Add("Fika2");
            tempList.Add("Fika3");
            tempList.Add("Mat");
            tempList.Add("Fika");
            tempList.Add("Fika2");
            tempList.Add("Fika3");
            tempList.Add("Mat");
            tempList.Add("Fika");
            tempList.Add("Fika2");
            tempList.Add("Fika3");
            tempList.Add("Mat");
            tempList.Add("Fika");
            tempList.Add("Fika2");
            tempList.Add("Fika3");
            tempList.Add("Mat");
            tempList.Add("Fika");
            tempList.Add("Fika2");
            tempList.Add("Fika3");
            tempList.Add("Mat");
            tempList.Add("Fika");
            tempList.Add("Fika2");
            tempList.Add("Fika3");
            tempList.Add("Mat");
            tempList.Add("Fika");
            tempList.Add("Fika2");
            tempList.Add("Fika3");
            tempList.Add("Fika4");
            tempList.Add("Fika5");
            tempList.Add("Fika6");

            foreach (string s in tempList)
            {
                var Box = new Subject();
                Box.Label.Text = s;

                NewsGridOri.RowDefinitions.Add(new RowDefinition { });
                NewsGridOri.RowDefinitions.Add(new RowDefinition { Height = 1 });


                NewsGridOri.Children.Add(Box.Label, 1, 8, Rownr, Rownr + 1); //Label
                NewsGridOri.Children.Add(Box.BellImage, 6, 7, Rownr, Rownr + 1); //Label
                NewsGridOri.Children.Add(Box.TrashImage, 7, 8, Rownr, Rownr + 1); //Boxview
                NewsGridOri.Children.Add(Box.Box, 1, 8, Rownr + 1, Rownr + 2); //Boxview


                Rownr++;
                Rownr++;

            }
        }

        public class Subject
        {
            public Button Box = new Button { };
            public Label Label = new Label { };
            public Image TrashImage = new Image { };
            public Image BellImage = new Image { };

            public Subject()
            {

                Label = new Label
                {
                    Text = "Ämne",
                    FontSize = 20,
                    TextColor = Color.Black,
                    BackgroundColor = Color.White,
                    VerticalOptions = LayoutOptions.Center,

                };

                //Label.GestureRecognizers.Add(TGR);

                Box = new Button
                {
                    BackgroundColor = Color.LightGray,
                };

                TrashImage = new Image
                {
                    Source = "trash.png",
                    WidthRequest = 10,
                    HeightRequest = 10,
                };

                BellImage = new Image
                {
                    Source = "bell.png",
                    WidthRequest = 10,
                    HeightRequest = 10,
                };

            }
        }

        //-------------------------------------------------------------

        public void SetKategori(object sender, EventArgs e)
        {
            var Sender = (Button)sender;
            Filter = Sender.ClassId;
        }
        public void SetTag(object sender, EventArgs e)
        {
            var Sender = (Button)sender;
            Tag = Sender.ClassId;
        }
        public void SetAuthor(object sender, EventArgs e)
        {
            var Sender = (Button)sender;
            Author = Sender.ClassId;
        }


        public void Clear(object sender, EventArgs e)
        {
            Filter = "";
            Author = "";
            Tag = "";
        }


        public void PrintNews(object sender, EventArgs e)
        {

            ButtonLock();
            NewsGridPage Page = (NewsGridPage)App.Mainpage.Children[1];
            App.database.LocalExecute("DELETE FROM NF");

            Page.PREV = 0;
            Page.CURR = NewsGridPage.DBLN;
            Page.NEXT = NewsGridPage.DBLN * 2;
            Page.Loadnr = 1;
            Page.Filter = Filter;
            Page.Author = Author;
            Page.Tag = Tag;

            Page.ArticleList.Clear();
            Page.LoadLocalDB();
            Page.AddNews(0);
            ButtonLock();

        }

        public void ButtonLock()
        {
            /*Nyheter.IsEnabled = !Nyheter.IsEnabled;
            Sport.IsEnabled = !Sport.IsEnabled;
            Ekonomi.IsEnabled = !Ekonomi.IsEnabled;
            NöjeochKultur.IsEnabled = !NöjeochKultur.IsEnabled; 
            Åsikter.IsEnabled = !Åsikter.IsEnabled;
            Familj.IsEnabled = !Familj.IsEnabled;*/
            UserSettingsB.IsEnabled = !UserSettingsB.IsEnabled;
            AboutB.IsEnabled = !AboutB.IsEnabled;
            LogoutB.IsEnabled = !LogoutB.IsEnabled;

            /*Tibro.IsEnabled = !Tibro.IsEnabled;
            Skövde.IsEnabled = !Skövde.IsEnabled;
            Falkköping.IsEnabled = !Falkköping.IsEnabled;
            Karlsborg.IsEnabled = !Karlsborg.IsEnabled;
            Rensa.IsEnabled = !Rensa.IsEnabled;
            Verkställ.IsEnabled = !Verkställ.IsEnabled;
            */

        }


        async void Logout(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            ProfilePage page = (ProfilePage)App.Mainpage.Children[2];
            page.Logout();
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            ButtonLock();
        }
        async void About(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new AboutPage());
            ButtonLock();
        }
        async void UserSettings(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new UserSettingsPage());
            ButtonLock();
        }

    }
}