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
	public partial class MissionsPage : ContentPage
	{
        public MissionsPage()
		{
			InitializeComponent ();
            var Tasklist = App.database.MissionUpdate(App.LoggedinUser, "Evaluate");
            m1t.Text = "Read "+Tasklist[0].Progress + "/" + Tasklist[0].Goal + "Articles";
            m2t.Text = "Post "+Tasklist[1].Progress + "/" + Tasklist[1].Goal + " Comments";
            m3t.Text = "Solve "+Tasklist[2].Progress + "/" + Tasklist[2].Goal + " Sudokus";



        }
    }
}