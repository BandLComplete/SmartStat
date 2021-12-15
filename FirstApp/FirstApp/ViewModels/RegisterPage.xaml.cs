using Domain;
using System;
using Xamarin.Forms;

namespace FirstApp
{
    public partial class RegisterPage : ContentPage
    {

        private readonly Client client = new Client();
        private Entry name = new Entry()
        {
            Placeholder = "Ввеидте логин"
        };

        private Entry passward = new Entry()
        {
            Placeholder = "Ввеидте пароль"
        };


        private Entry repassward = new Entry()
        {
            Placeholder = "Повторите пароль"
        };

        private Button asseptRegister = new Button()
        {
            Text = "Зарегестрироваться"
        };


        public RegisterPage()
        {
            Content = new StackLayout
            {
                Children = { name, passward, repassward, asseptRegister }
            };
            asseptRegister.Clicked += asseptRegister_Clicked;
        }

        async void asseptRegister_Clicked(object sender, EventArgs e)
        {
            if (passward.Text != repassward.Text | name.Text == null | passward.Text == null)
                await DisplayAlert("Повторите попытку", "Пароли не совпадают", "ОК");
            else
            {
                var user = new User() { Name = name.Text, Password = passward.Text };
                await client.Register(user);
                Application.Current.MainPage = new HomeFlyoutPage();
            }                   
        }
    }
}