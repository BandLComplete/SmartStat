using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace FirstApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatPage : ContentPage
    {
	    public static string Discipline => Picker.SelectedItem.ToString();

	    private readonly Client client = new Client();
        private static readonly Picker Picker = new Picker();
        private readonly StackLayout layout;
        private readonly StackLayout layoutButton;

        private readonly SfChart chart = new SfChart
        {
	        VerticalOptions = LayoutOptions.FillAndExpand,
	        HorizontalOptions = LayoutOptions.FillAndExpand,
	        Title =
	        {
		        Text = "Здесь будут отображаться Ваши 10 последних тренировок",
		        TextColor = Color.FromHex("#009e1d")
	        },
	        PrimaryAxis = new CategoryAxis
	        {
		        Title =
		        {
			        Text = "Дни тренировок"
		        }
	        },
	        SecondaryAxis = new NumericalAxis
	        {
		        Title =
		        {
			        Text = "Результат"
		        }
	        },
	        Series = new ChartSeriesCollection
	        {
		        new ColumnSeries
		        {
			        ItemsSource = new List<Stat> { new Stat { Date = DateTime.Today } },
			        XBindingPath = "Date",
			        YBindingPath = "Value",
			        DataMarker = new ChartDataMarker(),
			        Color = Color.FromHex("#040052"),
			        EnableDataPointSelection = true,
		        }
	        }
        };
        private IDictionary<string, List<Stat>> stats;
        private readonly Button statButton = new Button
        {
	        Text = "Добавить статистику",
	        HorizontalOptions = LayoutOptions.FillAndExpand, 
        };
        private readonly Button disciplineButton = new Button
        {
            Text = "Добавить дисципилну",
            HorizontalOptions = LayoutOptions.FillAndExpand, 
        };
        private readonly Button deleteDisciplineButton = new Button
        {
            Text = "Удалить дисциплину",
            TextColor = Color.Red,
            HorizontalOptions = LayoutOptions.FillAndExpand, 
        };

        public StatPage()
        {
            Title = "Статистика";
            InitializeComponent();

            RefreshStats();
            RefreshChart();

            statButton.Clicked += async (sender, args) => await Navigation.PushAsync(new AddStat());
            deleteDisciplineButton.Clicked += DeleteDisciplineButtonClicked;
            disciplineButton.Clicked += DisciplineButtonClicked;

            layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            
            layoutButton = new StackLayout
            {
                Children = { statButton, disciplineButton, deleteDisciplineButton},
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand, 
            };
            
            layout.Children.Add(Picker);
            layout.Children.Add(chart);
            layout.Children.Add(layoutButton);

            Content = layout;

            chart.SelectionChanged += Chart_SelectionChanged;
            Picker.SelectedIndexChanged += (sender, args) => RefreshChart();
        }

        private void DeleteDisciplineButtonClicked(object sender, EventArgs e)
        {
            Picker.Items.Remove(Picker.SelectedItem.ToString());
            RefreshChart();
            RefreshPage();
        }
        private async void DisciplineButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDiscipline());
        }

        private void RefreshPage()
        {
            layout.Children.Clear();
            layout.Children.Add(Picker);
            layout.Children.Add(chart);
            layout.Children.Add(layoutButton);
            Content = layout;
        }

        private void Chart_SelectionChanged(object sender, ChartSelectionEventArgs e)
        {
            if (e.SelectedDataPointIndex <= -1) return;
            Navigation.PushAsync(new ResultChanging());
        }

        private void RefreshStats()
        {
	        var stats1 = new[]
	        {
		        new Stat { Name = "Бег", Date = DateTime.Today, Value = 10},
		        new Stat { Name = "Бег", Date = DateTime.Today.AddDays(-2), Value = 6},
		        new Stat { Name = "Бег", Date = DateTime.Today.AddDays(-3), Value = 9},
		        new Stat { Name = "Бег", Date = DateTime.Today.AddDays(-6), Value = 4},
		        
		        new Stat { Name = "Отжимания", Date = DateTime.Today, Value = 35},
		        new Stat { Name = "Отжимания", Date = DateTime.Today.AddDays(-1), Value = 26},
		        new Stat { Name = "Отжимания", Date = DateTime.Today.AddDays(-3), Value = 23},
	        };//await client.GetStats(new Stat { User = MainPage.userName });
	        stats = stats1.Where(x => x.Name != null).GroupToDictionary(x => x.Name);
	        Picker.Items.Clear();
	        foreach (var discipline in stats.Keys)
		        Picker.Items.Add(discipline);

	        if (stats.Any())
		        Picker.SelectedItem = Picker.Items[0];
        }

        private void RefreshChart()
        {
	        chart.Series[0].ItemsSource = stats.TryGetValue(Picker.SelectedItem.ToString(), out var source)
		        ? source
		        : new List<Stat>();
        }
    }
}