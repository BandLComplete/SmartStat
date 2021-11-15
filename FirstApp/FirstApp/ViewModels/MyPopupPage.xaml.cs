using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms;

namespace FirstApp
{
	public partial class MyPopupPage : PopupPage
	{
		public MyPopupPage()
		{
			InitializeComponent();


			var timePicker = new TimePicker()
			{
				Time = new TimeSpan()
			};

			var NameEvent = new Entry()
			{
				Placeholder = "Введите название события"
			};

			var AddEventButton = new Button()
			{
				Text = "Добавить событие"
			};

			AddEventButton.Clicked += AddEvantButton_Clicked;


			Content = new StackLayout
			{
				Children = {timePicker, NameEvent, AddEventButton}
			};
		}

		async void AddEvantButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}


		//protected override void OnAppearing()
		//{
		//    base.OnAppearing();
		//}

		//protected override void OnDisappearing()
		//{
		//    base.OnDisappearing();
		//}

		//// ### Methods for supporting animations in your popup page ###

		//// Invoked before an animation appearing
		//protected override void OnAppearingAnimationBegin()
		//{
		//    base.OnAppearingAnimationBegin();
		//}

		//// Invoked after an animation appearing
		//protected virtual Task OnAppearingAnimationEnd()
		//{
		//    return Content.FadeTo(1); ;
		//}

		//// Invoked before an animation disappearing
		//protected virtual Task OnDisappearingAnimationBegin()
		//{
		//    return Content.FadeTo(1); ;
		//}

		//// Invoked after an animation disappearing


		//// ### Overrided methods which can prevent closing a popup page ###

		//// Invoked when a hardware back button is pressed
		//protected override bool OnBackButtonPressed()
		//{
		//    // Return true if you don't want to close this popup page when a back button is pressed
		//    return true;
		//}

		//// Invoked when background is clicked
		//protected override bool OnBackgroundClicked()
		//{
		//    // Return false if you don't want to close this popup page when a background of the popup page is clicked
		//    return base.OnBackgroundClicked();
		//}
	}
}