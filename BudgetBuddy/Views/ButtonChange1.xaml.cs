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
	public partial class ButtonChange1 : ContentPage
	{

        private SQLiteAsyncConnection _connection;
        public ButtonChange1 ()
		{
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            InitializeComponent ();
		}

        private async void Button_OnClicked1(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Inkomsten", "Button1");
            await DisplayAlert("Gelukt", "Knop verandert naar inkomsten", "OK");
            Navigation.RemovePage(this);
        }
        private async void Button_OnClicked2(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Uitgaven", "Button1");
            await DisplayAlert("Gelukt", "Knop verandert naar uitgaven", "OK");
            Navigation.RemovePage(this);
        }
        private async void Button_OnClicked3(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Spaardoelen", "Button1");
            await DisplayAlert("Gelukt", "Knop verandert naar spaardoelen", "OK");
            Navigation.RemovePage(this);
        }
        private async void Button_OnClicked4(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Overzicht", "Button1");
            await DisplayAlert("Gelukt", "Knop verandert naar overzicht", "OK");
            Navigation.RemovePage(this);
        }
        private async void Button_OnClicked5(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Totaal Overzicht", "Button1");
            await DisplayAlert("Gelukt", "Knop verandert naar totaal overzicht", "OK");
            Navigation.RemovePage(this);
        }
        private async void Button_OnClicked6(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Settings", "Button1");
            await DisplayAlert("Gelukt", "Knop verandert naar settings", "OK");
            Navigation.RemovePage(this);
        }
    }
}