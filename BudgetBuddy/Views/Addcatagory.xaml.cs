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
                catList.Add(item.Name.ToLower());
            }
	        if (string.IsNullOrWhiteSpace(Naam.Text))
	        {
	            await DisplayAlert("Alert", "Categorie heeft geen naam", "OK");
	        }
	        else if (Category.SelectedItem == null)
	        {
	            await DisplayAlert("Alert", "Kies een Type categorie", "OK");
	        }
            else if (catList.Contains(Naam.Text.ToLower()))
            {
                await DisplayAlert("Alert", "Categorie bestaat al", "OK");
            }

            
            else
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
                await DisplayAlert("Gelukt", "Categorie succesvol toegevoegd", "OK");
                Navigation.RemovePage(this);
            }
            
	        
        }
	}
}