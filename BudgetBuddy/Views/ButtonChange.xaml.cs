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
    public partial class ButtonChange : ContentPage
    {

        private SQLiteAsyncConnection _connection;

        public ButtonChange()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            InitializeComponent();
        }

        private async void Button_OnClicked1(object sender, EventArgs e)
        {
            if (knop1.SelectedItem == "Uitgaven")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Uitgaven", "Button1");
            }
            if (knop1.SelectedItem == "Inkomsten")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Inkomsten", "Button1");
            }
            if (knop1.SelectedItem == "Spaardoelen")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Spaardoelen", "Button1");
            }
            if (knop1.SelectedItem == "Spaardoel toevoegen")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Spaardoel toevoegen", "Button1");
            }
            if (knop1.SelectedItem == "Overzicht")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Overzicht", "Button1");
            }
            if (knop1.SelectedItem == "Totaal Overzicht")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Totaal Overzicht", "Button1");
            }
            if (knop1.SelectedItem == "Budget")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Budget", "Button1");
            }
            if (knop1.SelectedItem == "Settings")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Settings", "Button1");
            }
            await DisplayAlert("Gelukt", "Knop verandert naar "+ knop1.SelectedItem, "OK");
            //Navigation.PushAsync(new ButtonChange1());
        }
        private async void Button_OnClicked2(object sender, EventArgs e)
        {
            if (knop2.SelectedItem == "Uitgaven")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Uitgaven", "Button2");
            }
            if (knop2.SelectedItem == "Inkomsten")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Inkomsten", "Button2");
            }
            if (knop2.SelectedItem == "Spaardoelen")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Spaardoelen", "Button2");
            }
            if (knop2.SelectedItem == "Spaardoel toevoegen")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Spaardoel toevoegen", "Button2");
            }
            if (knop2.SelectedItem == "Overzicht")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Overzicht", "Button2");
            }
            if (knop2.SelectedItem == "Totaal Overzicht")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Totaal Overzicht", "Button2");
            }
            if (knop2.SelectedItem == "Budget")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Budget", "Button2");
            }
            if (knop2.SelectedItem == "Settings")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Settings", "Button2");
            }
            await DisplayAlert("Gelukt", "Knop verandert naar " + knop2.SelectedItem, "OK");
            //Navigation.PushAsync(new ButtonChange2());
        }
        private async void Button_OnClicked3(object sender, EventArgs e)
        {
            if (knop3.SelectedItem == "Uitgaven")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Uitgaven", "Button3");
            }
            if (knop3.SelectedItem == "Inkomsten")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Inkomsten", "Button3");
            }
            if (knop3.SelectedItem == "Spaardoelen")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Spaardoelen", "Button3");
            }
            if (knop3.SelectedItem == "Spaardoel toevoegen")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Spaardoel toevoegen", "Button3");
            }
            if (knop3.SelectedItem == "Overzicht")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Overzicht", "Button3");
            }
            if (knop3.SelectedItem == "Totaal Overzicht")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Totaal Overzicht", "Button3");
            }
            if (knop3.SelectedItem == "Budget")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Budget", "Button3");
            }
            if (knop3.SelectedItem == "Settings")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Settings", "Button3");
            }
            await DisplayAlert("Gelukt", "Knop verandert naar " + knop3.SelectedItem, "OK");
            //Navigation.PushAsync(new ButtonChange3());
        }
        private async void Button_OnClicked4(object sender, EventArgs e)
        {
            if (knop4.SelectedItem == "Uitgaven")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Uitgaven", "Button4");
            }
            if (knop4.SelectedItem == "Inkomsten")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Inkomsten", "Button4");
            }
            if (knop4.SelectedItem == "Spaardoelen")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Spaardoelen", "Button4");
            }
            if (knop4.SelectedItem == "Spaardoel toevoegen")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Spaardoel toevoegen", "Button4");
            }
            if (knop4.SelectedItem == "Overzicht")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Overzicht", "Button4");
            }
            if (knop4.SelectedItem == "Totaal Overzicht")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Totaal Overzicht", "Button4");
            }
            if (knop4.SelectedItem == "Budget")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Budget", "Button4");
            }
            if (knop4.SelectedItem == "Settings")
            {
                await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Settings", "Button4");
            }
            await DisplayAlert("Gelukt", "Knop verandert naar " + knop4.SelectedItem, "OK");
            //Navigation.PushAsync(new ButtonChange4());
        }
    }
}