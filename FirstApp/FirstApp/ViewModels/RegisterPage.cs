using Xamarin.Forms;

namespace FirstApp
{
    /// <summary>
    /// Поменять тип стрницы
    /// </summary>
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
                Children = { Name, asseptRegister }
            };
        }
    }
}