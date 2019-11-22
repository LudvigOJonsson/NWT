using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
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
	public partial class SudokuPage : ContentPage
	{
        public SudokuTable SudoT;
        public List<List<int>> Solution;
        public List<List<int>> Placement;
        public bool Fusk = false;
        public bool Solved = false;
        public int ClickedNR = 1;
        public bool Change = false;
        public List<List<Button>> Gameboard = new List<List<Button>>();


        public SudokuPage ()
		{
            SudoT = App.database.LoadSudoku().First();
            Solution = JsonConvert.DeserializeObject<List<List<int>>>(SudoT.ValueList);
            Placement = JsonConvert.DeserializeObject<List<List<int>>>(SudoT.PlacedList);
            InitializeComponent ();
            MakeBoard();
        }

        public void MakeBoard()
        {
            for (int x = 0; x < 9; x++)
            {
                var TempSol = Solution[x];
                var TempPlace = Placement[x];
                List<Button> Temp = new List<Button>();
                for (int y = 0; y < 9; y++)
                {
                    var Tile = new Button();
                    Tile.Margin = 1;
                    Tile.BackgroundColor = Color.White;
                    char temp = (char)TempSol[y];
                    
                    if(TempPlace[y] == 49)
                    {
                        Tile.Text = (temp - 48).ToString();
                    }
                    else
                    {
                        Tile.Text = " ";
                    }
                    Tile.Clicked += Place;
                    SudokuGrid.Children.Add(Tile, y, x);
                    Temp.Insert(y, Tile);
                }
                Gameboard.Insert(x, Temp);
            }
        }

        public async void Place(object sender, EventArgs e)
        {
            var Button = (Button)sender;
            await PopupNavigation.Instance.PushAsync(new NumpadPopup(Button));
        }


        async public void SolveSudoku(object sender, EventArgs e)
        {
            if((CalculateSudoku() || Fusk) && !Solved)
            {
                await DisplayAlert("Task", "Du löste Picrosset! Bra jobbat! Här får du 20 mynt! Kom tillbaka imorgon för mer!", "OK");
                App.database.StatUpdate("GameFinished");
                App.database.Plustoken(App.LoggedinUser, 20);
                Solved = true;
            }
            else if (Solved)
            {
                await DisplayAlert("WIP", "Du har redan löst denna Sudoku, återkom vid ett senare tillfälle.", "OK");
            }
            else {
                await DisplayAlert("Inkorrekt", "Felaktig lösning, försök hitta var du gjort ett misstag", "OK");
                Fusk = true;
            }
        }
        public bool CalculateSudoku()
        {
            Boolean Solved = false;
            for(int x = 0; x <= 8; x++)
            {
                var TempBoard = Gameboard[x];
                for (int y = 0; y <= 8; y++)
                {
                    var TempSol = Solution[x];
                    var SolTile = TempSol[y];
                    var Tile = TempBoard[y];

                    if (((SolTile-48).ToString() == Tile.Text))
                    {
                        Console.WriteLine("Sudoku Debug, X: " + x + " Y: " + y + " SolTile: " + (SolTile - 48) + " TileText: " + Tile.Text + " True");
                        Solved = true;
                    }
                    else
                    {
                        Console.WriteLine("Sudoku Debug, X: " + x + " Y: " + y + " SolTile: " + (SolTile - 48) + " TileText: " + Tile.Text + " False");
                        Solved = false;
                    }

                    if (!Solved)
                        return false;
                }
            }
            return true;    
        }


        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }
    }
}