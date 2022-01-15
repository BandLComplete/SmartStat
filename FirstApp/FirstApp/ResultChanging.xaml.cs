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
    public partial class ResultChanging : ContentPage
    {
        private readonly Entry resultEvent = new Entry
        {
            Placeholder = "Тут будет число",
            IsReadOnly = true,
            IsTextPredictionEnabled = false
        };
        
        private readonly Button changeButton = new Button
        {
            Text = "Редактировать",
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        private readonly Button deleteResultButton= new Button
        {
            Text = "Удалить результат",
            TextColor = Color.Red,
            HorizontalOptions = LayoutOptions.EndAndExpand, 
        };

        private StackLayout layout;
        private StackLayout buttonLayout;
        public ResultChanging()
        {
            InitializeComponent();
            Title = "Изменение результатов";


            buttonLayout = new StackLayout
            {
                Children = {changeButton, deleteResultButton},
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            layout = new StackLayout
            {
                Children = { resultEvent, buttonLayout }
            };
            
            Content = layout;
            
            changeButton.Clicked += ChangeButtonClicked;
            deleteResultButton.Clicked += DeleteResultButtonClicked;




        }

        private void ChangeButtonClicked(object sender, EventArgs e)
        {
            if (resultEvent.IsReadOnly)
            {
                resultEvent.IsReadOnly = false;
                changeButton.Text = "Сохрнаить";
            }
            else
            {
                Navigation.PopAsync();
            }
            changeButton.Clicked += ChangeButtonClicked;
        }
        
        private void DeleteResultButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}