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
    public partial class Overzicht_Detail : ContentPage
    {
        public Overzicht_Detail(string data)
        {
            InitializeComponent();

            String Category = data;
            Category_Texst.Text = string.Format("Dit zijn al jouw uitgaven in {0:F2}", Category);
        }
    }
}