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

            daysLeft.Subtract(DateTime.Today);

            DaysLeft.Text = "U heeft nog: " + daysLeft.Day.ToString() + " dagen om u doel te bereiken.";



        }
    }
}
