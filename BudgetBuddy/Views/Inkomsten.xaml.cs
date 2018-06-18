using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        private ObservableCollection<SQL_Category> _Categories;
        
        private double _budget;
        List<string> recurList = new List<string>{"Maandelijks", "Per kwartaal", "Jaarlijks"};


        public Inkomsten()
        {

            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            recurtype.ItemsSource = recurList;

            var profileTapRecognizer = new TapGestureRecognizer
            {
                TappedCallback = (v, o) => { Navigation.PushAsync(new Addcatagory()); },
                NumberOfTapsRequired = 1
            };
            plusbutton.GestureRecognizers.Add(profileTapRecognizer);
            //checks if this is first use of app, if so, execute this
            if (!App.Current.Properties.ContainsKey("savedPropA"))
            {
                Maand_Inkomst.IsVisible = false;
                Maand_Inkomst.IsToggled = true;
                Ga_verder.IsVisible = true;
                Mnd_inkmostlbl.IsVisible = false;
                Top_lbl.FontSize = 15;
                Top_lbl.Text = "Om te beginnen is het aangeraden om een vaste inkomst in te voeren(bijv. salaris), Het is later mogelijk om meer (vaste) inkomsten toe te voegen";
                recurtype.IsVisible = true;
                recurtypelbl.IsVisible = true;
                plusbutton.IsVisible = false;
            }
            else
            {
                Ga_verder.IsVisible = false;
            }
        }


        protected override async void OnAppearing()
        {
            List<string> _cats = new List<string>();
            if (App.Current.Properties.ContainsKey("savedPropB"))
            {
                plusbutton.Source = "Add.png";

            }
            else
            {
                plusbutton.Source = "Addblack.png";
            }





            var cats = await _connection.Table<SQL_Category>().Where(x => x.Income == true).ToListAsync();
            _Categories = new ObservableCollection<SQL_Category>(cats);
            foreach (var item in _Categories)
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
            int k = GetDaysInYear(DateTime.Now.Year);
            if (Pick_cat.SelectedItem == null)
            {
                await DisplayAlert("Alert", "Kies een geldige categorie", "OK");
            }
            else if (Bedrag.Text == null)
            {
                await DisplayAlert("Alert", "Voer een bedrag in", "OK");
            }
            else if (double.Parse(Bedrag.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture) ==
                     0.00)
            {
                await DisplayAlert("Alert", "Voer een geldig bedrag in", "OK");
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
                inkomsten.Recurtype = "";
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
                    inkomsten.Recurtype = recurtype.SelectedItem.ToString();
                    inkomsten.Name += " - " + recurtype.SelectedItem.ToString();
                    await _connection.InsertAsync(inkomsten);

                    inkomsten.Recurring = false;
                    inkomsten.Name = $"Update budget - {inkomsten.Category}";
                    // if it is monthly income
                    if (recurtype.SelectedItem.ToString() == "Maandelijks")
                    { 
                        _budget += inkomsten.Value / s;
                        inkomsten.Value /= s;
                        
                        foreach (var item in list_budget)
                        {
                            _budget += item.Value;
                        }

                        await _connection.InsertAsync(inkomsten);
                        await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", _budget, "Budget");
                    }
                    // if it is quartarly income
                    else if (recurtype.SelectedItem.ToString() == "Per kwartaal")
                    {
                        _budget += inkomsten.Value / (k/4);
                        inkomsten.Value /= (k / 4);
                        foreach (var item in list_budget)
                        {
                            _budget += item.Value;
                        }

                        await _connection.InsertAsync(inkomsten);
                        await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", _budget, "Budget");
                    }
                    // if it is a yearly incme
                    else if (recurtype.SelectedItem.ToString() == "Jaarlijks")
                    {
                        _budget += inkomsten.Value / k;
                        inkomsten.Value /= k;
                        foreach (var item in list_budget)
                        {
                            _budget += item.Value;
                        }

                        await _connection.InsertAsync(inkomsten);
                        await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", _budget, "Budget");
                    }
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
                else
                {
                    App.Current.MainPage = new Uitgaven();
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
                if (Bedrag.Text.IndexOf('.') == 0 || Bedrag.Text.IndexOf(',') == 0)
                {
                    DisplayAlert("Alert", "Dit is geen geldige invoer", "OK");
                    Bedrag.Text = "";

                }
                else if (entry == "-" || entry == "+")
                {
                    DisplayAlert("Alert", "Dit is geen geldige invoer", "OK");
                    Bedrag.Text = "";
                }

                else

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

        private void Maand_Inkomst_OnToggled(object sender, ToggledEventArgs e)
        {
            
            if (e.Value)
            {
                recurtype.SelectedIndex = 0;
                recurtype.IsVisible = true;
                recurtypelbl.IsVisible = true;
            }
            if (!e.Value)
            {
                recurtype.IsVisible = false;
                recurtypelbl.IsVisible = false;
            }
        }
        public static int GetDaysInYear(int year)
        {
            var thisYear = new DateTime(year, 1, 1);
            var nextYear = new DateTime(year + 1, 1, 1);

            return (nextYear - thisYear).Days;
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Information2());
        }
    }
}