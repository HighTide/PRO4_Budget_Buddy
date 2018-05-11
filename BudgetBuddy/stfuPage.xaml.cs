using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SQLite;

namespace BudgetBuddy
{
    public partial class stfuPage : ContentPage
    {
        public stfuPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new SQL());
		}
    }
}
