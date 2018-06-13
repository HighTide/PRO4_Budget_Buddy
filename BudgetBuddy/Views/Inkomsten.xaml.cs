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
using Microsoft.AppCenter.Analytics;

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

            //checks if this is first use of app, if so, execute this
            if (!App.Current.Properties.ContainsKey("savedPropA"))
            {
                Maand_Inkomst.IsVisible = false;
                Maand_Inkomst.IsToggled = true;
                Ga_verder.IsVisible = true;
                Mnd_inkmostlbl.IsVisible = false;
                Top_lbl.FontSize = 15;
                Top_lbl.Text = "Voeg hier uw maandelijke inkomsten toe. Het is mogelijk om dit meerdere malen te doen! Dit kan ook later nog in de App.";
            }
            else
            {
                Ga_verder.IsVisible = false;
            }
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


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            int s = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
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
				inkomsten.Value = double.Parse(Bedrag.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);    
                inkomsten.Category = Pick_cat.SelectedItem.ToString();
                inkomsten.Recurring = Maand_Inkomst.IsToggled;
                if (Naam.Text == null)
                {
                    inkomsten.Name = Pick_cat.SelectedItem.ToString();
                }
                else
                {
                    inkomsten.Name = Naam.Text;
                }

                var list_budget = await _connection.QueryAsync<SQL_Transacties>("SELECT * FROM SQL_Budget");

                if (Maand_Inkomst.IsToggled)
                {
                    
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
                if (App.Current.Properties.ContainsKey("savedPropA"))
                { 
					Analytics.TrackEvent("Inkomsten Aangemaakt");
                    await Navigation.PushAsync(new BudgetBuddyPage());
                    Navigation.RemovePage(this);
                }
            }
        }

        private void Button_OnClicked2(object sender, EventArgs e)
        {
            App.Current.MainPage = new Uitgaven();
        }

        private void Bedrag_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            {

                var entry = e.NewTextValue;
                var MaxLength = 9999999.99;
                var MinimumLength = 0;
                //if (Bedrag.Text.IndexOf('.') == 0 || Bedrag.Text.IndexOf(',') == 0)
                //{
                //    DisplayAlert("Alert", "Dit is geen geldige invoer", "OK");
                //    Bedrag.Text = "";

                //}

                //else

					if (entry != "")
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