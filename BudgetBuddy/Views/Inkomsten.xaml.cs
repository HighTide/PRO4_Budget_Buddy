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
            var inkomsten = new SQL_Inkomsten { }; //link with table
            inkomsten.Date = DateTime.Now;
            inkomsten.Value = Convert.ToDouble(Bedrag.Text, System.Globalization.CultureInfo.InvariantCulture);
            inkomsten.Category = Category.SelectedItem.ToString();
            await _connection.InsertAsync(inkomsten);             await DisplayAlert("Alert", "Inkomsten succesvol toegevoegd", "OK");             await Navigation.PushAsync(new BudgetBuddyPage());             Navigation.RemovePage(this);
        }
    }
}