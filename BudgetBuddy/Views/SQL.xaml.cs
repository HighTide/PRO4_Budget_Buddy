using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BudgetBuddy.Properties;
using SQLite;
using Xamarin.Forms;

namespace BudgetBuddy
{

    public partial class SQL : ContentPage
    {
		private SQLiteAsyncConnection _connection;
		private ObservableCollection<SQL_SpaarDoelen> _settings;

        public SQL()
        {
            InitializeComponent();           

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

		protected override async void OnAppearing()
        {
            
       

            
			var settings = await _connection.Table<SQL_SpaarDoelen>().OrderByDescending(x => x.Value).ToListAsync();
			_settings = new ObservableCollection<SQL_SpaarDoelen>(settings);
			ListView.ItemsSource = _settings;

			base.OnAppearing();
        }

		private void MyItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selected = e.SelectedItem as SQL_SpaarDoelen;
            DisplayAlert("Name", selected.Name.ToString(), "OK");
            DisplayAlert("Value", selected.Value.ToString(), "OK");
            DisplayAlert("Goal", selected.Goal.ToString(), "OK");

        }

		//void MyItemSelected (object sender, System.EventArgs e)
		//{
		//	DisplayAlert("Alert", "You Selected Something!", "OK");
		//}


    }


}
