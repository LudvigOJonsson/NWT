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
        public static List<Tile> SudokuBoard = new List<Tile>();

        public class Tile
        {
            public int x { get; set; }
            public int y { get; set; }
            public Entry entry { get; set; }
        }



        public SudokuPage ()
		{
			InitializeComponent ();
            MakeEntry();
            App.database.UpdateStats("GameStarted");
            LoadSudoku();
        }

        public void MakeEntry()
        {
            for (int x = 0; x <= 8; x++)
            {
                for (int y = 0; y <= 8; y++)
                {
                    var Number = new Entry();
                    Number.Text = "";
                    Number.FontSize = 10;
                    Number.TextColor = Color.Black;
                    Number.Keyboard = Keyboard.Numeric;
                    Number.MaxLength = 1;
                    Number.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    Number.VerticalOptions = LayoutOptions.Center;
                    SudokuGrid.Children.Add (Number, x, y);
                    var Tile = new Tile();
                    Tile.x = x;
                    Tile.y = y;
                    Tile.entry = Number; 
                    SudokuBoard.Add(Tile);
                }
            }

        }
        async public void SolveSudoku(object sender, EventArgs e)
        {
            if(CalculateSudoku())
            {
                await DisplayAlert("Task", "You Solved the Sudoku!", "OK");
                if (App.LoggedinUser != null)
                {
                    App.database.MissionUpdate(App.LoggedinUser, "SudokuSolved");
                    App.database.UpdateStats("GameFinished");
                }
            }
            else {
                await DisplayAlert("Task", "Incorrect Solution, please correct your mistakes.", "OK");
            }
        }
        public bool CalculateSudoku()
        {
            Boolean Solved = false;
            for(int x = 1; x <= 9; x++)
            {
                for (int y = 1; y <= 9; y++)
                {
                    
                    var Tile = App.database.GetTile(x,y).First().Value;
                    foreach (Tile T in SudokuBoard)
                    {
                        if (T.x == x - 1 && T.y == y - 1 && T.entry.Text == Tile.ToString())
                        {
                            Solved = true;
                            break;
                        }
                        else
                        {
                            Solved = false;
                        }
                    }
                    if(!Solved)
                        return false;
                }
            }
            return true;    
        }

        public void LoadSudoku()
        {
            for (int x = 1; x <= 9; x++)
            {
                for (int y = 1; y <= 9; y++)
                {
                    var Tile = App.database.GetTile(x, y).First();
                    if (Tile.Placed == 1)
                    {
                        foreach (Tile T in SudokuBoard)
                        {
                            if(T.x == x-1 && T.y == y-1)
                            {
                                T.entry.Text = Tile.Value.ToString();
                            }
                        }                           
                    }
                }
            }
        }
	}
}