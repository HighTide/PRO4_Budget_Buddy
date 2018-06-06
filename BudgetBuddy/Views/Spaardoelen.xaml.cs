﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using BudgetBuddy.Properties;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace BudgetBuddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Spaardoelen : ContentPage
	{
	    private SQLiteAsyncConnection _connection;
	    private ObservableCollection<SQL_SpaarDoelen> _settings;

        public Spaardoelen ()
		{
			InitializeComponent ();

		    _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

		private void Handle_Clicked(object sender, System.EventArgs e)
		{
			Navigation.PushAsync(new Spaardoelen_Toevoegen());
		}

	    protected override async void OnAppearing()
	    {

	        var settings = await _connection.Table<SQL_SpaarDoelen>().ToListAsync();
	        _settings = new ObservableCollection<SQL_SpaarDoelen>(settings);
	        ListView.ItemsSource = _settings;

	        base.OnAppearing();
	    }
    }


}