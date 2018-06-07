using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BudgetBuddy.Properties;
using SQLite;

namespace BudgetBuddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inkomsten : ContentPage
    {
        //private ObservableCollection<SQL_Category> _cats;
        private SQLiteAsyncConnection _connection;
        List<string> _cats = new List<string>();
        private double _budget;


        public Inkomsten()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            var cats = await _connection.Table<SQL_Category>().Where(x => x.Income == true).ToListAsync();
            foreach (var item in cats)
            {
                _cats.Add(item.Name);
            }

            Pick_cat.ItemsSource = _cats;
            
        }


        void Entry_Completed(object sender, EventArgs e)
        {
            var text = ((Entry)sender).Text; //cast sender to access the properties of the Entry
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (Pick_cat.SelectedItem == null)
            {
                await DisplayAlert("Alert", "Kies een geldige categorie", "OK");
            }
            else if (Bedrag.Text == null)
            {
                await DisplayAlert("Alert", "Voer een bedrag in", "OK");
            }
            else
            {
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load("cash.wav");

                player.Play();

                
                var inkomsten = new SQL_Transacties(); //link with table
                inkomsten.Date = DateTime.Now;
                inkomsten.Value = Convert.ToDouble(Bedrag.Text, System.Globalization.CultureInfo.InvariantCulture);
                inkomsten.Category = Pick_cat.SelectedItem.ToString();
                inkomsten.Name = Pick_cat.SelectedItem.ToString();
                inkomsten.Recurring = Maand_Inkomst.IsToggled;

                var list_budget = await _connection.QueryAsync<SQL_Transacties>("SELECT * FROM SQL_Budget");

                if (Maand_Inkomst.IsToggled)
                {
                    int s = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    _budget += inkomsten.Value / s;
                    
                    foreach (var item in list_budget)
                    {
                        _budget += item.Value;
                    }
                    await _connection.InsertAsync(inkomsten);
                    await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", _budget, "Budget");
                }
                else if (!Maand_Inkomst.IsToggled)
                {
                    _budget += inkomsten.Value;
                    foreach (var item in list_budget)
                    {
                        _budget += item.Value;
                    }
                    await _connection.InsertAsync(inkomsten);
                    await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", _budget, "Budget");
                }
                await DisplayAlert("Gelukt", "Inkomsten succesvol toegevoegd", "OK");
                await Navigation.PushAsync(new BudgetBuddyPage());
                Navigation.RemovePage(this);
            }
        }
        private void Bedrag_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            {

                var entry = e.NewTextValue;
                var MaxLength = 9999999.99;
                var MinimumLength = 0;
                if (Bedrag.Text.IndexOf('.') == 0 || Bedrag.Text.IndexOf(',') == 0)
                {
                    DisplayAlert("Alert", "Dit is geen geldige invoer", "OK");
                    Bedrag.Text = "";

                }

                else if (entry != "")
                {
                    double _entry = Convert.ToDouble(entry);
                    if (_entry > MaxLength)
                    {
                        DisplayAlert("Alert", "Max bedrag in één transactie is 9999999.99", "OK");
                        Bedrag.Text = "";
                    }

                    if (_entry <= MinimumLength)
                    {
                        DisplayAlert("Alert", "Dit is geen geldige invoer", "OK");
                        Bedrag.Text = "";
                    }
                }
            }
        }
    }
}