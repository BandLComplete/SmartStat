using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstApp
{
	public partial class App : Application
	{
		public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new MainPage())
            //{
            //    BarBackgroundColor = Color.FromHex("#009e1d")
            //};
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