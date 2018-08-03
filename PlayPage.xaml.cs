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
	public partial class PlayPage : ContentPage
	{
		public PlayPage ()
		{
			InitializeComponent ();
		}

        async void PlaySudoku(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SudokuPage());
        }

    }
}