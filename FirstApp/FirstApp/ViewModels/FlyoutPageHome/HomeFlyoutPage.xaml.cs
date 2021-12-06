using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeFlyoutPage : FlyoutPage
	{
		public HomeFlyoutPage()
		{
			InitializeComponent();
			FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
			//NavigationPage.SetHasBackButton(this, false);
		}

		private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as HomeFlyoutPageFlyoutMenuItem;
			if (item == null)
				return;

			var page = (Page)Activator.CreateInstance(item.TargetType);
			page.Title = item.Title;

			Detail = new NavigationPage(page);
			IsPresented = false;

			FlyoutPage.ListView.SelectedItem = null;
		}
	}
}