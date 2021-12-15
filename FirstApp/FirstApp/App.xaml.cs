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
			MainPage = new NavigationPage(new MainPage())
			{
				BarBackgroundColor=Color.FromHex("A8E9AF")
			};
			//MainPage = new HomeFlyoutPage();
            //MainPage = new NavigationPage(new MyCalendar()) 
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