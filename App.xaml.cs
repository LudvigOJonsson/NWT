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
        public static bool Online = true;
        public static System.Timers.Timer Timer;
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
                Master = new NavigationPage(SideMenu) { Title = "Side Menu", BarBackgroundColor = Color.FromHex("#2f6e83"), BarTextColor = Color.FromHex("#FFFFFF"), },
                Detail = new NavigationPage(Mainpage) { BarBackgroundColor = Color.FromHex("#2f6e83"), BarTextColor = Color.FromHex("#FFFFFF"),  }
            };

            Timer = new System.Timers.Timer();
            Timer.Interval = 60000;
            Timer.Elapsed += OnTimedEvent;
            Timer.Enabled = true;

            MainPage = Startpage;
            Instanciated = true;
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(App.LoggedinUser != null)
            {
                App.database.UpdateStats("UseTime");
            }            
            Timer.Start();         
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
