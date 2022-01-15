using System;
using Domain;
using Xamarin.Forms;

namespace FirstApp
{
    public class RegisterPage : ContentPage
    {
        private readonly Client client = new Client();

        private readonly Button asseptRegister = new Button
        {
            Text = "Зарегестрироваться"
        };

        private readonly Entry name = new Entry
        {
            Placeholder = "Ввеидте логин"
        };

        private readonly Entry passward = new Entry
        {
            Placeholder = "Ввеидте пароль"
        };


        private readonly Entry repassward = new Entry
        {
            Placeholder = "Повторите пароль"
        };


        public RegisterPage()
        {
            Content = new StackLayout
            {
                Children = { name, passward, repassward, asseptRegister }
            };
            asseptRegister.Clicked += asseptRegister_Clicked;
        }

        private async void asseptRegister_Clicked(object sender, EventArgs e)
        {
            if ((passward.Text != repassward.Text) | (name.Text == null) | (passward.Text == null))
            {
                await DisplayAlert("Повторите попытку", "Пароли не совпадают", "ОК");
            }
            else
            {
                var user = new User { Name = name.Text, Password = passward.Text };
                await client.Register(user);
                Application.Current.MainPage = new AppShell();
            }
        }
    }
}