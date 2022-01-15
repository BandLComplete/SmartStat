using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Domain;

namespace FirstApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDiscipline : ContentPage
    {
        
        private readonly Button addSavingButton = new Button
        {
            Text = "Сохрнаить"
        };
        
        
        private readonly Entry addDisciplineEvent = new Entry
        {
            Placeholder = "Введите название дисциплины, которую хотите добавить",
            IsTextPredictionEnabled = false
        };

        private Client client = new Client();

        private StackLayout layout;
        public AddDiscipline()
        {
            InitializeComponent();
            Title = "Редактирование дисциплин";
            
            layout = new StackLayout
            {
                Children = { addDisciplineEvent,addSavingButton }
            };
            
            Content = layout;
            
            addSavingButton.Clicked += AddSavingButtonClicked;
        }


        private async void AddSavingButtonClicked(object sender, EventArgs e)
        {
            var stat = new Stat()
            {
                Name = addDisciplineEvent.Text,
                Date = DateTime.Today,
                User = MainPage.userName
                
            };
            await client.PatchStat(stat,DbAction.Add);
            await Navigation.PopAsync();
            
        }
    }
}