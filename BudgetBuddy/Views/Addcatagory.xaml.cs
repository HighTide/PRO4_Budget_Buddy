using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using BudgetBuddy.Properties;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetBuddy.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Addcatagory : ContentPage
	{
	    private SQLiteAsyncConnection _connection;
        public List<string> catList = new List<string>();
        public Addcatagory ()
		{
			InitializeComponent ();
		    _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

	    private async void Button_OnClicked(object sender, EventArgs e)
	    {
            var cats = await _connection.QueryAsync<SQL_Category>("SELECT Name FROM SQL_Category");
            foreach( var item in cats)
            {
                catList.Add(item.Name);
            }
            if(!catList.Contains(Naam.Text))
            {
                var category = new SQL_Category { };
                category.Name = Naam.Text;
                if (Category.SelectedIndex == 0)
                {
                    category.Income = false;
                }
                else
                {
                    category.Income = true;
                }
                await _connection.InsertAsync(category);
                await DisplayAlert("Alert", "categorie succesvol toegevoegd", "OK");
                Navigation.RemovePage(this);
            }
            else
            {
                await DisplayAlert("Error", "Categorie bestaat al", "Ok");
            }
            
	        
        }
	}
}