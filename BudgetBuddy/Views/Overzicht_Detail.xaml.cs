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
        private ObservableCollection<SQL_Transacties> _uitgaven;
        private ObservableCollection<SQL_Transacties> _uitgavenfilter;
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
            var uitgaven = await _connection.Table<SQL_Transacties>().Where(x => x.Category == category).ToListAsync();
            foreach (var item in uitgaven)
            {
                cat_list.Add(item.Category);
            }
            if (cat_list.Contains(Title))
            {
                _uitgaven = new ObservableCollection<SQL_Transacties>(uitgaven);
                ListView.ItemsSource = _uitgaven;

                foreach (var item in uitgaven)
                {
                    results.Add(item.Value);
                    total += item.Value;
                }
                Soort_label.IsVisible = true;
                Total.IsVisible = true;
                if(total > 0)
                {
                    Soort_label.Text = $"Je totale inkomsten aan {Title} is:";
                    Total.TextColor = Color.LawnGreen;
                }
                else
                {
                    Soort_label.Text = $"Je totale uitgaven aan {Title} is:";
                    Total.TextColor = Color.Red;
                }

                
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

        private async void MainSearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = e.NewTextValue;
            if (keyword == "")
            {
                if (cat_list.Contains(category))
                {
                    ListView.ItemsSource = _uitgaven;
                }
            }
            else if (cat_list.Contains(category))
            {
                var uitgaven = await _connection.QueryAsync<SQL_Transacties>("SELECT * FROM SQL_Transacties WHERE Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE '%" + keyword + "%' AND Category = '" + category + "'");
                _uitgavenfilter = new ObservableCollection<SQL_Transacties>(uitgaven);
                ListView.ItemsSource = _uitgavenfilter;
            }
        }
    }
}