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
    public partial class WelcomeBack : ContentPage
    {
        public WelcomeBack()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Application.Current.MainPage = new MainPage();

        }
    }
}