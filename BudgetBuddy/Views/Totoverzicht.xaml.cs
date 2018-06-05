using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetBuddy.Properties;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetBuddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Totoverzicht : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<SQL_Uitgaven> _Tots;
        List<double> results = new List<double>();
        private double totalis;
        public Totoverzicht()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            var Tots = await _connection.QueryAsync<SQL_Uitgaven>("SELECT * FROM SQL_Uitgaven ORDER BY Date DESC");

            Total.ItemsSource = Tots;

            foreach (var item in Tots)
            {
                results.Add(item.Value);
                totalis += item.Value;
            }
            if (totalis > 0)
            {
                Totals.TextColor = Color.LawnGreen;
            }
            else
            {
                Totals.TextColor = Color.Red;
            }
            Totals.Text = "€ " + totalis.ToString("0.00");

        }
    }
}