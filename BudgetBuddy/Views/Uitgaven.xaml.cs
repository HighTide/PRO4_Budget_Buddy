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

        public Uitgaven()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            _Bedrag = Convert.ToDouble(Bedrag.Text, System.Globalization.CultureInfo.InvariantCulture);
            if (Category.SelectedItem == null)
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
                var uitgaven = new SQL_Uitgaven { };
                uitgaven.Date = DateTime.Now;
                uitgaven.Value = Convert.ToDouble(Bedrag.Text, System.Globalization.CultureInfo.InvariantCulture);
                uitgaven.Category = Category.SelectedItem.ToString();
                if (Naam.Text == null)
                {
                    uitgaven.Name = Category.SelectedItem.ToString();
                }
                else
                {
                    uitgaven.Name = Naam.Text;
                }
                await _connection.InsertAsync(uitgaven);
                await DisplayAlert("Alert", "Uitgaven succesvol toegevoegd", "OK");
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