using System;
using Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FirstApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPracties : ContentPage
    {
        private readonly Button addEventButton = new Button
        {
            Text = "Добавить событие"
        };
        //private  readonly TimePicker timeOfNotification = new TimePicker();   Таймер для выбора времени уведомления


        private readonly Client client = new Client();

        private readonly Entry descriptionEvent = new Entry
        {
            Placeholder = "Введите описание тренировки",
            IsTextPredictionEnabled = false
        };

        private readonly Entry friendUsersEvent = new Entry
        {
            Placeholder = "Введите логины друзей",
            IsTextPredictionEnabled = false
        };

        private readonly Entry nameEvent = new Entry
        {
            Placeholder = "Введите название тренировки",
            IsTextPredictionEnabled = false
        };

        private readonly Entry placeEvent = new Entry
        {
            Placeholder = "Введите место тренировки",
            IsTextPredictionEnabled = false
        };

        private readonly char[] separators = { ' ', ',', '.' };

        private readonly Entry tagEvent = new Entry
        {
            Placeholder = "Придумайте хэштеги к тренировке",
            IsTextPredictionEnabled = false
        };


        private readonly TimePicker timeEndOfEvent = new TimePicker();

        private readonly TimePicker timePicker = new TimePicker();

        private readonly Entry typeEvent = new Entry
        {
            Placeholder = "Введите тип тренировки",
            IsTextPredictionEnabled = false
        };

        public AddPracties()
        {
            InitializeComponent();

            addEventButton.Clicked += AddEventButton_Clicked;


            Content = new StackLayout
            {
                Children =
                {
                    nameEvent,
                    new Label
                    {
                        Text = "Введите начало тренировки"
                    },
                    timePicker,
                    new Label
                    {
                        Text = "Введите окончание тренировки"
                    },
                    timeEndOfEvent,
                    placeEvent,
                    typeEvent,
                    descriptionEvent,
                    tagEvent,
                    friendUsersEvent,
                    //timeOfNotification,
                    addEventButton
                }
            };
        }

        private async void AddEventButton_Clicked(object sender, EventArgs e)
        {
            var practice = new Practice
            {
                Date = MyCalendar.calendar.SelectedDate.Value + timePicker.Time,
                Name = nameEvent.Text,
                Length = timePicker.Time - timeEndOfEvent.Time,
                Place = placeEvent.Text,
                Type = typeEvent.Text,
                Description = descriptionEvent.Text,
                Tag = tagEvent.Text,
                Users = (MainPage.safelogin + friendUsersEvent.Text).Split(separators,
                    StringSplitOptions.RemoveEmptyEntries)
            };

            await client.AddPractice(practice);
            await Navigation.PopAsync();
        }
    }
}