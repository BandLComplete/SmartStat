using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FirstApp
{
    public class RegisterPage : ContentPage
    {
        public RegisterPage()
        {

            var Name = new Entry()
            {
                Placeholder = "Имя"
            };

            var asseptRegister = new Button()
            {
                Text = "Зарегестрироваться"
            };



            Content = new StackLayout
            {
                Children = {Name,asseptRegister}

            };
        }
    }
}