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
    public partial class GSamplePage : ContentPage
    {
        public GSamplePage()
        {
            InitializeComponent();


            Button.CornerRadius = (int)((Button.WidthRequest + Button.HeightRequest) / 4);

        }

       
         
    }
}