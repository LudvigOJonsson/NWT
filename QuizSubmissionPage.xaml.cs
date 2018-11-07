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
	public partial class QuizSubmissionPage : ContentPage
	{
        public string CorAnsw = "";
        public string Categ = "";
        public QuizSubmissionPage()
		{
			InitializeComponent();
		}

        public void CatPop(object sender, EventArgs e)
        {

        }

        public void AnswerPop(object sender, EventArgs e)
        {
            
        }

        public void SubmitQ(object sender, EventArgs e)
        {
            if (CorAnsw != "" && Categ != "")
            {
                var Q = new QuizTable();
                Q.QuestionText = QuestionText.Text;
                Q.ChoiceA = A.Text;
                Q.ChoiceB = B.Text;
                Q.ChoiceC = C.Text;
                Q.ChoiceD = D.Text;
                Q.Category = Categ;
                Q.CorrectAnswer = CorAnsw;
            }
            
        }

        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }
    }
}