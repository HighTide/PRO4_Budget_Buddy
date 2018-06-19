using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetBuddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reset_page : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        public Reset_page()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await _connection.ExecuteAsync("DELETE FROM SQL_SpaarDoelen");
            await _connection.ExecuteAsync("DELETE FROM SQL_Transacties");
            await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", 0.00, "Budget");
            await DisplayAlert("", "Alle gegevens succesvol verwijderd", "OK");
            Navigation.RemovePage(this);
        }
    }
}