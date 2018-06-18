using System;
using System.Collections.Generic;
using System.Diagnostics;
using SQLite;
using Xamarin.Forms;
using BudgetBuddy.Properties;
using Microsoft.AppCenter.Analytics;

namespace BudgetBuddy.Views
{
    public partial class Spaardoelen_Toevoegen : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private double InputDay;
        private double _budget;

        public Spaardoelen_Toevoegen()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }


        private void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            UpdateCalculations();
        }

        private void UpdateCalculations()
        {
            try
            {
                DateTime daysLeft = DatePickerSpaardoel.Date;
                double goal = double.Parse(SpaardoelBedrag.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);


                //daysLeft.Subtract(DateTime.Today);
                //double days = Convert.ToDouble(daysLeft.Day.ToString(), System.Globalization.CultureInfo.InvariantCulture);

                double days = (daysLeft.Date - DateTime.Now.Date).TotalDays;


                DaysLeft.Text = "U heeft nog " + days.ToString() + " dagen om uw doel te bereiken.";

                //Calculate Daily Input
                InputDay = ((double)goal / (double)days);

                if (double.IsInfinity(InputDay))
                {
                    //DisplayAlert("Oneindigheid is maar een idee!", "Probeer je een paradox te creeren, stop hier mee!", "Ok, Sorry!");
                    throw new Exception("This is an INFINITE number!");
                }

                if (goal <= 0)
                {
                    DisplayAlert("",
                        "Voer geldig bedrag in!",
                        "OK");
                    SpaardoelBedrag.Text = "";
                    throw new Exception("This is invalid number!");
                }

                if (goal >= 10000000)
                {
                    DisplayAlert("",
                        "Het maximale bedrag is 9.999.999,99",
                        "OK");
                    SpaardoelBedrag.Text = "9999999.99";
                    throw new Exception("This is invalid number!");
                }

                if (Double.IsNaN(goal))
                {
                    throw new Exception("Invalid Number!");
                }

                EuroPerDag.Text = "U moet hiervoor dagelijks " + InputDay.ToString("0.00") + " Euro Inleggen.";


                SpaardoelenToevoegenButton.IsEnabled = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                SpaardoelenToevoegenButton.IsEnabled = false;
                //throw;
            }
        }

        private async void InsertTransaction()
        {
            var Transaction = new SQL_Transacties();
            Transaction.Date = DateTime.Now;
            Transaction.Value = -InputDay;
            Transaction.Category = "Inleg Spaardoel";
            Transaction.Name = "Inleg Spaardoel: " + SpaardoelNaam.Text;
            Transaction.Recurring = false;
            await _connection.InsertAsync(Transaction);


            //Fake it till you make it!
            var list_budget = await _connection.QueryAsync<SQL_Transacties>("SELECT * FROM SQL_Budget");

            _budget -= InputDay;
            foreach (var item in list_budget)
            {
                _budget += item.Value;
            }
            await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", _budget, "Budget");
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var spaarDoelen = new SQL_SpaarDoelen { }; //link with table
            spaarDoelen.Date = DateTime.Now;
            spaarDoelen.Value = Math.Round(-Convert.ToDouble(InputDay, System.Globalization.CultureInfo.InvariantCulture), 2);
            spaarDoelen.Name = SpaardoelNaam.Text;
            spaarDoelen.Goal = double.Parse(SpaardoelBedrag.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            spaarDoelen.Completed = false;
            spaarDoelen.Days = (DatePickerSpaardoel.Date - DateTime.Now.Date).TotalDays - 2;
            spaarDoelen.TotalDays = spaarDoelen.Days;
            spaarDoelen.ProgressBar = 0;
            spaarDoelen.Saved = -spaarDoelen.Value;
            await _connection.InsertAsync(spaarDoelen);
            InsertTransaction();

			Analytics.TrackEvent("Spaardoel Aangemaakt");
            await DisplayAlert("Gelukt", "Spaardoel succesvol toegevoegd", "OK");
            Navigation.RemovePage(this);
        }

        private void SpaardoelNaam_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCalculations();
        }

        private void SpaardoelBedrag_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCalculations();
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Information3());
        }
    }
}
