using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace NWT
{
	public partial class App : Application
	{

        public static DBHelper database;
        public static MasterDetailPage Startpage;
        public static Sidemenu SideMenu;
        public static MainPage Mainpage;
        public static bool Instanciated = false;
        public static UserTable LoggedinUser = null;
        public static TokenTable Token = null;
        public App()
        {
            InitializeComponent();

            if (database == null)
            {
                database = new DBHelper(DependencyService.Get<IFileHelper>().GetLocalFilePath("UserDB.db3"));
            }
            Mainpage = new MainPage();
            SideMenu = new Sidemenu();
        
            
            Startpage = new MasterDetailPage()
            {
                Master = new NavigationPage(SideMenu) { Title = "Side Menu" },
                Detail = new NavigationPage(Mainpage) { BarBackgroundColor = Color.FromHex("#FFFFFF") }

            };
            
            MainPage = Startpage;
            Mainpage.Children[0] = new CommunityPage();
            Mainpage.Children[2] = new LoginPage();

            Instanciated = true;
        }
        
        protected override void OnStart ()
		{
            
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
