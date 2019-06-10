using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NWT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomizationPage : ContentPage
	{
		public CustomizationPage()
		{
            InitializeComponent ();

            foreach (var Box in CustomizationGrid.Children)
            {
                var Category = (Button)Box;


                if (App.SideMenu.Categories.Contains(Category.ClassId) || App.SideMenu.Tags.Contains(Category.ClassId))
                {
                    Box.IsEnabled = false;
                }


            }



        }

        void CategoryButtonClicked(object sender, System.EventArgs e)
        {

            var Button = (Button)sender;
            App.SideMenu.Categories.Add(Button.ClassId);
            App.SideMenu.UpdateTags();
            Button.IsEnabled = false;
        }
        void TagButtonClicked(object sender, System.EventArgs e)
        {

            var Button = (Button)sender;
            App.SideMenu.Tags.Add(Button.ClassId);
            App.SideMenu.UpdateTags();
            Button.IsEnabled = false;
        }



        public void FollowButton(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "Follow")
            {
                button.Text = "Unfollow";
            } else if (button.Text == "Unfollow")
            {
                button.Text = "Unfollow";
            }
        }
    }
}