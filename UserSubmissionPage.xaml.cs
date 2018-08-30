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
	public partial class UserSubmissionPage : ContentPage
	{
		public UserSubmissionPage ()
		{
			InitializeComponent ();
		}

        public void Submit()
        {
            var RSS = new UserRSSTable();
            RSS.Rubrik = Rubrik.Text;
            RSS.Ingress = Ingress.Text;
            RSS.Brodtext = Brodtext.Text;
            RSS.PubDate = DateTime.Now;

            App.database.InsertInsandare(RSS);
        }

	}
}