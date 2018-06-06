﻿using Xamarin.Forms;
using BudgetBuddy.Views;
using SQLite;
using BudgetBuddy.Properties;
using System;
using System.Collections.ObjectModel;

namespace BudgetBuddy
{
    public partial class BudgetBuddyPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        public string Button1Val = "Inkomsten";
        public string Button2Val = "Uitgaven";
        public string Button3Val = "Spaardoelen";
        public string Button4Val = "Overzicht";
        private ObservableCollection<SQL_Uitgaven> _uitgaven_maand_filter;
        private ObservableCollection<SQL_Uitgaven> _laatste_uitgave_filter;
        private DateTime _datum = DateTime.UtcNow.AddDays(-4);

        public BudgetBuddyPage()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            InitializeComponent();

        }

        private void Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }

        protected override async void OnAppearing()
        {
            // int month = System.DateTime.Now.Month;
            // int year = System.DateTime.Now.Year;
            // int days_this_month = System.DateTime.DaysInMonth(year, month);
            // int days_left = days_this_month - System.DateTime.Now.Day;

            // int centuryBegin = System.DateTime(2001, 1, 1);
            // int monthBegin = System.DateTime(year, month, 1);
            // int monthEnd = System.DateTime(year, month, days_this_month);

            //var spending = await _connection.Table<SQL_Uitgaven>().OrderByDescending(x => x.Value).Where(
            //    SQL_Uitgaven.Date < new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month + 1, 1).Ticks and
            //    SQL_Uitgaven.Date > new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month - 1, System.DateTime.DaysInMonth(System.DateTime.Now.Month - 1)).Ticks;

            var laatste_uitgave = await _connection.QueryAsync<SQL_Uitgaven>("SELECT * FROM SQL_Uitgaven ORDER BY Date DESC LIMIT 1");
            _laatste_uitgave_filter = new ObservableCollection<SQL_Uitgaven>(laatste_uitgave);
            //uitgaveView.ItemSource = _laatste_uitgave_filter;

            var uitgaven_maand = await _connection.QueryAsync<SQL_Uitgaven>("SELECT SUM(Value) FROM SQL_Uitgaven WHERE Date <= ? AND Date >= ? LIMIT 1",
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Ticks, new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                             DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).Ticks);

            var uitgaven = await _connection.Table<SQL_Uitgaven>().Where(x => x.Date > _datum).ToListAsync();
            uitgaveView.ItemsSource = _laatste_uitgave_filter;

            // var inkomen_maand = await _connection.QueryAsync<SQL_Inkomsten>("SELECT SUM(Value) FROM SQL_Inkomsten WHERE Date <= ? AND Date >= ? LIMIT 1",
            //     new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Ticks, new DateTime(DateTime.Now.Year, DateTime.Now.Month,
            //                  DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).Ticks);

            //var bedrag_per_dag = (inkomen_maand - uitgaven_maand) / (System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month)
            //                     - System.DateTime.Now.Day);

            //_uitgaven_maand_filter = new ObservableCollection<SQL_Uitgaven>(uitgaven_maand);
            //dataView.ItemsSource = _uitgaven_maand_filter; 


            var buttons = await _connection.Table<SQL_Buttons>().ToListAsync();
            foreach (var item in buttons)
            {
                if (item.Name == "Button1")
                {
                    Button1Val = item.Value;
                }

                if (item.Name == "Button2")
                {
                    Button2Val = item.Value;
                }

                if (item.Name == "Button3")
                {
                    Button3Val = item.Value;
                }

                if (item.Name == "Button4")
                {
                    Button4Val = item.Value;
                }
            }
            Button1.Text = Button1Val;
            Button2.Text = Button2Val;
            Button3.Text = Button3Val;
            Button4.Text = Button4Val;
            base.OnAppearing();
        }


        void Text_Click(object sender, System.EventArgs e)
        {
            DisplayAlert("Budget Buddy", "Test", "OK");

        }



        async void Button_Pressed1(object sender, System.EventArgs e)
        {
            if (Button1.Text == "Uitgaven")
            {
                await Navigation.PushAsync(new Uitgaven());
            }
            if (Button1.Text == "Inkomsten")
            {
                await Navigation.PushAsync(new Inkomsten());
            }
            if (Button1.Text == "Spaardoelen")
            {
                await Navigation.PushAsync(new Spaardoelen());
            }
            if (Button1.Text == "Overzicht")
            {
                await Navigation.PushAsync(new Overzicht());
            }
            if (Button1.Text == "Totaal Overzicht")
            {
                await Navigation.PushAsync(new Totoverzicht());
            }
            if (Button1.Text == "Settings")
            {
                await Navigation.PushAsync(new Settings());
            }
        }

        async void Button_Pressed2(object sender, System.EventArgs e)
        {
            if (Button2.Text == "Uitgaven")
            {
                await Navigation.PushAsync(new Uitgaven());
            }
            if (Button2.Text == "Inkomsten")
            {
                await Navigation.PushAsync(new Inkomsten());
            }
            if (Button2.Text == "Spaardoelen")
            {
                await Navigation.PushAsync(new Spaardoelen());
            }
            if (Button2.Text == "Overzicht")
            {
                await Navigation.PushAsync(new Overzicht());
            }
            if (Button2.Text == "Totaal Overzicht")
            {
                await Navigation.PushAsync(new Totoverzicht());
            }
            if (Button2.Text == "Settings")
            {
                await Navigation.PushAsync(new Settings());
            }
        }

        async void Button_Pressed3(object sender, System.EventArgs e)
        {
            if (Button3.Text == "Uitgaven")
            {
                await Navigation.PushAsync(new Uitgaven());
            }
            if (Button3.Text == "Inkomsten")
            {
                await Navigation.PushAsync(new Inkomsten());
            }
            if (Button3.Text == "Spaardoelen")
            {
                await Navigation.PushAsync(new Spaardoelen());
            }
            if (Button3.Text == "Overzicht")
            {
                await Navigation.PushAsync(new Overzicht());
            }
            if (Button3.Text == "Totaal Overzicht")
            {
                await Navigation.PushAsync(new Totoverzicht());
            }
            if (Button3.Text == "Settings")
            {
                await Navigation.PushAsync(new Settings());
            }
        }

        async void Button_Pressed4(object sender, System.EventArgs e)
        {
            if (Button4.Text == "Uitgaven")
            {
                await Navigation.PushAsync(new Uitgaven());
            }
            if (Button4.Text == "Inkomsten")
            {
                await Navigation.PushAsync(new Inkomsten());
            }
            if (Button4.Text == "Spaardoelen")
            {
                await Navigation.PushAsync(new Spaardoelen());
            }
            if (Button4.Text == "Overzicht")
            {
                await Navigation.PushAsync(new Overzicht());
            }
            if (Button4.Text == "Totaal Overzicht")
            {
                await Navigation.PushAsync(new Totoverzicht());
            }
            if (Button4.Text == "Settings")
            {
                await Navigation.PushAsync(new Settings());
            }
        }
    }
}