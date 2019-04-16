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
	public partial class PicrossPage : ContentPage
	{
        public PicrossTable PCT;
        public Color Flag = Color.Green;
        public List<string> Left;
        public List<string> Top;
        public List<List<int>> Solution;
        public List<List<Button>> Gameboard = new List<List<Button>>();
        public bool Solved = false;
        public bool Fusk = false;


        public PicrossPage ()
		{
			InitializeComponent ();
            PCT = App.database.LoadPicross().First();
            Left = JsonConvert.DeserializeObject<List<string>>(PCT.Left);
            Top = JsonConvert.DeserializeObject<List<string>>(PCT.Top);
            Solution = JsonConvert.DeserializeObject<List<List<int>>>(PCT.Gameboard);
            MakeBoard();
		}






        public void MakeBoard()
        {
            for (int x = 0; x < 10; x++)
            {
                List<Button> Temp = new List<Button>();
                for (int y = 0; y < 10; y++)
                {
                    var Tile = new Button();
                    Tile.BackgroundColor = Color.White;
                    Tile.Clicked += Place;
                    PicrossGrid.Children.Add(Tile, x + 1, y + 1);
                    Temp.Insert(y,Tile);                 
                }
                Gameboard.Insert(x,Temp);
            }

            T1.Text = Top[0];
            T2.Text = Top[1];
            T3.Text = Top[2];
            T4.Text = Top[3];
            T5.Text = Top[4];
            T6.Text = Top[5];
            T7.Text = Top[6];
            T8.Text = Top[7];
            T9.Text = Top[8];
            T10.Text = Top[9];


            L1.Text = Left[0];
            L2.Text = Left[1];
            L3.Text = Left[2];
            L4.Text = Left[3];
            L5.Text = Left[4];
            L6.Text = Left[5];
            L7.Text = Left[6];
            L8.Text = Left[7];
            L9.Text = Left[8];
            L10.Text = Left[9];


        }

        public void Place(object sender, EventArgs e)
        {
            var Button = (Button)sender;
            if(Button.BackgroundColor == Color.White)
            {
                Button.BackgroundColor = Flag;
            }
            else if (Button.BackgroundColor == Color.Red && Flag == Color.Green)
            {
                Button.BackgroundColor = Flag;
            }
            else if (Button.BackgroundColor == Color.Green && Flag == Color.Red)
            {
                Button.BackgroundColor = Flag;
            }
            else
            {
                Button.BackgroundColor = Color.White;
            }
        }

        async public void SolvePicross(object sender, EventArgs e)
        {
            if ((CalculatePicross() || Fusk) && !Solved)
            {
                await DisplayAlert("Task", "You Solved the Picross!", "OK");
                App.database.StatUpdate("GameFinished");
                Solved = true;
            }
            else
            {
                await DisplayAlert("Task", "Incorrect Solution, please correct your mistakes. (Unless you want to Cheat, at which you can just click solve again..)", "OK");
                //Fusk = true;
            }
        }
        public bool CalculatePicross()
        {
            Boolean Solved = false;
            for (int x = 0; x < 10; x++)
            {
                
                var TempBoard = Gameboard[x];
                for (int y = 0; y < 10; y++)
                {
                    var TempSol = Solution[y];
                    var SolTile = TempSol[x];
                    var Tile = TempBoard[y];

                    if ((SolTile == 49 && Tile.BackgroundColor == Color.Green)|| (SolTile == 48 && Tile.BackgroundColor != Color.Green))
                    {
                        Console.WriteLine("Picross Debug, X: " + x + " Y: " + y + " SolTile: " + SolTile + " TileColor: " + Tile.BackgroundColor.ToString() + " True");
                        Solved = true;
                    }
                    else
                    {
                        Console.WriteLine("Picross Debug, X: " + x + " Y: " + y + " SolTile: " + SolTile + " TileColor: " + Tile.BackgroundColor.ToString() + " False");
                        Solved = false;
                    }
                    if (!Solved)
                        return false;
                }
            }
            return true;
        }


        public void SetGreen(object sender, EventArgs e)
        {
            Flag = Color.Green;
        }
        public void SetRed(object sender, EventArgs e)
        {
            Flag = Color.Red;
        }


    }
}