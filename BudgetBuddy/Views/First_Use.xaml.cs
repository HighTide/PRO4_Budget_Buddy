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
    public partial class First_Use : ContentPage
    {
        public First_Use()
        {
            
            InitializeComponent();

        }

        private void Next_OnClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Inkomsten();
        }
    }
}