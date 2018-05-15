﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SQLite;
using Xamarin.Forms;

namespace BudgetBuddy
{

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
            
       

            
			var settings = await _connection.Table<Settings>().OrderByDescending(x => x.Value).ToListAsync();
			_settings = new ObservableCollection<Settings>(settings);
			ListView.ItemsSource = _settings;

			base.OnAppearing();
        }

		void MyItemSelected(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var settings = e.Item as Settings;
            DisplayAlert("Alert", settings.Value, "OK");
        }

		//void MyItemSelected (object sender, System.EventArgs e)
		//{
		//	DisplayAlert("Alert", "You Selected Something!", "OK");
		//}


    }


}
