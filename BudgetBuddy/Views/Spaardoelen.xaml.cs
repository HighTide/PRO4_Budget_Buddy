using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using BudgetBuddy.Properties;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace BudgetBuddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Spaardoelen : ContentPage
	{
	    private SQLiteAsyncConnection _connection;
	    private ObservableCollection<SQL_SpaarDoelen> _sqlSpaarDoelen;

        public Spaardoelen ()
		{
			InitializeComponent ();

		    _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

		private void Handle_Clicked(object sender, System.EventArgs e)
		{
			Navigation.PushAsync(new Spaardoelen_Toevoegen());
		}

	    protected override async void OnAppearing()
	    {

	        var spaardoelen = await _connection.Table<SQL_SpaarDoelen>().ToListAsync();
	        _sqlSpaarDoelen = new ObservableCollection<SQL_SpaarDoelen>(spaardoelen);
	        ListView.ItemsSource = _sqlSpaarDoelen;

	        base.OnAppearing();
	    }
	    async void MenuItem_Clicked(object sender, System.EventArgs e)
	    {
	        int s = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

	        var mi = ((MenuItem)sender);
	        var viewCellSelected = sender as MenuItem;
	        var calculationToDelete = viewCellSelected?.BindingContext as SQL_SpaarDoelen;

            await _connection.ExecuteAsync("Update SQL_Budget SET Value = Value + ? Where Name = ?", calculationToDelete.Saved, "Budget");

	        

	        var Transaction = new SQL_Transacties();
	        Transaction.Date = DateTime.Now;
	        Transaction.Value = calculationToDelete.Saved;
	        Transaction.Category = "Spaardoel";
	        Transaction.Name = "Opnamen Spaardoel: " + calculationToDelete.Name;
	        Transaction.Recurring = false;
	        await _connection.InsertAsync(Transaction);


	        await _connection.DeleteAsync(calculationToDelete);

	        var spaardoelen = await _connection.Table<SQL_SpaarDoelen>().ToListAsync();
	        _sqlSpaarDoelen = new ObservableCollection<SQL_SpaarDoelen>(spaardoelen);
	        ListView.ItemsSource = _sqlSpaarDoelen;

        }
    }


}