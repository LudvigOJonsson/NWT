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
                var Check = App.database.VoteCheck(App.LoggedinUser.ID, Q);

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