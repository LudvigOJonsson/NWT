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
	public partial class AvatarPage : ContentPage
	{
        public Label Label = new Label { };
        public Label Label2 = new Label { };
        public Image Image = new Image { };
        public Image Image2 = new Image { };
        public Button Button = new Button { };

        public AvatarPage()
        {
            InitializeComponent();
        }

        public void ChangeClothes(object sender, EventArgs e)
        {
            ProfilePictureClothes.Source = "snailClothes";
        }
        public void ChangeClothes2(object sender, EventArgs e)
        {
            ProfilePictureClothes.Source = "";
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        protected override void OnDisappearing()
        {
            Console.WriteLine("Memory Cleanup");
            GC.Collect();
        }
    }
}