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
	public partial class VoteSubmissionPage : ContentPage
	{
		public VoteSubmissionPage ()
		{
			InitializeComponent ();
		}
        public async void SubmitQ(object sender, EventArgs e)
        {
            if (Question.Text != "" && Op1.Text != "" && Op2.Text != "")
            {
                var VQ = new VoteQuestionTable();
                
                VQ.Question = Question.Text;
                VQ.Option1 = Op1.Text;
                VQ.Option2 = Op2.Text;
                VQ.Option3 = Op3.Text;
                VQ.Option4 = Op4.Text;
                VQ.Posted = DateTime.Now;
                VQ.Stage = 1;

                App.database.InsertVoteQuestion(VQ);
            }

            await DisplayAlert("Submission Successful", "Question Submitted", "OK");
        }
    }
}