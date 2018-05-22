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
                Title = "Home",
                Icon = "Home.png",
                TargetType = typeof(BudgetBuddyPage)
            });
            MenuList.Add(new Category()
            {
                Title = "Uitgaven toevoegen",
                Icon = "Uitgaven.png",
                TargetType = typeof(Uitgaven)
            });
            MenuList.Add(new Category()
            {
                Title = "Inkomsten toevoegen",
                Icon = "Inkomsten.png",
                TargetType = typeof(Inkomsten)
            });
            MenuList.Add(new Category()
            {
                Title = "Spaardoelen",
                Icon = "Piggy.png",
                TargetType = typeof(Spaardoelen)
            });
            MenuList.Add(new Category()
            {
                Title = "Totaal overzicht",
                Icon = "Overzicht.png",
                TargetType = typeof(Overzicht)
            });
            MenuList.Add(new Category()
            {
                Title = "Settings",
                Icon = "Settings.png",
                TargetType = typeof(Settings)
            });
            MenuList.Add(new Category()
            {
                Title = "-----SQL-----",
                Icon = "Jurre.jpg",
                TargetType = typeof(Settings),
            });


            // Setting our list to be ItemSource for ListView in MainPage.xaml  
            Categories.ItemsSource = MenuList;
            // Initial navigation, this can be used for our home page  
        }
        // Event for Menu Item selection, here we are going to handle navigation based  
        // on user selection in menu ListView  


	    private void Categories_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        
	    }
	}
}