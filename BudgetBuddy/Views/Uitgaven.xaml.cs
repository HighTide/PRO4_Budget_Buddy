using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetBuddy.Properties;
using Microsoft.AppCenter.Analytics;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetBuddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Uitgaven : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<SQL_Category> _Categories;
        private double _Bedrag;
        private double _budget;
        
        List<string> recurList = new List<string> { "Maandelijks", "Per kwartaal", "Jaarlijks" };
        int s = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

        public Uitgaven()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            recurtype.ItemsSource = recurList;

            //checks if this is first use of app, if so, execute this
            if (!App.Current.Properties.ContainsKey("savedPropA"))
            {
                Vaste_Lasten.IsVisible = false;
                Vaste_Lasten.IsToggled = true;
                vastlstlbl.IsVisible = false;
                Top_lbl.FontSize = 15;
                Top_lbl.Text = "Voeg hier een maandelijkse uitgaven toe. Het is later mogelijk om meer (vaste) uitgaven toe te voegen in de App.";
                recurtype.IsVisible = true;
                recurtypelbl.IsVisible = true;
                plusbutton.IsVisible = false;
            }
            else
            {
                Ga_verder.IsVisible = false;
            }

            var profileTapRecognizer = new TapGestureRecognizer
            {
                TappedCallback = (v, o) => { Navigation.PushAsync(new Addcatagory()); },
                NumberOfTapsRequired = 1
            };
            plusbutton.GestureRecognizers.Add(profileTapRecognizer);
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



            var cats = await _connection.Table<SQL_Category>().Where(x => x.Income == false).ToListAsync();
            _Categories = new ObservableCollection<SQL_Category>(cats);
            foreach (var item in _Categories)
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
                

                var uitgaven = new SQL_Transacties();
                uitgaven.Date = DateTime.Now;
				uitgaven.Value = -double.Parse(Bedrag.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);            
                uitgaven.Category = Pick_cat.SelectedItem.ToString();
                uitgaven.Recurring = Vaste_Lasten.IsToggled;
                uitgaven.Recurtype = "";
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

                    // add database entry for budget samenstelling
                    uitgaven.Recurtype = recurtype.SelectedItem.ToString();
                    uitgaven.Name += " - " + recurtype.SelectedItem.ToString();
                    await _connection.InsertAsync(uitgaven);

                    //change variables for update budget record
                    uitgaven.Recurring = false;
                    uitgaven.Name = $"Update budget - {uitgaven.Category}";

                    int k = GetDaysInYear(DateTime.Now.Year);

                    if (recurtype.SelectedItem.ToString() == "Maandelijks")
                    {
                        
                        _budget += uitgaven.Value / s;
                        uitgaven.Value /= s;
                        foreach (var item in list_budget)
                        {
                            _budget += item.Value;
                        }
                        await _connection.InsertAsync(uitgaven);
                        await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", _budget, "Budget");
                    }

                    else if (recurtype.SelectedItem.ToString() == "Per kwartaal")
                    {
                        _budget += uitgaven.Value / (k / 4);
                        uitgaven.Value /= (k / 4);
                        foreach (var item in list_budget)
                        {
                            _budget += item.Value;
                        }

                        await _connection.InsertAsync(uitgaven);
                        await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", _budget, "Budget");
                    }
                    else if (recurtype.SelectedItem.ToString() == "Jaarlijks")
                    {
                        _budget += uitgaven.Value / k;
                        uitgaven.Value /= k;
                        foreach (var item in list_budget)
                        {
                            _budget += item.Value;
                        }

                        await _connection.InsertAsync(uitgaven);
                        await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", _budget, "Budget");
                    }

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
                if (App.Current.Properties.ContainsKey("savedPropA"))
                {
					Analytics.TrackEvent("Uitgaven Toegevoed");
                    await Navigation.PushAsync(new BudgetBuddyPage());
                    Navigation.RemovePage(this);
                }
                else
                {
                    App.Current.Properties.Add("savedPropA", "start");
                    App.Current.SavePropertiesAsync();
                    App.Current.MainPage = new MainPage();
                }
            }




        }

        private void Button_OnClicked2(object sender, EventArgs e)
        {
            App.Current.Properties.Add("savedPropA", "start");
            App.Current.SavePropertiesAsync();
            App.Current.MainPage = new MainPage();

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
                else if (entry == "-" || entry == "+")
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
        public static int GetDaysInYear(int year)
        {
            var thisYear = new DateTime(year, 1, 1);
            var nextYear = new DateTime(year + 1, 1, 1);

            return (nextYear - thisYear).Days;
        }

        private void Vaste_Lasten_OnToggled(object sender, ToggledEventArgs e)
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
    }
}