using System;
using System.Collections.Generic;
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
    public partial class Uitgaven : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private double _Bedrag;
        private double _budget;
        List<string> _cats = new List<string>();

        public Uitgaven()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        }

        protected override async void OnAppearing()
        {
            var cats = await _connection.Table<SQL_Category>().Where(x => x.Income == false).ToListAsync();
            foreach (var item in cats)
            {
                _cats.Add(item.Name);
            }

            Pick_cat.ItemsSource = _cats;

        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            _Bedrag = Convert.ToDouble(Bedrag.Text, System.Globalization.CultureInfo.InvariantCulture);
            if (Pick_cat.SelectedItem == null)
            {
                await DisplayAlert("Alert", "Kies een geldige categorie", "OK");
            }
            else if (Bedrag.Text == null)
            {
                await DisplayAlert("Alert", "Voer een bedrag in", "OK");
            }
            else if (_Bedrag > 9999999.99)
            {

            }
            else
            {
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load("cash.wav");

                player.Play();
                

                var uitgaven = new SQL_Transacties { };
                uitgaven.Date = DateTime.Now;
				uitgaven.Value = -double.Parse(Bedrag.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);            
                uitgaven.Category = Pick_cat.SelectedItem.ToString();
                uitgaven.Recurring = Vaste_Lasten.IsToggled;
                if (Naam.Text == null)
                {
                    uitgaven.Name = Pick_cat.SelectedItem.ToString();
                }
                else
                {
                    uitgaven.Name = Naam.Text;
                }
                



                var list_budget = await _connection.QueryAsync<SQL_Transacties>("SELECT * FROM SQL_Budget");

                if (Vaste_Lasten.IsToggled)
                {
                    int s = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    _budget += uitgaven.Value / s;
                    
                    foreach (var item in list_budget)
                    {
                        _budget += item.Value;
                    }
                    await _connection.InsertAsync(uitgaven);
                    await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", _budget, "Budget");
                }
                else if(!Vaste_Lasten.IsToggled)
                {
                    _budget += uitgaven.Value;
                    foreach (var item in list_budget)
                    {
                        _budget += item.Value;
                    }
                    await _connection.InsertAsync(uitgaven);
                    await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", _budget, "Budget");
                }

                await DisplayAlert("Gelukt", "Uitgaven succesvol toegevoegd", "OK");
                await Navigation.PushAsync(new BudgetBuddyPage());
                Navigation.RemovePage(this);
            }




        }

        private void Naam_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            {
                var entry = (Entry)sender;
                var MaxLength = 150;
                if (entry.Text.Length > MaxLength)
                {
                    string entryText = entry.Text;
                    entry.TextChanged -= Naam_OnTextChanged;
                    entry.Text = e.OldTextValue;
                    entry.TextChanged += Naam_OnTextChanged;
                }
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

                    if (_entry < MinimumLength)
                    {
                        DisplayAlert("Alert", "Dit is geen geldige invoer", "OK");
                        Bedrag.Text = "";
                    }
                }
            }
        }
    }
}