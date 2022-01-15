using System;
using Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;

namespace FirstApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyCalendar : ContentPage
    {
        public static readonly Calendar Calendar = new Calendar
        {
            EnableTitleMonthYearView = true,
            StartDay = DayOfWeek.Monday,
            SelectedDate = DateTime.Today,
            DatesTextColor = Color.FromHex("#009e1d"),
            TitleLeftArrowTextColor = Color.FromHex("#009e1d"),
            TitleRightArrowTextColor = Color.FromHex("#009e1d"),
            SelectedBackgroundColor = Color.FromHex("#040052"),
            SelectedTextColor = Color.White,
            SelectedBorderColor = Color.FromHex("#040052"),
            TitleLabelTextColor = Color.FromHex("#040052")
        };

        private readonly Button addingButton = new Button
        {
            Text = "Создать событие"
        };


        private readonly Client client = new Client();
        private readonly StackLayout layout;
        private readonly ScrollView scrollView;

        public MyCalendar()
        {
            InitializeComponent();

            Title = "Календарь";

            layout = new StackLayout
            {
                Children = { Calendar, addingButton }
            };

            scrollView = new ScrollView
            {
                Content = layout
            };

            addingButton.Clicked += AddingButton_Clicked;


            Content = scrollView;
            Calendar.DateClicked += DateClickedEvent;
        }

        private async void DateClickedEvent(object s, EventArgs a)
        {
            layout.Children.Clear();
            layout.Children.Add(Calendar);
            layout.Children.Add(addingButton);
            //picker.Items.Clear();

            var arrayOfPractices = await client.GetPractices(new Practice { Date = Calendar.SelectedDate.Value,
                                                                                    Users = new []{MainPage.userName}});
            foreach (var t in arrayOfPractices)
            {
                var picker = CreaterNewPicker(t);
                picker.Title = picker.Items[0];
                layout.Children.Add(picker);
            }

            Content = scrollView;
        }


        private Picker CreaterNewPicker(Practice e)
        {
            var picker = new Picker
            {
                TitleColor = Color.FromHex("040052"),
                TextColor = Color.FromHex("040052"),
                Items =
                {
                    "Название тренировки: " + e.Name,
                    "Время тренировки: " + e.Date.ToString("HH:mm"),
                    "Продолжительность тренировки: " + e.Length.ToString(@"hh\:mm"),
                    "Место тренировки: " + e.Place,
                    "Тип тренировки: " + e.Type,
                    "Описание тренировки: " + e.Description,
                    "Тег тренировки: " + e.Tag
                }
            };
            return picker;
        }

        private async void AddingButton_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushPopupAsync(new MyPopupPage());
            //await PopupNavigation.PushAsync(new MyPopupPage);
            await Navigation.PushAsync(new AddPracties());
        }
    }
}