﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NWT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizPage : ContentPage
    {
        public string state = "Menu";
        public int Score = 0;
        public string CorrectAnswer = "";
        public int CurrentQuestion;
        public List<BoxView> ScoreList = new List<BoxView>();
        public static Random rnd = new Random();
        public bool Confirmed = false;

        public class Question{
            public string QuestionText { get; set; }
            public string ChoiceA { get; set; }
            public string ChoiceB { get; set; }
            public string ChoiceC { get; set; }
            public string ChoiceD { get; set; }
            public string CorrectAnswer { get; set; }
            public List<string> AnswerList { get; set; }
            public Question()
            {
                QuestionText = "Den lever i vattnet Och kan simma, inte Flyger den heller...";
                ChoiceA = "Jaffakaj";
                ChoiceB = "Jaffakaj";
                ChoiceC = "Jaffakaj";
                ChoiceD = "Jaffakaj";

                var RNG = rnd.Next(3);

                switch (RNG)
                {
                    case 0:
                        CorrectAnswer = "A";
                        ChoiceA = "En elegant firre";
                        break;
                    case 1:
                        CorrectAnswer = "B";
                        ChoiceB = "En elegant firre";
                        break;
                    case 2:
                        CorrectAnswer = "C";
                        ChoiceC = "En elegant firre";
                        break;
                    case 3:
                        CorrectAnswer = "D";
                        ChoiceD = "En elegant firre";
                        break;
                }
                    

            }

        }


		public QuizPage ()
		{
			InitializeComponent ();
            ScoreList.Add(Q1);
            ScoreList.Add(Q2);
            ScoreList.Add(Q3);
            ScoreList.Add(Q4);
            ScoreList.Add(Q5);

        }

        public void Gametick(object sender, System.EventArgs e)
        {
            var Clicked = (Button)sender;
            switch (state)
            {
                case "Menu":
                    Start.IsVisible = false;
                    Submit.IsVisible = false;
                    Header.IsVisible = false;

                    QuestionText.IsVisible = true;
                    QuestionText.Text = "Välj en kategori";
                    Cat1.IsVisible = true;
                    Cat2.IsVisible = true;
                    Cat3.IsVisible = true;

                    CurrentQuestion = 0;

                    state = "Category";
                    break;
                case "Category":

                    Cat1.IsVisible = false;
                    Cat2.IsVisible = false;
                    Cat3.IsVisible = false;

                    
                    A.IsVisible = true;
                    B.IsVisible = true;
                    C.IsVisible = true;
                    D.IsVisible = true;
                    A.BackgroundColor = Color.White;
                    B.BackgroundColor = Color.White;
                    C.BackgroundColor = Color.White;
                    D.BackgroundColor = Color.White;
                    A.BorderColor = Color.FromHex("#50d2c2");
                    B.BorderColor = Color.FromHex("#50d2c2");
                    C.BorderColor = Color.FromHex("#50d2c2");
                    D.BorderColor = Color.FromHex("#50d2c2");

                    var Q = new Question();
                    QuestionText.Text = "Kategori; "+Clicked.Text+ ": " +Q.QuestionText; 
                    A.Text = Q.ChoiceA;
                    B.Text = Q.ChoiceB;
                    C.Text = Q.ChoiceC;
                    D.Text = Q.ChoiceD;
                    CorrectAnswer = Q.CorrectAnswer;

                    state = "Question";
                    break;
                case "Question":

                    


                    if (Confirmed)
                    {
                        
                        A.IsVisible = false;
                        B.IsVisible = false;
                        C.IsVisible = false;
                        D.IsVisible = false;
                       

                        if (CurrentQuestion == 4)
                        {

                            foreach (var Box in ScoreList)
                            {
                                Box.Color = Color.White;
                            }
                            Start.IsVisible = true;
                            Submit.IsVisible = true;
                            Header.IsVisible = true;
                            QuestionText.IsVisible = false;
                            state = "Menu";
                        }
                        else
                        {
                            CurrentQuestion++;
                            Cat1.IsVisible = true;
                            Cat2.IsVisible = true;
                            Cat3.IsVisible = true;
                            
                            QuestionText.Text = "Välj en kategori";
                            state = "Category";
                        }
                        Confirmed = false;
                    }
                    else
                    {
                        A.BackgroundColor = Color.Red;
                        B.BackgroundColor = Color.Red;
                        C.BackgroundColor = Color.Red;
                        D.BackgroundColor = Color.Red;
                        A.BorderColor = Color.FromHex("#50d2c2");
                        B.BorderColor = Color.FromHex("#50d2c2");
                        C.BorderColor = Color.FromHex("#50d2c2");
                        D.BorderColor = Color.FromHex("#50d2c2");

                        if (Clicked.ClassId == CorrectAnswer)
                        {
                            ScoreList.ElementAt(CurrentQuestion).Color = Color.Green;
                            Clicked.BackgroundColor = Color.Green;
                        }
                        else
                        {
                            ScoreList.ElementAt(CurrentQuestion).Color = Color.Red;

                            switch (CorrectAnswer)
                            {
                                case "A":
                                    A.BackgroundColor = Color.Green;
                                    break;
                                case "B":
                                    B.BackgroundColor = Color.Green;
                                    break;
                                case "C":
                                    C.BackgroundColor = Color.Green;
                                    break;
                                case "D":
                                    D.BackgroundColor = Color.Green;
                                    break;
                            }
                        }
                        Confirmed = true;
                    }
                    

                    break;
            }


        }
	}
}