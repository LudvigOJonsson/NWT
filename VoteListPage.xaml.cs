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
	public partial class VoteListPage : ContentPage
	{
        public static TapGestureRecognizer TGR;
        public static int ARGC = 0;

        public int Rownr = 0;


        public class VoteQuestion
        {
            
            public VoteQuestionTable VQ;
            public string Question = "";

            public BoxView Box = new BoxView { };
            public Label Label = new Label { };




            public VoteQuestion(VoteQuestionTable VQ_)
            {
                VQ = VQ_;
                Question = VQ.Question;

                


                Box = new BoxView
                {
                    BackgroundColor = Color.White,
                    WidthRequest = 200,
                    HeightRequest = 30 + Label.HeightRequest,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    ClassId = VQ.ID.ToString(),
                    Margin = 0,
                };
                Label = new Label
                {
                    Text = Question,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Start,
                    FontSize = 25,
                    FontAttributes = FontAttributes.Bold,
                    WidthRequest = 200,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HeightRequest = ((Question.Length / 30)) * 50 + 70,
                    TextColor = Color.Black,
                    ClassId = JsonConvert.SerializeObject(VQ),
                    Margin = 6,
                };
                Label.GestureRecognizers.Add(TGR);
            }



        }


        async void LoadVoteQuestion(object sender, EventArgs e)
        {

            var Header = (Label)sender;

            var VQ = JsonConvert.DeserializeObject<VoteQuestionTable>(Header.ClassId);

            await Navigation.PushAsync(new VoteDisplayPage(VQ, ARGC));

        }

        public VoteListPage (int argc)
		{
            
			InitializeComponent ();

            ARGC = argc;
            Console.WriteLine(ARGC);
            var VoteQuestionQuery = App.database.GetVoteQuestions(ARGC);
            Console.WriteLine("Questions Read, Nr: "+ VoteQuestionQuery.Count());
            var VoteQuestionList = new List<VoteQuestion>();
            
            TGR = new TapGestureRecognizer();
            TGR.Tapped += (s, e) => {
                IsEnabled = false;
                LoadVoteQuestion(s, e);
                IsEnabled = true;
            };



            foreach (VoteQuestionTable VQ in VoteQuestionQuery)
            {
                
                var VoteQ = new VoteQuestion(VQ);
                VoteQuestionList.Add(VoteQ);
                Console.WriteLine("Question Nr: " + VoteQ.VQ.Question);
                VotesGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                VotesGrid.RowSpacing = 5;
                VotesGrid.Children.Add(VoteQ.Box, 0, 3, Rownr, Rownr + 1);
                VotesGrid.Children.Add(VoteQ.Label, 0, 3, Rownr, Rownr + 1);
                Rownr++;

            }


		}
	}
}