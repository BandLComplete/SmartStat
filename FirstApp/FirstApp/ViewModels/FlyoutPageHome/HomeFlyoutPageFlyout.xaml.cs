using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeFlyoutPageFlyout : ContentPage
	{
		public ListView ListView;

		public HomeFlyoutPageFlyout()
		{
			InitializeComponent();

			BindingContext = new HomeFlyoutPageFlyoutViewModel();
			ListView = MenuItemsListView;
		}

		class HomeFlyoutPageFlyoutViewModel : INotifyPropertyChanged
		{
			public ObservableCollection<HomeFlyoutPageFlyoutMenuItem> MenuItems { get; set; }

			public HomeFlyoutPageFlyoutViewModel()
			{
				MenuItems = new ObservableCollection<HomeFlyoutPageFlyoutMenuItem>(new[]
				{
					new HomeFlyoutPageFlyoutMenuItem {Id = 0, Title = "Календарь", TargetType = typeof(MyCalendar)}
				});
			}

			#region INotifyPropertyChanged Implementation

			public event PropertyChangedEventHandler PropertyChanged;

			void OnPropertyChanged([CallerMemberName] string propertyName = "")
			{
				if (PropertyChanged == null)
					return;

				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}

			#endregion
		}
	}
}