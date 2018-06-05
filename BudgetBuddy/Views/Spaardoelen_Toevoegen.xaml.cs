using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BudgetBuddy.Views
{
    public partial class Spaardoelen_Toevoegen : ContentPage
    {
        
        public Spaardoelen_Toevoegen()
        {
            InitializeComponent();

            //WHY U MERGE NO RIGHT VISUAL STUDIO?
        }


        private void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {


            DateTime daysLeft = DatePickerSpaardoel.Date;
            double goal = Convert.ToDouble(SpaardoelBedrag.Text, System.Globalization.CultureInfo.InvariantCulture);


            daysLeft.Subtract(DateTime.Today);
            double days = Convert.ToDouble(daysLeft.Day.ToString(), System.Globalization.CultureInfo.InvariantCulture);
            

            

            DaysLeft.Text = "U heeft nog " + daysLeft.Day.ToString() + " dagen om uw doel te bereiken.";

            //Calculate Daily Input
            double InputDay = ((double) goal / (double) days);

            EuroPerDag.Text = "U moet hiervoor dagelijks " + InputDay.ToString("0.00") + " Euro Inleggen.";



        }
    }
}
