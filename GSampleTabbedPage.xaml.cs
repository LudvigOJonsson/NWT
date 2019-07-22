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
    public partial class GSampleTabbedPage : TabbedPage
    {
        public GSampleTabbedPage()
        {
            InitializeComponent();

            
            Children[0] = new GSampleNewsGrid();
            Children[1] = new GSamplePage();            
            Children[2] = new GSamplePage2();
            Children[3] = new GSampleProfilePage();


        }
    }
}