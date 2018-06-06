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
	public partial class HelpPage : ContentPage
	{
		public HelpPage ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Information1());
        }

        private void Button_Clicked2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Information2());
        }

    }
}