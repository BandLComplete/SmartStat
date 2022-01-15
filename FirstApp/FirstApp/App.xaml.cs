using Xamarin.Forms;

namespace FirstApp
{
	public partial class App : Application
	{
		public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTYyMDc3QDMxMzkyZTM0MmUzMFNhczkxVEhVUDU0U2kzWkMzWlFTWW9EaFpVZlZMVHJ2V01SN2hOaERJT009");
            /*MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#009e1d")
            };*/
            //MainPage = new NavigationPage(new MyCalendar());
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}