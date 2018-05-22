using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetBuddy.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Overzicht : ContentPage
	{
        public List<Category> MenuList
        {
            get;
            set;
        }
        public Overzicht()
        {
            InitializeComponent();
            MenuList = new List<Category>();
            // Adding menu items to menuList and you can define title ,page and icon  
            MenuList.Add(new Category()
            {
                Title = "Kleding",
            });
            MenuList.Add(new Category()
            {
                Title = "Drinken",
            });
            MenuList.Add(new Category()
            {
                Title = "Eten",
            });
            MenuList.Add(new Category()
            {
                Title = "Boodschappen",
            });
            MenuList.Add(new Category()
            {
                Title = "Amusement",
            });
            MenuList.Add(new Category()
            {
                Title = "Abonnementen",
            });


            // Setting our list to be ItemSource for ListView in MainPage.xaml  
            Categories.ItemsSource = MenuList;
            // Initial navigation, this can be used for our home page  
        }
        // Event for Menu Item selection, here we are going to handle navigation based  
        // on user selection in menu ListView  


	    private async void Categories_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
            var selected = e.SelectedItem as Category;
            await Navigation.PushAsync(new Overzicht_Detail(selected.Title.ToString()));
        }
	}
}