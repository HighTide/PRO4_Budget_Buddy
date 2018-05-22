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
        
        public Uitgaven ()
		{
			InitializeComponent ();

		    _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

	    private async void Button_OnClicked(object sender, EventArgs e)
	    {

	        var uitgaven = new SQL_Uitgaven { };
	        uitgaven.Date = DateTime.Now;
	        uitgaven.Value = Convert.ToDouble(Bedrag.Text, System.Globalization.CultureInfo.InvariantCulture);
            uitgaven.Category = Category.SelectedItem.ToString();
	        uitgaven.Name = Naam.Text;
	        await _connection.InsertAsync(uitgaven);
	        await DisplayAlert("Alert", "Uitgaven succesvol toegevoegd", "OK");
	        await Navigation.PushAsync(new BudgetBuddyPage());
            Navigation.RemovePage(this);

	    }
	}
}