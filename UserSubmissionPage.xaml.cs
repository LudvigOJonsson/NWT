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
            ClassId = "-1";
		}

        public void Submit()
        {
            if(App.LoggedinUser != null)
            {
                var RSS = new UserRSSTable();
                RSS.Rubrik = Rubrik.Text;
                RSS.Ingress = Ingress.Text;
                RSS.Brodtext = Brodtext.Text;
                RSS.PubDate = DateTime.Now;
                RSS.Author = App.LoggedinUser.ID;
                RSS.Referat = Int32.Parse(ClassId);
                App.database.InsertInsandare(RSS);
                App.database.UpdateStats("InsandareSubmitted");
            }
            
        }
        async public void GetReference()
        {
            
            await Navigation.PushAsync(new NewsGridPage(1));
            
        }        
	}
}