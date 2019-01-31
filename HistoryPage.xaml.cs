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
	public partial class HistoryPage : ContentPage
	{

        public Button Box = new Button { };
        public Label Label = new Label { };

        public bool isArticle;
        public bool isComment;
        public bool isGame;
        public bool isInsend; //What the fuck should this actually be called? O_o

        public HistoryPage()
		{
			InitializeComponent ();
        }

        public void TempCreateHistoryBox()
        {
            //If the box is an article, then a label
            if (isArticle)
            {
                Label = new Label
                {
                    Text = "19-01-01" + " Article Name" + " Read.",
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    FontAttributes = FontAttributes.Bold,
                    ///WidthRequest = IMGXC,
                    //HeightRequest = ((RSS.Title.Length / 30)) * 50,
                    TextColor = Color.Black,
                    //ClassId = RSS.ID.ToString(),
                    Margin = 10,
                };

                //Label.GestureRecognizers.Add();

                Box = new Button
                {
                    BackgroundColor = Color.White,
                    //WidthRequest = IMGXC,
                    //HeightRequest = Label.HeightRequest,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.End,
                    //ClassId = RSS.ID.ToString(),
                    Margin = 0,
                    BorderWidth = 2,
                    BorderColor = Color.Black,
                };

                //Box.GestureRecognizers.Add();
            }
            if (isComment)
            {
                Label = new Label
                {
                    Text = "19-01-02" + " The first few letters of the comm...",
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    FontAttributes = FontAttributes.Bold,
                    ///WidthRequest = IMGXC,
                    //HeightRequest = ((RSS.Title.Length / 30)) * 50,
                    TextColor = Color.Black,
                    //ClassId = RSS.ID.ToString(),
                    Margin = 10,
                };

                //Label.GestureRecognizers.Add();

                Box = new Button
                {
                    BackgroundColor = Color.White,
                    //WidthRequest = IMGXC,
                    //HeightRequest = Label.HeightRequest,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.End,
                    //ClassId = RSS.ID.ToString(),
                    Margin = 0,
                    BorderWidth = 2,
                    BorderColor = Color.Black,
                };

                //Box.GestureRecognizers.Add();
            }
            if (isGame)
            {
                Label = new Label
                {
                    Text = "19-01-03" + " Sudoku game completed!",
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    FontAttributes = FontAttributes.Bold,
                    ///WidthRequest = IMGXC,
                    //HeightRequest = ((RSS.Title.Length / 30)) * 50,
                    TextColor = Color.Black,
                    //ClassId = RSS.ID.ToString(),
                    Margin = 10,
                };

                //Label.GestureRecognizers.Add();

                Box = new Button
                {
                    BackgroundColor = Color.White,
                    //WidthRequest = IMGXC,
                    //HeightRequest = Label.HeightRequest,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.End,
                    //ClassId = RSS.ID.ToString(),
                    Margin = 0,
                    BorderWidth = 2,
                    BorderColor = Color.Black,
                };

                //Box.GestureRecognizers.Add();
            }
            if (isInsend)
            {
                Label = new Label
                {
                    Text = "19-01-03" + " Name of insändare" + "read",
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    FontAttributes = FontAttributes.Bold,
                    ///WidthRequest = IMGXC,
                    //HeightRequest = ((RSS.Title.Length / 30)) * 50,
                    TextColor = Color.Black,
                    //ClassId = RSS.ID.ToString(),
                    Margin = 10,
                };

                //Label.GestureRecognizers.Add();

                Box = new Button
                {
                    BackgroundColor = Color.White,
                    //WidthRequest = IMGXC,
                    //HeightRequest = Label.HeightRequest,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.End,
                    //ClassId = RSS.ID.ToString(),
                    Margin = 0,
                    BorderWidth = 2,
                    BorderColor = Color.Black,
                };

                //Box.GestureRecognizers.Add();
            }
        }
        public void AddBox()
        {
            //Basic code for adding the boxes as children to the newsgrid.

            /*NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
            NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 120 });
            NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
            NewsGrid.RowDefinitions.Add(new RowDefinition { Height = 1 });
            NewsGrid.RowSpacing = 0;
            
            NewsGrid.Children.Add(Box.Box, 0, 3, Rownr, Rownr + 3); //Boxview
            NewsGrid.Children.Add(Box.Label, 0, 2, Rownr, Rownr + 3); //Label

            Rownr++;
            Rownr++;
            Rownr++;
            Rownr++;*/
        }
    }
}