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
	public partial class Information1 : ContentPage
	{
		public Information1 ()
		{
			InitializeComponent ();

            menu.Source = "menu.png";
		}
	}
}