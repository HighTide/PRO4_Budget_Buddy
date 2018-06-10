using System;
using System.Collections.Generic;
using System.Diagnostics;
using SQLite;
using Xamarin.Forms;
using BudgetBuddy.Properties;

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
            

            //WHY U MERGE NO RIGHT VISUAL STUDIO?
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
            await _connection.InsertAsync(Transaction);


            //Doing Lame shit because they did not make function
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
            spaarDoelen.Value = Convert.ToDouble(InputDay, System.Globalization.CultureInfo.InvariantCulture);
            spaarDoelen.Name = SpaardoelNaam.Text;
            spaarDoelen.Goal = double.Parse(SpaardoelBedrag.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            spaarDoelen.Completed = false;
            spaarDoelen.Days = (DatePickerSpaardoel.Date - DateTime.Now.Date).TotalDays;
            await _connection.InsertAsync(spaarDoelen);
            InsertTransaction();
            await DisplayAlert("Alert", "Spaardoel succesvol toegevoegd", "OK");

            await DisplayAlert("Gelukt", "Spaardoel succesvol toegevoegd", "OK");
            await Navigation.PushAsync(new BudgetBuddyPage());
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
    }
}
