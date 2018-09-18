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
        public List<CommentTable> Chain = new List<CommentTable>();
        public Button BackButton = new Button() { Image = "uparrow.png" };
        public CommentPage (int argc,CommentTable s)
		{
            root = s;
            ArticleNR = argc;
			InitializeComponent ();

            BackButton.Clicked += (o, e) => {
                Chain.Remove(Chain.Last());
                CLVL--;
                LoadComments(CLVL, Chain.Last().ID);
            };

            


            Chain.Add(root);
            LoadComments(CLVL,Chain.Last().ID);
            
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
            var CurLVL = new Label
            {
                Text = s.Replylvl.ToString(),
                TextColor = Color.Black,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.End,
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
            
            var Reply = new Button()
            {
                BackgroundColor = Color.FromHex("#50d2c2"),
                TextColor = Color.FromHex("#FFFFFF"),
                WidthRequest = 60,
                HeightRequest = 30,
                Text = "Reply",
                FontSize = 10,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                Margin = 20,
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
                    LoadComments(CLVL, Chain.Last().ID);

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
                    LoadComments(CLVL, Chain.Last().ID);

                }
            };
            Reply.Clicked +=  (o, e) => {
                SubmitComment(s.ID);
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
            CommentGrid.Children.Add(Reply, 5, 6, s.CommentNR, s.CommentNR + 1);
            CommentGrid.Children.Add(CurLVL, 4, 5, s.CommentNR, s.CommentNR + 1);
            //CommentGrid.Children.Add(Userimage, 0, s.CommentNR + 8);      

            if (s.Replylvl == CLVL)
            {
                var Replies = new Button()
                {
                    Text = "X Replies",
                    BackgroundColor = Color.FromRgb(80, 210, 194),
                    TextColor = Color.Black,
                    WidthRequest = 60,
                    HeightRequest = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.End,
                    Margin = 0,

                };

                Replies.Clicked += (o, e) => {
                    Chain.Add(s);
                    CLVL++;
                    LoadComments(CLVL, Chain.Last().ID);
                };

                CommentGrid.Children.Add(Replies, 3, 4, s.CommentNR, s.CommentNR + 1);
            }

            if (CLVL > 1)
            {
                CommentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                CommentGrid.Children.Add(BackButton, 0, 6, s.CommentNR + 1, s.CommentNR + 2);
            }


        }

        void SubmitComment(int ReplyNR)
        {

            if (App.database.TokenCheck() && (Comment.Text != null || Comment.Text != ""))
            {
                var CNR = App.database.CommentCount(ArticleNR);
                var SC = new CommentTable();

                SC.Article = ArticleNR;
                SC.CommentNR = CNR;
                SC.UserSubmitted = root.UserSubmitted;
                SC.User = App.LoggedinUser.ID;
                SC.Replynr = ReplyNR;
                if (ReplyNR > -1)
                {
                    var Reply = App.database.GetComment(ReplyNR).First();
                    var User = App.database.GetUser(Reply.User).First();
                    SC.Replylvl = Reply.Replylvl + 1;
                    SC.Comment = "@" + User.Name + Reply.CommentNR + ", " + Comment.Text; //
                }
                else
                {
                    SC.Replylvl = 0;
                    SC.Comment = Comment.Text;
                }

                SC.Point = 0;
                Comment.Text = "";
                App.database.InsertComment(SC);
                LoadComments(CLVL, Chain.Last().ID);
            }
            if (App.LoggedinUser != null)
            {
                App.database.MissionUpdate(App.LoggedinUser, "CommentPosted");
            }
        }

        void LoadComments(int LVL, int ReplyID)
        {
            var Query = App.database.GetComments(ArticleNR,LVL,ReplyID);
            CommentGrid.Children.Clear();
            foreach (var a in Chain)
            {
                AddComment(a);
                
            }           
            foreach (var s in Query)
            {
                AddComment(s);   
            }

            
            
        }
    }
}