using BudgetBuddy.MenuItems;
using BudgetBuddy.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetBuddy
{
    public partial class MainPage : MasterDetailPage
    {

        string hex1 = "#303030";

        public List<MasterPageItem> MenuList
        {
            get;
            set;
        }
        public MainPage()
        {
            InitializeComponent();
            MenuList = new List<MasterPageItem>();
            // Adding menu items to menuList and you can define title, page and icon  
            MenuList.Add(new MasterPageItem()
            {
                Title = "Home",
                Icon = "Home.png",
                TargetType = typeof(BudgetBuddyPage)
            });
            MenuList.Add(new MasterPageItem()
            {
                Title = "Uitgaven toevoegen",
                Icon = "Uitgaven.png",
                TargetType = typeof(Uitgaven)
            });
            MenuList.Add(new MasterPageItem()
            {
                Title = "Inkomsten toevoegen",
                Icon = "Inkomsten.png",
                TargetType = typeof(Inkomsten)
            });
            MenuList.Add(new MasterPageItem()
            {
                Title = "Spaardoelen",
                Icon = "Piggy.png",
                TargetType = typeof(Spaardoelen)
            });
            MenuList.Add(new MasterPageItem()
            {
                Title = "Totaal overzicht",
                Icon = "Overzicht.png",
                TargetType = typeof(Overzicht)
            });
            MenuList.Add(new MasterPageItem()
            {
                Title = "Settings",
                Icon = "Settings.png",
                TargetType = typeof(Settings)
            });
            MenuList.Add(new MasterPageItem()
            {
                Title = "-----SQL-----",
                Icon = "Jurre.jpg",
                TargetType = typeof(SQL)
            });



            // Setting our list to be ItemSource for ListView in MainPage.xaml  
            navigationDrawerList.ItemsSource = MenuList;
            // Initial navigation, this can be used for our home page  
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(BudgetBuddyPage)))
            {
                BarBackgroundColor = Color.FromHex(hex1),
                BarTextColor = Color.White
            };
        }
        // Event for Menu Item selection, here we are going to handle navigation based  
        // on user selection in menu ListView  
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page))
            {
                BarBackgroundColor = Color.FromHex(hex1),
                BarTextColor = Color.White
            };
            IsPresented = false;
        }

    }
}