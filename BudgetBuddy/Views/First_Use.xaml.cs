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
            Navigation.PushModalAsync(new MainPage());
        }
        protected override async void OnAppearing()
        {
            App.Current.Properties.Add("savedPropA", "start");
            await App.Current.SavePropertiesAsync();
            base.OnAppearing();
        }
    }
}