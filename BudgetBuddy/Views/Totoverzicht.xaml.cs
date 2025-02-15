﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetBuddy.Properties;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetBuddy.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Totoverzicht : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        List<double> results = new List<double>();
        private double totalis;
        private double totalis2;
        public Totoverzicht()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            var Tots = await _connection.QueryAsync<SQL_Transacties>("SELECT * FROM SQL_Transacties WHERE NOT Recurring ORDER BY Date DESC");

            Total.ItemsSource = Tots;

            foreach (var item in Tots)
            {

                totalis += item.Value;
            }
            if (totalis > 0)
            {
                Totals.TextColor = Color.LawnGreen;
            }
            else
            {
                Totals.TextColor = Color.Red;
            }
            Totals.Text = "€ " + totalis.ToString("0.00");

        }

        async void MenuItem_Clicked(object sender, System.EventArgs e)
        {
            int s = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            totalis2 = 0.00;
            var mi = ((MenuItem)sender);
            var viewCellSelected = sender as MenuItem;
            var calculationToDelete = viewCellSelected?.BindingContext as SQL_Transacties;
            if (calculationToDelete.Value > 0)
                await _connection.ExecuteAsync("Update SQL_Budget SET Value = Value - ? Where Name = ?", calculationToDelete.Value, "Budget");
            else if (calculationToDelete.Value < 0)
                await _connection.ExecuteAsync("Update SQL_Budget SET Value = Value + ? Where Name = ?", Math.Abs(calculationToDelete.Value), "Budget");
            await _connection.DeleteAsync(calculationToDelete);
            var Tots = await _connection.QueryAsync<SQL_Transacties>("SELECT * FROM SQL_Transacties WHERE NOT Recurring ORDER BY Date DESC");
            Total.ItemsSource = Tots;
            foreach (var item in Tots)
            {
                totalis2 += item.Value;
            }
            if (totalis2 >= 0)
            {
                Totals.TextColor = Color.LawnGreen;
            }
            else
            {
                Totals.TextColor = Color.Red;
            }
            Totals.Text = "€ " + totalis2.ToString("0.00");
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
                Navigation.PushAsync(new Information4());
        }
    }
}