﻿using Rg.Plugins.Popup.Services;
using Lottie.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace NWT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
        public bool M1HI = false;
        public bool M2HI = false;
        public bool M3HI = false;
        public int M1T = 0;
        public int M2T = 0;
        public int M3T = 0;

        public Image avatarHairPic;
        public Image avatarBodyPic;
        public Image avatarFacePic;
        



        public ProfilePage ()
		{
			InitializeComponent ();

         
            avatarFacePic = ProfilePictureFace;
            avatarHairPic = ProfilePictureHair;
            avatarBodyPic = ProfilePictureBody;

            


        }
        void ProgressBallClicked1(object sender, EventArgs e)
        {
            if (App.LoggedinUser.TutorialProgress == 5)
            {
                ChangeIntroStep(App.LoggedinUser.TutorialProgress);
            }
            else
            {
                //await DisplayAlert("Anpassa dina nyheter!", "Okej", "");
            }
        }
        void ProgressBallClicked2(object sender, EventArgs e)
        {
            if (App.LoggedinUser.TutorialProgress == 5)
            {
                ChangeIntroStep(App.LoggedinUser.TutorialProgress);
            }
            else
            {
                //await DisplayAlert("Anpassa din avatar!", "Okej", "");
            }
        }
        void ProgressBallClicked3(object sender, EventArgs e)
        {
            if (App.LoggedinUser.TutorialProgress == 5)
            {
                ChangeIntroStep(App.LoggedinUser.TutorialProgress);
            }
            else
            {
                //await DisplayAlert("Anpassa din stil!", "Okej", "");
            }
        }
        public void ChangeIntroStep(int step)
        {
            if (step == 1)
            {
                progressBallCheckmark1.IsVisible = true;
                progressBallCheckmark2.IsVisible = false;
                progressBallCheckmark3.IsVisible = false;

            }
            if (step == 2)
            {
                PopupNavigation.Instance.PushAsync(new TutorialPopUp2());
                App.LoggedinUser.TutorialProgress = 3;
                progressBallCheckmark1.IsVisible = true;
                progressBallCheckmark2.IsVisible = true;
                progressBallCheckmark3.IsVisible = false;

            }
            if (step == 4)
            {
                PopupNavigation.Instance.PushAsync(new TutorialPopUp3());
                App.LoggedinUser.TutorialProgress = 5;
                progressBallCheckmark1.IsVisible = true;
                progressBallCheckmark2.IsVisible = true;
                progressBallCheckmark3.IsVisible = true;
                IntroBackground.Color = Color.Green;

            }
            if (step == 5)
            {
                PopupNavigation.Instance.PushAsync(new TutorialPopUp4());
                App.LoggedinUser.TutorialProgress = 6;
                RemoveIntro();
            }
            if (step == 6)
            {
                RemoveIntro();
            }
            App.database.UpdateTutorialProgress(App.LoggedinUser);
        }
        void RemoveIntro()
        {
            IntroBackground.IsVisible = false;
            progressBallCheckmark1.IsVisible = false;
            progressBallCheckmark2.IsVisible = false;
            progressBallCheckmark3.IsVisible = false;
            progressBall1.IsVisible = false;
            progressBall2.IsVisible = false;
            progressBall3.IsVisible = false;
            progressBallCogwheel.IsVisible = false;
            progressBallProfile.IsVisible = false;
            progressBallNews.IsVisible = false;
            IntroText.IsVisible = false;
        }
        void AddIntro()
        {
            IntroBackground.IsVisible = true;
            progressBallCheckmark1.IsVisible = true;
            progressBallCheckmark2.IsVisible = true;
            progressBallCheckmark3.IsVisible = true;
            progressBall1.IsVisible = true;
            progressBall2.IsVisible = true;
            progressBall3.IsVisible = true;
            progressBallCogwheel.IsVisible = true;
            progressBallProfile.IsVisible = true;
            progressBallNews.IsVisible = true;
            IntroText.IsVisible = true;
        }

        public void Login(UserTable User)
        {


                Welcome.Text = "Hej, " + User.Username + "!";
                TokenNumber.Text = App.LoggedinUser.Plustokens.ToString();
                updateMissions();



            var Avatar = JsonConvert.DeserializeObject<List<string>>(App.LoggedinUser.Avatar);
            updateAvatar(Avatar[0], Avatar[1], Avatar[2]);

            //Getting update

            if (App.LoggedinUser.DailyLogin == 0)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await DisplayAlert("Daily Login", "You have logged in " + User.LoginStreak + " days in a row and you get " + User.LoginStreak + " tokens as a reward!", "Nice");

                    await PopupNavigation.Instance.PushAsync(new DailyPopUp());
                });
            }

            ChangeIntroStep(App.LoggedinUser.TutorialProgress);
        }

        public void updateAvatar(ImageSource hair, ImageSource body, ImageSource face)
        {
            ProfilePictureHair.Source = hair;
            ProfilePictureBody.Source = body;
            ProfilePictureFace.Source = face;
        }

        public void updateMissions()
        {
            TokenNumber.Text = App.LoggedinUser.Plustokens.ToString();
            var Tasklist = JsonConvert.DeserializeObject<List<Task>>(App.LoggedinUser.MissionString);

            List<String> missionNames = new List<string>();
            missionNames.Add("");
            missionNames.Add("");
            missionNames.Add("");

            for (int i = 0; i < 3; i++)
            {
                if (Tasklist[i].Type == "ArticlesRead")
                {
                    missionNames[i] = "Artiklar lästa";
                }
                else if (Tasklist[i].Type == "InsandareSubmitted")
                {
                    missionNames[i] = "Insändare skapad";
                }
                else if (Tasklist[i].Type == "InsandareRead")
                {
                    missionNames[i] = "Insändare lästa";
                }
                else if (Tasklist[i].Type == "GameFinished")
                {
                    missionNames[i] = "Spel avklarade";
                }
                else if (Tasklist[i].Type == "QuestionSubmitted")
                {
                    missionNames[i] = "Quiz skapad";
                }
                else if (Tasklist[i].Type == "QuestionAnswered")
                {
                    missionNames[i] = "Quiz svarad";
                }
                else if (Tasklist[i].Type == "VoteQuestionSubmitted")
                {
                    missionNames[i] = "Omröstning skapad";
                }
                else if (Tasklist[i].Type == "VoteSubmitted")
                {
                    missionNames[i] = "Omröstning svarad";
                }
                else if (Tasklist[i].Type == "CommentsPosted")
                {
                    missionNames[i] = "Kommentarer skrivna";
                }
                else if (Tasklist[i].Type == "TokensCollected")
                {
                    missionNames[i] = "Tokens insamlade";
                }
            }

            if (Tasklist[0].Completed == 1)
            {
                m1t1.Text = "Mission Complete";
                m1t2.Text = "";
                m1t3.Text = "Come back tomorrow for new Missions";
                m1.BackgroundColor = Color.FromHex("D3D3D3");
                m1.IsEnabled = false;
                M1HI = false;
            }
            else
            {
                M1T = Convert.ToInt32(Tasklist[0].Goal / Tasklist[0].Modifier);
                m1t1.Text = missionNames[0];
                m1t2.Text = Tasklist[0].Progress + "/" + Tasklist[0].Goal;
                m1t3.Text = "" + M1T +" Tokens";
                if (Tasklist[0].Progress >=  Tasklist[0].Goal)
                {
                    m1.BackgroundColor = Color.FromHex("#649FD4");
                    M1HI = true;
                }
            }
            if (Tasklist[1].Completed == 1)
            {
                m2t1.Text = "Mission Complete";
                m2t2.Text = "";
                m2t3.Text = "Come back tomorrow for new Missions";
                m2.BackgroundColor = Color.FromHex("D3D3D3");
                m2.IsEnabled = false;
                M2HI = false;
            }
            else
            {
                M2T = Convert.ToInt32(Tasklist[1].Goal / Tasklist[1].Modifier);
                m2t1.Text = missionNames[1];
                m2t2.Text = Tasklist[1].Progress + "/" + Tasklist[1].Goal;
                m2t3.Text = "" + M2T + " Tokens";
                if (Tasklist[1].Progress >= Tasklist[1].Goal)
                {
                    m2.BackgroundColor = Color.FromHex("FFDF00");
                    M2HI = true;
                }
            }
            if (Tasklist[2].Completed == 1)
            {
                m3t1.Text = "Mission Complete";
                m3t2.Text = "";
                m3t3.Text = "Come back tomorrow for new Missions";
                m3.BackgroundColor = Color.FromHex("D3D3D3");
                m3.IsEnabled = false;
                M3HI = false;
            }
            else
            {
                M3T = Convert.ToInt32(Tasklist[2].Goal / Tasklist[2].Modifier);
                m3t1.Text = missionNames[2];
                m3t2.Text = Tasklist[2].Progress + "/" + Tasklist[2].Goal;
                m3t3.Text = "" + M3T + " Tokens";
                if (Tasklist[2].Progress >= Tasklist[2].Goal)
                {
                    m3.BackgroundColor = Color.FromHex("FFDF00");
                    M3HI = true;
                }
            }
            
            
            
        }

        public void Evaluate(object sender, EventArgs e)
        {
            if (M1HI || M2HI || M3HI)
            {
                App.database.MissionEvaluation();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await DisplayAlert("Daily Login", "You have logged in " + User.LoginStreak + " days in a row and you get " + User.LoginStreak + " tokens as a reward!", "Nice");

                    await PopupNavigation.Instance.PushAsync(new TutorialPopUp5());
                });
                var T = Convert.ToInt32(TokenNumber.Text);
                if (M1HI)
                {
                    T += M1T;
                }
                if (M2HI)
                {
                    T += M2T;
                }
                if (M3HI)
                {
                    T += M3T;
                }
                TokenNumber.Text = T.ToString();
            }
            else
            {

            }
            updateMissions();
        }

        public void ButtonLock()
        {
            FavoritesButton.IsEnabled = !FavoritesButton.IsEnabled;
            HistoryButton.IsEnabled = !HistoryButton.IsEnabled;
            AchievementsButton.IsEnabled = !AchievementsButton.IsEnabled;
            ProfileSettingsButton.IsEnabled = !ProfileSettingsButton.IsEnabled;
            m1.IsEnabled = !m1.IsEnabled;
            m2.IsEnabled = !m2.IsEnabled;
            m3.IsEnabled = !m3.IsEnabled;
        }

        public void Logout()
        {
            App.database.Logout();
            var NG = (NewsGridPage)App.Mainpage.Children[1];

            foreach (NewsGridPage.Article A in NG.ArticleList)
            {               
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        //A.Box.BorderColor = Color.FromRgb(150, 150, 150);
                    });               
            }
        }
        async void Avatar(object sender, EventArgs e)
        {
            ButtonLock();
            await Navigation.PushAsync(new AvatarPage());
            ButtonLock();
        }
        async void Settings(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new SettingsPage());
            ButtonLock();
        }
        async void Achivements(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            var Stats = App.database.GetUserStats(App.LoggedinUser.ID).First();
            if(Stats != null)
            {
                await Navigation.PushAsync(new AchivementsPage(Stats));
            }
            else
            {
                await DisplayAlert("Article Load Failure", "Troferna misslyckades att laddas in, vänligen försök igen.", "OK");
            }

            
            ButtonLock();
        }
        async void Points(object sender, EventArgs e)
        {

            App.database.Plustoken(App.LoggedinUser, 1);
            var variable = (ProfilePage)App.Mainpage.Children[2];
            variable.TokenNumber.Text = App.LoggedinUser.Plustokens.ToString();

            Button button = (Button)sender;
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
        }
        async void Favorites(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new NewsGridPage(3));
            ButtonLock();
        }
        async void History(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new NewsGridPage(2));
            ButtonLock();
        }

        async void StylePage(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ButtonLock();
            await button.RotateTo(-5, 80, Easing.BounceOut);
            await button.RotateTo(5, 120, Easing.BounceOut);
            await button.RotateTo(0, 80, Easing.BounceOut);
            await Navigation.PushAsync(new StylePage());
            ButtonLock();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            updateMissions();
            ChangeIntroStep(App.LoggedinUser.TutorialProgress);
            
        }
    }
}