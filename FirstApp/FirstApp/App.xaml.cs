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
            //MainPage = new NavigationPage(new MainPage());
            MainPage = new HomeFlyoutPage()
            {

            };
            //MainPage = new NavigationPage(new MyCalendar())
            //{
            //    BarBackgroundColor=Color.FromHex("A8E9AF")
            //};

            //       var contentPageStyle = new Style(typeof(ContentPage))
            //       {
            //           Setters =
            //           {
            //               new Setter
            //               {
            //                   Property = ContentPage.BarBackgroundProperty, Value=Color.FromHex("A8E9AF")
            //}
            //           }
            //       };

            //       MainPage = new NavigationPage(new MyCalendar())
            //       {
            //           Style = contentPageStyle
            //       };




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