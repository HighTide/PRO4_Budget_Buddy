using Xamarin.Forms;

namespace BudgetBuddy
{
    public partial class BudgetBuddyPage : ContentPage
    {
        public BudgetBuddyPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Welcome", "This is Budget Buddy", "OK");
        }
    }
}
