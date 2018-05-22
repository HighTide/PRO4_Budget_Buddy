﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Collections.ObjectModel;
using BudgetBuddy.Properties;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetBuddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Overzicht_Detail : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<SQL_Uitgaven> _uitgaven;
        private String category = "";

        public Overzicht_Detail(string data)
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            category = data;
        }
        protected override async void OnAppearing()
        {




            var uitgaven = await _connection.Table<SQL_Uitgaven>().Where(x => x.Category == category).ToListAsync();
            _uitgaven = new ObservableCollection<SQL_Uitgaven>(uitgaven);
            ListView.ItemsSource = _uitgaven;

            base.OnAppearing();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selected = e.SelectedItem as SQL_Uitgaven;
            DisplayAlert("Alert", selected.Name, "OK");
        }
    }
}