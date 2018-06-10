using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;
using BudgetBuddy.Properties;

namespace BudgetBuddy.Views
{
    public partial class Spaardoelen_Toevoegen : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private double InputDay;

        public Spaardoelen_Toevoegen()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            //WHY U MERGE NO RIGHT VISUAL STUDIO?
        }


        private void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {


            DateTime daysLeft = DatePickerSpaardoel.Date;
            double goal = Convert.ToDouble(SpaardoelBedrag.Text, System.Globalization.CultureInfo.InvariantCulture);


			//daysLeft.Subtract(DateTime.Today);
			//double days = Convert.ToDouble(daysLeft.Day.ToString(), System.Globalization.CultureInfo.InvariantCulture);
            
			double days = (daysLeft.Date - DateTime.Now.Date).TotalDays;
            

            DaysLeft.Text = "U heeft nog " + days.ToString() + " dagen om uw doel te bereiken.";

            //Calculate Daily Input
            InputDay = ((double) goal / (double) days);

            EuroPerDag.Text = "U moet hiervoor dagelijks " + InputDay.ToString("0.00") + " Euro Inleggen.";



        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var spaarDoelen = new SQL_SpaarDoelen { }; //link with table
            spaarDoelen.Date = DateTime.Now;
            spaarDoelen.Value = Convert.ToDouble(InputDay, System.Globalization.CultureInfo.InvariantCulture);
            spaarDoelen.Name = SpaardoelNaam.Text;
            spaarDoelen.Goal = Convert.ToDouble(SpaardoelBedrag.Text, System.Globalization.CultureInfo.InvariantCulture);
            await _connection.InsertAsync(spaarDoelen);

            await DisplayAlert("Gelukt", "Spaardoel succesvol toegevoegd", "OK");
            await Navigation.PushAsync(new BudgetBuddyPage());
            Navigation.RemovePage(this);
        }
    }
}
