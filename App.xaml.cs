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
        public static ChoicesPage SideMenu;
        public static LoginPage Loginpage;
        public static MainPage Mainpage;
        public static bool Instanciated = false;
        public static UserTable LoggedinUser = null;
        public static TokenTable Token = null;
        public static bool Online = false;
        public static System.Timers.Timer Timer;
        public static bool Login = false;
        public static LoadingPopUp LoadingScreen;
        public static LoadingPopUp LS = new LoadingPopUp();
        public static Color MC = Color.FromHex("#649FD4");
        public static bool TutorialSafety = true;
        public static double Version = 1.1;
        public App()
        {
            InitializeComponent();

            
            


            if (database == null)
            {
                database = new DBHelper(DependencyService.Get<IFileHelper>().GetLocalFilePath("UserDB.db3"));
            }
            Loginpage = new LoginPage();
            SideMenu = new ChoicesPage();
            Mainpage = new MainPage();
            LoadingScreen = new LoadingPopUp();
            Startpage = new MasterDetailPage()
            {               
                Master = new NavigationPage(SideMenu) { Title = "Side Menu", BarBackgroundColor = App.MC, BarTextColor = Color.FromHex("#FFFFFF"), },
                Detail = new NavigationPage(Loginpage) { BarBackgroundColor = App.MC, BarTextColor = Color.FromHex("#FFFFFF"),  }
            };
            
            Timer = new System.Timers.Timer
            {
                Interval = 60000
            };
            Timer.Elapsed += OnTimedEvent;
            Timer.Enabled = true;

            
            

            MainPage = Startpage;// new NavigationPage();

            App.Startpage.IsPresentedChanged += (s, e) => {
                SideMenu.TaglistCheck();
            };

            Instanciated = true;
        }

        public void State()
        {
            if (Login)
            {
                MainPage = Loginpage;
                Login = false;
            }
            else
            {
                MainPage = Startpage;
                Login = true;
            }
        }



        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {           
            //Timer.Start();         
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
