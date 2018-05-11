using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLite;
using Xamarin.Forms;

namespace BudgetBuddy
{
	public class Settings
    {
        [PrimaryKey]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Value { get; set; }
    }

    public partial class SQL : ContentPage
    {
		private SQLiteAsyncConnection _connection;
		private ObservableCollection<Settings> _settings;

        public SQL()
        {
            InitializeComponent();
           

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

		protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Settings>();

            var settings_name = new Settings { Name = "Name", Value = "Jurre" };
            await _connection.InsertAsync(settings_name);


            var settings = await _connection.Table<Settings>().ToListAsync();
			_settings = new ObservableCollection<Settings>(settings);
			ListView.ItemsSource = _settings;

			base.OnAppearing();
        }
    }
}
