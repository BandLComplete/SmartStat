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
		private readonly ScrollView scrollView;


		private readonly Client client = new Client();

		public readonly static Calendar calendar = new Calendar()
		{
			EnableTitleMonthYearView = true,
			StartDay = DayOfWeek.Monday,
			SelectedDate = DateTime.Today,
			DatesTextColor = Color.FromHex("#009e1d"),
			TitleLeftArrowTextColor = Color.FromHex("#009e1d"),
			TitleRightArrowTextColor = Color.FromHex("#009e1d"),
			SelectedBackgroundColor = Color.FromHex("#040052"),
			SelectedTextColor = Color.White,
			SelectedBorderColor = Color.FromHex("#040052"),
			TitleLabelTextColor = Color.FromHex("#040052")
		};

		public readonly Button addingButton = new Button()
		{
			Text = "Создать событие"
		};

		public MyCalendar()
		{
			InitializeComponent();

			Title = "Календарь";

			layout = new StackLayout()
			{
				Children = { calendar, addingButton }
			};

			scrollView = new ScrollView()
			{
				Content = layout
			};

			addingButton.Clicked += AddingButton_Clicked;


			Content = scrollView;
			calendar.DateClicked += DateClickedEvent;
		}

		protected async void DateClickedEvent(object s, EventArgs a)
		{
			layout.Children.Clear();
			layout.Children.Add(calendar);
			layout.Children.Add(addingButton);
			//picker.Items.Clear();

			var arrayOfPractices = await client.GetPractices(new Practice { Date = calendar.SelectedDate.Value });
			for (var i = 0; i < arrayOfPractices.Length; i++)
			{
				var picker = CreaterNewPicker(arrayOfPractices[i]);
				picker.Title = picker.Items[0];
				layout.Children.Add(picker);
			}

			Content = scrollView;
		}


		public Picker CreaterNewPicker(Practice e)
		{
			var picker = new Picker()
			{
				TitleColor = Color.FromHex("040052"),
				TextColor = Color.FromHex("040052"),
			};
			picker.Items.Add("Название тренировки: " + e.Name);
			picker.Items.Add("Время тренировки: " + e.Date.ToString("HH:mm"));
			picker.Items.Add("Продолжительность тренировки: " + e.Length.ToString(@"hh\:mm"));
			picker.Items.Add("Место тренировки: " + e.Place);
			picker.Items.Add("Тип тренировки: " + e.Type);
			picker.Items.Add("Описание тренировки: " + e.Description);
			picker.Items.Add("Тег тренировки: " + e.Tag);

			return picker;
		}

		private async void AddingButton_Clicked(object sender, EventArgs e)
		{
			//await Navigation.PushPopupAsync(new MyPopupPage());
			//await PopupNavigation.PushAsync(new MyPopupPage);
			await Navigation.PushAsync(new AddPracties());
		}
	}
}