using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;
using Domain;
using System.Linq;

namespace FirstApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyCalendar : ContentPage
	{
		private readonly StackLayout layout;

		private readonly Client client = new Client();


		public readonly static Calendar calendar = new Calendar()
		{
			EnableTitleMonthYearView = true,
			StartDay = DayOfWeek.Monday,
			SelectedDate = DateTime.Today
		};

		public readonly  Button addingButton = new Button()
		{
			Text = "Создать событие"
		};

		public MyCalendar()
		{
			InitializeComponent();

			layout= new StackLayout()
			{
				Children = { calendar, addingButton }
			};

			addingButton.Clicked += AddingButton_Clicked;
			

			Content = layout;
			calendar.DateClicked += DateClickedEvent;
		}

		protected async void  DateClickedEvent(object s, EventArgs a)
		{

			//layout.Children.Clear();
			//layout.Children.Add(calendar);
			//layout.Children.Add(addingButton);
			var i = layout.Children.Count;
			while (i>2)
            {
				layout.Children.RemoveAt(i);
				i--;
            }

			var av = await client.GetPractices(new Practice { Date = calendar.SelectedDate.Value });
			foreach (var e in av.SelectMany(x=>CreaterNewLabel(x)))
			{
				layout.Children.Add(e);
			}
			Content = layout;
		}

		public IEnumerable<Label> CreaterNewLabel(Practice e)
		{
			yield return new Label
			{
				Text = "Название тренировки: " + e.Name,
			};
			yield return new Label
			{
				Text = "Время тренировки: " + e.Date.ToString("HH:mm"),
			};
			yield return new Label
			{
				Text = "Продолжительность тренировки: " + e.Length.ToString(@"hh\:mm"),
			};
			yield return new Label
			{
				Text = "Место тренировки: " + e.Place,
			};
			yield return new Label
			{
				Text = "Тип тренировки: " + e.Type,
			};
			yield return new Label
			{
				Text = "Описание тренировки: " + e.Description,
			};
			yield return new Label
			{
				Text = "Тег тренировки: " + e.Tag,
			};		
		}

		private async void AddingButton_Clicked(object sender, EventArgs e)
		{
			//await Navigation.PushPopupAsync(new MyPopupPage());
			//await PopupNavigation.PushAsync(new MyPopupPage);
			await Navigation.PushAsync(new AddPracties());
		}
	}
}