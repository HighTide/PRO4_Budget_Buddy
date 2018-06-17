using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using BudgetBuddy.Properties;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetBuddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Overzicht : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<SQL_Category> _Categories;
        private ObservableCollection<SQL_Category> _Categories2;
        public List<Category> MenuList { get; set; }

        public Overzicht()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            var settings = await _connection.Table<SQL_Category>().Where(x => x.Income == false).ToListAsync();
            _Categories = new ObservableCollection<SQL_Category>(settings);
            Categories.ItemsSource = _Categories;

            var _settings = await _connection.Table<SQL_Category>().Where(x => x.Income == true).ToListAsync();
            _Categories2 = new ObservableCollection<SQL_Category>(_settings);
            Categories2.ItemsSource = _Categories2;
            base.OnAppearing();
        }
        // Event for Menu Item selection, here we are going to handle navigation based  
        // on user selection in menu ListView  


        private async void Categories_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selected = e.SelectedItem as SQL_Category;
            await Navigation.PushAsync(new Overzicht_Detail(selected.Name.ToString()));
        }

        async void MenuItem_Clicked(object sender, System.EventArgs e)
        {
            var mi = ((MenuItem) sender);
            var viewCellSelected = sender as MenuItem;
            var calculationToDelete = viewCellSelected?.BindingContext;

            await _connection.DeleteAsync(calculationToDelete);

            var settings = await _connection.Table<SQL_Category>().Where(x => x.Income == false).ToListAsync();
            _Categories = new ObservableCollection<SQL_Category>(settings);
            Categories.ItemsSource = _Categories;

            var _settings = await _connection.Table<SQL_Category>().Where(x => x.Income == true).ToListAsync();
            _Categories2 = new ObservableCollection<SQL_Category>(_settings);
            Categories2.ItemsSource = _Categories2;
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            DisplayAlert("",
                "Hier vind je een overzicht van alle categorieen, door op een categorie te drukken kan je alle uitgaven of inkomsten die je in die categorie hebt zien.\n\nHet is ook mogelijk een categorie te verwijderen door er 1 seconde met je vinger op te drukken, en dan op delete te drukken",
                "OK");

        }

        private void MenuItem_OnClicked1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Addcatagory());
        }
    }
}