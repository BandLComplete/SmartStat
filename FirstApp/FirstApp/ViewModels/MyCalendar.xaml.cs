using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using FirstApp.Tests;
using FirstApp.Service;

namespace FirstApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyCalendar : ContentPage
	{
		public StackLayout layout = new StackLayout()
		{
			Children = {calendar}
		};

		public readonly static Calendar calendar = new Calendar()
		{
			EnableTitleMonthYearView = true,
			StartDay = DayOfWeek.Monday,
			SelectedDate = DateTime.Today
		};

		public MyCalendar()
		{
			InitializeComponent();

			var AddingButton = new Button()
			{
				Text = "Создать событие"
			};

			AddingButton.Clicked += AddingButton_Clicked;
			layout.Children.Add(AddingButton);


			//{

			//new Label
			//{
			//    Text = "Название тренировки: " + Class1.practies.Name,
			//},
			//new Label
			//{
			//    //Text = "Время тренировки: " + Class1.practies.Date.Hour+":"+Class1.practies.Date.Minute,
			//    Text = "Время тренировки: " + Class1.practies.Date.ToString("HH:mm")
			//},
			//new Label
			//{
			//    Text = "Продолжительность тренировки: " + Class1.practies.Length.ToString(@"hh\:mm")
			//},
			//new Label
			//{
			//    Text = "Место тренировки: " + Class1.practies.Place,
			//},
			//new Label
			//{
			//    Text = "Тип тренировки: " + Class1.practies.Type,
			//},
			//new Label
			//{
			//    Text = "Описание тренировки: " + Class1.practies.Description,
			//},
			//new Label
			//{
			//    Text = "Тег тренировки: " + Class1.practies.Tag,
			//},

			//}
			//};

			Content = layout;
			calendar.DateClicked += DateClickedEvent;
		}

		protected void DateClickedEvent(object s, EventArgs a)
		{
			if (Class1.practies.Date.Date == calendar.SelectedDate)
			{
				var listOfLabels = CreaterNewLabel();
				foreach (var e in listOfLabels)
				{
					layout.Children.Add(e);
				}

				Content = layout;
			}
		}

		public List<Label> CreaterNewLabel()
		{
			var listOfLabels = new List<Label>
			{
				new Label
				{
					Text = "Название тренировки: " + Class1.practies.Name,
				},
				new Label
				{
					//Text = "Время тренировки: " + Class1.practies.Date.Hour+":"+Class1.practies.Date.Minute,
					Text = "Время тренировки: " + Class1.practies.Date.ToString("HH:mm")
				},

				new Label
				{
					Text = "Продолжительность тренировки: " + Class1.practies.Length.ToString(@"hh\:mm")
				},

				new Label
				{
					Text = "Место тренировки: " + Class1.practies.Place,
				},

				new Label
				{
					Text = "Тип тренировки: " + Class1.practies.Type,
				},

				new Label
				{
					Text = "Описание тренировки: " + Class1.practies.Description,
				},
				new Label
				{
					Text = "Тег тренировки: " + Class1.practies.Tag,
				}
			};
			return listOfLabels;
		}

		private async void AddingButton_Clicked(object sender, EventArgs e)
		{
			//await Navigation.PushPopupAsync(new MyPopupPage());
			//await PopupNavigation.PushAsync(new MyPopupPage);
			await Navigation.PushAsync(new AddPracties());
		}
	}
}