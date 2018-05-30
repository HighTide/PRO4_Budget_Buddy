using System;
using System.Collections.Generic;
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
        private SQLiteAsyncConnection _connection;
        private double _Bedrag;

        public Inkomsten()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        void Entry_Completed(object sender, EventArgs e)
        {
            var text = ((Entry)sender).Text; //cast sender to access the properties of the Entry
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
            else
            {
                var inkomsten = new SQL_Inkomsten { }; //link with table
                inkomsten.Date = DateTime.Now;
                inkomsten.Value = Convert.ToDouble(Bedrag.Text, System.Globalization.CultureInfo.InvariantCulture);
                inkomsten.Category = Category.SelectedItem.ToString();
                await _connection.InsertAsync(inkomsten);
                await DisplayAlert("Alert", "Inkomsten succesvol toegevoegd", "OK");
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

                else if (entry != "" && Bedrag.Text.IndexOf('.') == 0 || Bedrag.Text.IndexOf(',') == 0)
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