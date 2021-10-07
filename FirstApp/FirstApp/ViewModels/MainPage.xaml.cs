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
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var login = new Entry()
            {
                Placeholder = "Логин"
            };

            var passward = new Entry()
            {
                Placeholder = "Пароль",
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


            Content = new StackLayout
            {
                Children = { login, passward, loginButton, registerButton }

            };
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
