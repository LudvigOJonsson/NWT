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
	public partial class CommentPage : ContentPage
	{
        public int ArticleNR;
        public int CLVL = 1;
        public CommentTable root;
        public CommentPage (int argc,CommentTable s)
		{
            root = s;
            ArticleNR = argc;
			InitializeComponent ();
            

            LoadComments(CLVL);
		}

        void AddComment(CommentTable s)
        {
            bool UUV = false;
            var User = App.database.GetUser(s.User).First();
            UpvoteTable Upvote = null;
            var UpvoteList = App.database.GetUpvote(s.CommentNR, ArticleNR, s.UserSubmitted);
            Console.WriteLine("The current Upvotelist: " + UpvoteList);



            if (UpvoteList.Any() && App.LoggedinUser != null)
            {
                Console.WriteLine("The current Upvotelist First: " + UpvoteList.First());

                foreach (UpvoteTable UV in UpvoteList)
                {
                    if (UV.User == App.LoggedinUser.ID)
                    {
                        UUV = true;
                        Upvote = UV;
                        break;
                    }
                }
            }

            Console.WriteLine("TEST");
            /*var CommentBox = new BoxView
            {
                Color = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = 3,
            };*/

            var Box = new Button
            {
                CornerRadius = 10,
                Margin = 5,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            var Comment = new Label
            {
                Text = s.Comment,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.Black,
                FontSize = 16,
                WidthRequest = 290,
                Margin = 20,
            };
            var Username = new Label
            {
                Text = "  " + User.Name,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                TextColor = Color.Black,
                FontSize = 16,

            };
            var VoteArrowUp = new Button()
            {
                Image = "uparrow.png",
                BackgroundColor = Color.Transparent,
                WidthRequest = 20,
                HeightRequest = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Margin = 0,
                ClassId = "false"
            };
            var VoteNumber = new Label
            {
                Text = s.Point.ToString(),
                TextColor = Color.Black,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin = 0,
                FontSize = 16,

            };
            var VoteArrowDown = new Button()
            {
                Image = "downarrow.png",
                BackgroundColor = Color.Transparent,
                WidthRequest = 20,
                HeightRequest = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Margin = 0,
                ClassId = "false"
            };
            /*var Userimage = new Image
            {
                Source = "snail.png",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };*/



            if (App.LoggedinUser != null)
            {
                App.database.MissionUpdate(App.LoggedinUser, "CommentPosted");

                if (UUV)
                {
                    if (Upvote.Point == 1)
                    {
                        VoteArrowUp.ClassId = "true";
                        VoteArrowUp.Image = "uparrowBlue.png";
                    }
                    else if (Upvote.Point == -1)
                    {
                        VoteArrowDown.ClassId = "true";
                        VoteArrowDown.Image = "downarrowRed.png";
                    }
                }


            }

            VoteArrowUp.Clicked += (o, e) =>
            {
                if (App.LoggedinUser != null)
                {
                    var UV = new UpvoteTable();
                    UV.User = App.LoggedinUser.ID;
                    UV.UserSubmitted = s.UserSubmitted;
                    UV.Article = ArticleNR;
                    UV.CommentNR = s.CommentNR;
                    UV.Point = 0;

                    if (VoteArrowUp.ClassId == "false")
                    {
                        VoteArrowUp.ClassId = "true";
                        VoteArrowUp.Image = "uparrowBlue.png";
                        UpClicked(o, e);
                        UV.Point = 1;
                        App.database.InsertUpvote(UV);
                        if (VoteArrowDown.ClassId == "true")
                        {
                            VoteArrowDown.ClassId = "false";
                            VoteArrowDown.Image = "downarrow.png";
                            App.database.DeleteUpvote(Upvote);
                            UV.Point = 2;
                        }


                    }
                    else
                    {
                        VoteArrowUp.ClassId = "false";
                        VoteArrowUp.Image = "uparrow.png";
                        App.database.DeleteUpvote(Upvote);
                        UV.Point = -1;

                    }

                    App.database.PointUpdate(s, UV.Point);
                    LoadComments(CLVL);

                }


            };
            VoteArrowDown.Clicked += (o, e) =>
            {
                if (App.LoggedinUser != null)
                {
                    var UV = new UpvoteTable();
                    UV.User = App.LoggedinUser.ID;
                    UV.UserSubmitted = s.UserSubmitted;
                    UV.Article = ArticleNR;
                    UV.CommentNR = s.CommentNR;
                    UV.Point = 0;

                    if (VoteArrowDown.ClassId == "false")
                    {
                        VoteArrowDown.ClassId = "true";
                        VoteArrowDown.Image = "downarrowRed.png";
                        DownClicked(o, e);
                        UV.Point = -1;
                        App.database.InsertUpvote(UV);
                        if (VoteArrowUp.ClassId == "true")
                        {
                            VoteArrowUp.ClassId = "false";
                            VoteArrowUp.Image = "uparrow.png";
                            App.database.DeleteUpvote(Upvote);
                            UV.Point = -2;
                        }


                    }
                    else
                    {
                        VoteArrowDown.ClassId = "false";
                        VoteArrowDown.Image = "downarrow.png";
                        App.database.DeleteUpvote(Upvote);
                        UV.Point = 1;

                    }

                    App.database.PointUpdate(s, UV.Point);
                    LoadComments(CLVL);

                }
            };
            async void UpClicked(object sender, System.EventArgs e)
            {
                await VoteArrowUp.RotateTo(-15, 80, Easing.BounceOut);
                await VoteArrowUp.RotateTo(15, 120, Easing.BounceOut);
                await VoteArrowUp.RotateTo(0, 80, Easing.BounceOut);
            }
            async void DownClicked(object sender, System.EventArgs e)
            {
                await VoteArrowDown.RotateTo(-15, 80, Easing.BounceOut);
                await VoteArrowDown.RotateTo(15, 120, Easing.BounceOut);
                await VoteArrowDown.RotateTo(0, 80, Easing.BounceOut);
            }
            
            //0, 1, Rownr, Rownr + 3
            CommentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            CommentGrid.Children.Add(Box, 0, 6, s.CommentNR, s.CommentNR + 1);
            CommentGrid.Children.Add(Comment, 1, 5, s.CommentNR, s.CommentNR + 1);
            CommentGrid.Children.Add(Username, 1, 5, s.CommentNR, s.CommentNR + 1);
            CommentGrid.Children.Add(VoteArrowDown, 0, 1, s.CommentNR, s.CommentNR + 1);
            CommentGrid.Children.Add(VoteArrowUp, 0, 1, s.CommentNR, s.CommentNR + 1);
            CommentGrid.Children.Add(VoteNumber, 0, 1, s.CommentNR, s.CommentNR + 1);
            //CommentGrid.Children.Add(Userimage, 0, s.CommentNR + 8);        
        }


        void LoadComments(int LVL)
        {
            var Query = App.database.GetComments(ArticleNR,LVL,root.ID);
            
            CommentGrid.Children.Clear();
            AddComment(root);
            foreach (var s in Query)
            {
                AddComment(s);   
            }
        }
    }
}