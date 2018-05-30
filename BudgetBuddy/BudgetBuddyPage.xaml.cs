using Xamarin.Forms;
using BudgetBuddy.Views;

namespace BudgetBuddy
{
    public partial class BudgetBuddyPage : ContentPage
    {

        public BudgetBuddyPage()
        {
            InitializeComponent();
        }

        void Text_Click(object sender, System.EventArgs e)
        {
            DisplayAlert("Budget Buddy", "Test", "OK");
        }



        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SQL());
        }

        async void Naar_Uitgaven(object sender, System.EventArgs e)
        {
            await Uitgaven.PushAsync(new SQL());
        }

        async void Overzicht(object sender, System.EventArgs e)
        {
            await Overzicht.PushAsync(new SQL());
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            await Settings.PushAsync(new SQL());
        }
    }
}