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
    public partial class IntroWalkthrough : CarouselPage
    {
        public IntroWalkthrough()
        {
            InitializeComponent();
        }

        public void PageRight(object sender, EventArgs e)
        {
            var pageCount = Children.Count;

            var index = Children.IndexOf(CurrentPage);
            index++;
            if (index >= pageCount)
            {
                Customization();
                return;
            }

            CurrentPage = Children[index];
        }

        public void Customization()
        {
            App.Startpage.Detail = new CustomizationPage() { };
        }
    }
}