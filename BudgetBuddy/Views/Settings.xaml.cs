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
        string hex1 = "#303030";

		public Settings ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked1(object sender, EventArgs e)
        {
            App.Current.Resources["backgroundColor"] = Color.White;
            App.Current.Resources["textColor"] = Color.FromHex(hex1);
        }

        private void Button_Clicked2(object sender, EventArgs e)
        {
            App.Current.Resources["backgroundColor"] = Color.FromHex(hex1);
            App.Current.Resources["textColor"] = Color.White;
        }

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new Addcatagory());
        }
	}
}