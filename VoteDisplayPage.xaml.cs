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
	public partial class VoteDisplayPage : ContentPage
	{
		public VoteDisplayPage (VoteQuestionTable VQ ,int argc)
		{
			InitializeComponent ();
            Question.ClassId = VQ.ID.ToString();
            Question.Text = VQ.Question;
            Op1.Text = VQ.Option1;
            Op2.Text = VQ.Option2;
            Op3.Text = VQ.Option3;
            Op4.Text = VQ.Option4;

            Op1.BackgroundColor = App.MC;
            Op2.BackgroundColor = App.MC;
            Op3.BackgroundColor = App.MC;
            Op4.BackgroundColor = App.MC;
            Op1.TextColor = Color.White;
            Op2.TextColor = Color.White;
            Op3.TextColor = Color.White;
            Op4.TextColor = Color.White;

            Question.TextColor = App.MC;

            int TV1 = 0;
            int TV2 = 0;
            int TV3 = 0;
            int TV4 = 0;

            if (argc == 1)
            {
                VoteCheck(VQ.ID);
            }
            else if(argc == 2)
            {
                VoteCheck(VQ.ID);
                Op1.IsEnabled = false;
                Op2.IsEnabled = false;
                Op3.IsEnabled = false;
                Op4.IsEnabled = false;


                var Votelist = App.database.GetVotes(VQ.ID);

                foreach (VoteTable Vote in Votelist)
                {
                    switch (Vote.ChoosenOption)
                    {
                        case 1:
                            TV1++;
                            break;
                        case 2:
                            TV2++;
                            break;
                        case 3:
                            TV3++;
                            break;
                        case 4:
                            TV4++;
                            break;
                    }
                }
                var TVMax = Math.Max(TV1, Math.Max(TV2, Math.Max(TV3, TV4)));
                if(TVMax == TV1)
                {
                    Question.Text = "Val '" + VQ.Option1 + "' leder röstningen";
                }
                else if (TVMax == TV2)
                {
                    Question.Text = "Val '" + VQ.Option2 + "' leder röstningen";
                }
                else if (TVMax == TV3)
                {
                    Question.Text = "Val '" + VQ.Option3 + "' leder röstningen";
                }
                else if (TVMax == TV4)
                {
                    Question.Text = "Val '" + VQ.Option4 + "' leder röstningen";
                }

                

                Op1.Text = TV1.ToString();
                Op2.Text = TV2.ToString();
                Op3.Text = TV3.ToString();
                Op4.Text = TV4.ToString();


            }
            else if (argc == 3)
            {
                Op1.IsEnabled = false;
                Op2.IsEnabled = false;
                Op3.IsEnabled = false;
                Op4.IsEnabled = false;
            }

        }

        public void VoteCheck(int Q)
        {
            if (App.LoggedinUser != null)
            {
                var Check =  App.database.VoteCheck(App.LoggedinUser.ID, Q);

                if (Check.Count() > 0)
                {
                    Op1.IsEnabled = false;
                    Op2.IsEnabled = false;
                    Op3.IsEnabled = false;
                    Op4.IsEnabled = false;
                    var Answ = Check.First().ChoosenOption;

                    switch (Answ)
                    {
                        case 1:
                            Op1.BackgroundColor = Color.ForestGreen;
                            break;
                        case 2:
                            Op2.BackgroundColor = Color.ForestGreen;
                            break;
                        case 3:
                            Op3.BackgroundColor = Color.ForestGreen;
                            break;
                        case 4:
                            Op4.BackgroundColor = Color.ForestGreen;
                            break;
                    }

                }
            }
        }



        public async void SubmitV(object sender, EventArgs e)
        {
            var Clicked = (Button)sender;
            if (App.LoggedinUser != null)
            {
                var V = new VoteTable();
                V.Question = Convert.ToInt32(Question.ClassId);
                V.User = App.LoggedinUser.ID;
                V.ChoosenOption = Convert.ToInt32(Clicked.ClassId);             
                App.database.InsertVote(V);
                await DisplayAlert("Submission Successful", "Vote Submitted", "OK");
            }
            else
            {
                await DisplayAlert("Submission Unsuccessful", "Please Login to use the Vote Function", "OK");
            }
            Op1.IsEnabled = false;
            Op2.IsEnabled = false;
            Op3.IsEnabled = false;
            Op4.IsEnabled = false;

        }
    }
}