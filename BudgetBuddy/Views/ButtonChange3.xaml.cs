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
	public partial class ButtonChange3 : ContentPage
	{
        private SQLiteAsyncConnection _connection;

        public ButtonChange3 ()
		{
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            InitializeComponent ();
		}
        private async void Button_OnClicked1(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Inkomsten", "Button3");
        }
        private async void Button_OnClicked2(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Uitgaven", "Button3");
        }
        private async void Button_OnClicked3(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Spaardoelen", "Button3");
        }
        private async void Button_OnClicked4(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Overzicht", "Button3");
        }
        private async void Button_OnClicked5(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Totaal Overzicht", "Button3");
        }
        private async void Button_OnClicked6(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("Update SQL_Buttons SET Value = ? Where Name = ?", "Settings", "Button3");
        }
    }
}