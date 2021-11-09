using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FirstApp
{
    public partial class MainPage : ContentPage
    {
        public static readonly Entry login = new Entry()
        {
            Placeholder = "Логин",
            IsTextPredictionEnabled = false
        };

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            var passward = new Entry()
            {
                Placeholder = "Пароль",
                IsSpellCheckEnabled = false,
                IsTextPredictionEnabled = false,
                IsPassword = true
            };

            var loginButton = new Button()
            {
                Text = "Вход"
            };


            var registerButton = new Button()
            {
                Text = "Регистрация"
            };

            registerButton.Clicked += RegisterButton_Clicked;
            loginButton.Clicked += LoginButton_Clicked;


            Content = new StackLayout
            {
                Children = { login, passward, loginButton, registerButton }

            };
        }

        private  void LoginButton_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new HomeFlyoutPage());
            Application.Current.MainPage = new HomeFlyoutPage();

        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
