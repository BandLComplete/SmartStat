
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;

namespace FirstApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyCalendar : ContentPage
    {
        private readonly Label actionLabel;

        public MyCalendar()
        {
            InitializeComponent();

            actionLabel = new Label()
            {
                Text="ahahahhaha"
            };
            

            var calendar = new Calendar()
            {
                EnableTitleMonthYearView = true,
                StartDay = DayOfWeek.Monday,
                SelectedDate = DateTime.Today
                


            };

            var AddingButton = new Button()
            {
                Text = "Создать событие"
            };

            AddingButton.Clicked += AddingButton_Clicked;

                  

            Content = new StackLayout
            {
                Children = { calendar, AddingButton, actionLabel}
            };
        }


        protected void DateClickedEvent(object s, EventArgs a)
        {
            actionLabel.Text = "ergqergqeqg";
        }

        private async void AddingButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new MyPopupPage());
            //await PopupNavigation.PushAsync(new MyPopupPage);
        }
    }
}