using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Collections.ObjectModel;
using BudgetBuddy.Properties;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetBuddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Overzicht_Detail : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<SQL_Uitgaven> _uitgaven;
        private ObservableCollection<SQL_Uitgaven> _uitgavenfilter;
        private ObservableCollection<SQL_Inkomsten> _inkomsten;
        private ObservableCollection<SQL_Inkomsten> _inkomstenfilter;
        private String category;
        List<double> results = new List<double>();
        List<string> cat_list = new List<string>();
        List<string> cat_list2 = new List<string>();

        public double total;
        //private DateTime _datum = DateTime.UtcNow.AddDays(-4);





        public Overzicht_Detail(string data)
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            category = data;

            
        }
        protected override async void OnAppearing()
        {
            Title = category;
            Soort_label.IsVisible = false;
            Total.IsVisible = false;
            var uitgaven = await _connection.Table<SQL_Uitgaven>().Where(x => x.Category == category).ToListAsync();
            var inkomsten = await _connection.Table<SQL_Inkomsten>().Where(x => x.Category == category).ToListAsync();
            foreach (var item in uitgaven)
            {
                cat_list.Add(item.Category);
            }
            foreach (var item in inkomsten)
            {
                cat_list2.Add(item.Category);
            }
            if (cat_list.Contains(Title))
            {
                _uitgaven = new ObservableCollection<SQL_Uitgaven>(uitgaven);
                ListView.ItemsSource = _uitgaven;

                foreach (var item in uitgaven)
                {
                    results.Add(item.Value);
                    total += item.Value;
                }
                Soort_label.IsVisible = true;
                Total.IsVisible = true;
                Soort_label.Text = "Je totale uitgaven zijn:";
            }
            else if (cat_list2.Contains(Title))
            {
                
                _inkomsten = new ObservableCollection<SQL_Inkomsten>(inkomsten);
                ListView.ItemsSource = _inkomsten;

                foreach (var item in inkomsten)
                {
                    results.Add(item.Value);
                    total += item.Value;
                }
                Soort_label.IsVisible = true;
                Total.IsVisible = true;
                Soort_label.Text = "Je totale inkomsten zijn:";

            }
            else
            {
                Soort_label.IsVisible = true;
                Soort_label.Text = "Nog geen transacties in deze categorie";
            }

            Total.Text = "€ " + total.ToString("0.00");
            System.Diagnostics.Debug.WriteLine(total);
            base.OnAppearing();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selected = e.SelectedItem as SQL_Uitgaven;
            DisplayAlert("Alert", selected.Value.ToString(), "OK");
        }

        private async void MainSearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = e.NewTextValue;
            if (keyword == "")
            {
                if (cat_list.Contains(category))
                {
                    ListView.ItemsSource = _uitgaven;
                }
                else if (cat_list2.Contains(category))
                {
                    ListView.ItemsSource = _inkomsten;
                }
            }
            else if (cat_list.Contains(category))
            {
                var uitgaven = await _connection.QueryAsync<SQL_Uitgaven>("SELECT * FROM SQL_Uitgaven WHERE Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE '%" + keyword + "%' AND Category = '" + category + "'");
                _uitgavenfilter = new ObservableCollection<SQL_Uitgaven>(uitgaven);
                ListView.ItemsSource = _uitgavenfilter;
            }
            else if (cat_list2.Contains(category))
            {
                var inkomsten = await _connection.QueryAsync<SQL_Inkomsten>("SELECT * FROM SQL_Uitgaven WHERE Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE '%" + keyword + "%' AND Category = '" + category + "'");
                _inkomstenfilter = new ObservableCollection<SQL_Inkomsten>(inkomsten);
                ListView.ItemsSource = _inkomstenfilter;
            }
        }
    }
}