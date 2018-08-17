using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace NWT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsPage : ContentPage
	{
        public List<string> imageLinks = new List<string>();
        Random rnd = new Random();
        public int red = 255;
        public int green = 0;
        public int blue = 0;

        public static int ArticleNR;
		public NewsPage (RSSTable RSS)
		{
			InitializeComponent ();
            imageLinks.Add("http://media2.hitzfm.nu/2016/11/Nyheter_3472x1074.jpg");
            imageLinks.Add("https://pbs.twimg.com/media/CynmmdYWgAAjky1.jpg");
            imageLinks.Add("https://www.surfertoday.com/images/stories/clouds.jpg");
            imageLinks.Add("https://s-ec.bstatic.com/images/hotel/max1024x768/683/68345961.jpg");
            imageLinks.Add("https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Hertig_Johans_torg%2C_Sk%C3%B6vde%2C_2014_01.JPG/1200px-Hertig_Johans_torg%2C_Sk%C3%B6vde%2C_2014_01.JPG");
            imageLinks.Add("https://cdn2.acsi.eu/5/8/5/2/5852b667270eb.jpeg");
            imageLinks.Add("https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Runder_Berg.JPG/1200px-Runder_Berg.JPG");
            imageLinks.Add("https://thumbs.dreamstime.com/z/online-robber-17098197.jpg");

            if(App.LoggedinUser != null)
            {
                App.database.MissionUpdate(App.LoggedinUser, "ArticleRead");
                NewsPageView.BackgroundColor = Color.FromRgb(red, green, blue);
                CountDown();
            }
            else
            {
                NewsPageView.BackgroundColor = Color.FromRgb(150, 150, 150);
            }

            
            LoadNews(RSS);

        }
        private void CountDown()
        {

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 10000;
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;

        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (red > 0) { red = red - 255; };
            if (green < 255) { green = green + 255; };
            NewsPageView.BackgroundColor = Color.FromRgb(red, green, blue);
            CountDown();
        }

        void LoadNews(RSSTable RSS)
        {
            

            
            Header.Text = RSS.Title;
            Body.Text = RSS.Description;
            Body.Text = Body.Text + " Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            //Text.Text = ""; "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Link.Text = RSS.Link;
            ArticleNR = RSS.ID;
            Date.Text = "Publicerad: "+RSS.PubDate;
            ArticleImage.Source = imageLinks[rnd.Next(7)];
            LoadComments();
        }

        void SubmitCommentBullshit()
        {
            SubmitComment(-1);
        }

        void SubmitComment(int ReplyNR)
        {

            if(App.database.TokenCheck() && (Comment.Text != null || Comment.Text != ""))
            {
                var CNR = App.database.CommentCount(ArticleNR);
                var SC = new CommentTable();
                
                SC.Article = ArticleNR;
                SC.CommentNR = CNR;
                SC.User = App.LoggedinUser.ID;
                if (ReplyNR > -1)
                {
                    var Reply = App.database.GetComment(ReplyNR).First();
                    var User = App.database.GetUser(Reply.User).First();
                    SC.Comment = "@" + User.Name + Reply.CommentNR +", " + Comment.Text; //
                }
                else
                {
                    SC.Comment = Comment.Text;
                }
                  
                SC.Point = 0;
                Comment.Text = "";
                App.database.InsertComment(SC);
                LoadComments();
            }
            if (App.LoggedinUser != null)
            {
                App.database.MissionUpdate(App.LoggedinUser, "CommentPosted");
            }
        }

        void LoadComments() // Remove Previously Rendered Comments.
        {
            var Query = App.database.GetComments(ArticleNR);


            foreach (var s in Query)
            {
                var User = App.database.GetUser(s.User).First();
                ArticleGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                var CommentBox = new BoxView
                {
                    Color = Color.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
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
                var VoteArrowUp = new Button
                {
                    Image = "uparrow.png",
                    BackgroundColor = Color.Transparent,
                    WidthRequest = 20,
                    HeightRequest = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    Margin = 0,
                };
                var VoteArrowDown = new Button
                {
                    Image = "downarrow.png",
                    BackgroundColor = Color.Transparent,
                    WidthRequest = 20,
                    HeightRequest = 20,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.End,
                    Margin = 0,
                };
                /*var Userimage = new Image
                {
                    Source = "snail.png",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };*/
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

                Reply.Clicked += (o, e) => {
                    SubmitComment(s.ID);
                };

                ArticleGrid.Children.Add(CommentBox, 0, s.CommentNR + 8);
                ArticleGrid.Children.Add(Comment, 0, s.CommentNR + 8);
                ArticleGrid.Children.Add(Username, 0, s.CommentNR + 8);
                ArticleGrid.Children.Add(VoteArrowDown, 0, s.CommentNR + 8);
                ArticleGrid.Children.Add(VoteArrowUp, 0, s.CommentNR + 8);
                //ArticleGrid.Children.Add(Userimage, 0, s.CommentNR + 8);
                ArticleGrid.Children.Add(Reply, 0, s.CommentNR + 8);
            }
        }
    }
}
