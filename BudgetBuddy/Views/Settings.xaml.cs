using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetBuddy.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public Settings ()
		{
			InitializeComponent ();
		}

        private void doneButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.Resources["backgroundColor"] = Color.FromHex(backgroundColorEntry.Text);
            App.Current.Resources["textColor"] = Color.FromHex(textColorEntry.Text);
        }
    }
}