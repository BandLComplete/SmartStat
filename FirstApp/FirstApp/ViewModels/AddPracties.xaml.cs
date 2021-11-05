using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPracties : ContentPage
    {
        public AddPracties()
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
                Children = { timePicker, NameEvent, AddEventButton }
            };
        }

        async void AddEvantButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    
    }
}