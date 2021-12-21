using Domain;
using System;
using Xamarin.Forms;

namespace FirstApp
{
	public partial class MainPage : ContentPage
	{
		private readonly Client client = new Client();

		public static string safelogin;

		private readonly Entry login = new Entry()
		{
			Placeholder = "Логин",
			IsTextPredictionEnabled = false
		};

		private readonly Entry passward = new Entry()
		{
			Placeholder = "Пароль",
			IsSpellCheckEnabled = false,
			IsTextPredictionEnabled = false,
			IsPassword = true
		};

		private readonly Button loginButton = new Button()
		{
			Text = "Вход"
		};

		private readonly Button registerButton = new Button()
		{
			Text = "Регистрация"
		};


		public MainPage()
		{
			InitializeComponent();

			registerButton.Clicked += RegisterButton_Clicked;
			loginButton.Clicked += LoginButton_Clicked;


			Content = new StackLayout
			{
				Children = { login, passward, loginButton, registerButton }
			};
		}

		async void LoginButton_Clicked(object sender, EventArgs e)
		{
			var user = new User() { Name = login.Text, Password = passward.Text };
			safelogin = login.Text;
			if (await client.Login(user))
				Application.Current.MainPage = new AppShell();
			else
				await DisplayAlert("Повторите попытку", "Неправильный логин или пароль", "ОК");
		}

		private async void RegisterButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RegisterPage());
		}
	}
}