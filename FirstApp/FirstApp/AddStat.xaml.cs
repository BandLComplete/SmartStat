using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Domain;

namespace FirstApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStat : ContentPage
    {
        private readonly Button addSavingButton = new Button
        {
            Text = "Сохрнаить"
        };

        private readonly Entry addValueEvent = new Entry
        {
            Placeholder = "Введите значение",
            IsTextPredictionEnabled = false
        };

        private readonly Client client = new Client();
        private StackLayout layout;
        
        public AddStat()
        {
            InitializeComponent();
            Title = "Добавление статистики";
            
            layout = new StackLayout
            {
                Children = { addValueEvent,addSavingButton }
            };
            
            Content = layout;
            
            addSavingButton.Clicked += AddSavingButtonClicked;
        }


        private async void AddSavingButtonClicked(object sender, EventArgs e)
        {
            var stat = new Stat()
            {
                Name = StatPage.Discipline,
                Date = DateTime.Today,
                User = MainPage.userName,
                Value = int.TryParse(addValueEvent.Text, out var value) ? value : 0
            };
            await client.PatchStat(stat,DbAction.Add);
            await Navigation.PopAsync();
            
        }
    }
}