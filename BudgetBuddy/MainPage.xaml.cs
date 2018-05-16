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
        public List<MasterPageItem> MenuList
        {
            get;
            set;
        }
        public MainPage()
        {
            InitializeComponent();
            MenuList = new List<MasterPageItem>();
            // Adding menu items to menuList and you can define title ,page and icon  
            MenuList.Add(new MasterPageItem()
            {
                Title = "Home",
                Icon = "homeicon.png",
                TargetType = typeof(BudgetBuddyPage)
            });
            MenuList.Add(new MasterPageItem()
            {
                Title = "Uitgaven toevoegen",
                Icon = "contacticon.png",
                TargetType = typeof(Uitgaven)
            });
            MenuList.Add(new MasterPageItem()
            {
                Title = "Spaardoelen",
                Icon = "contacticon.png",
                TargetType = typeof(Spaardoelen)
            });
			MenuList.Add(new MasterPageItem()
            {
                Title = "-----SQL-----",
                Icon = "contacticon.png",
                TargetType = typeof(SQL)
            });


            // Setting our list to be ItemSource for ListView in MainPage.xaml  
            navigationDrawerList.ItemsSource = MenuList;
            // Initial navigation, this can be used for our home page  
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(BudgetBuddyPage)));
        }
        // Event for Menu Item selection, here we are going to handle navigation based  
        // on user selection in menu ListView  
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}