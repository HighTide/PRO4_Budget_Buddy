using Xamarin.Forms;

namespace BudgetBuddy
{
    public partial class BudgetBuddyPage : ContentPage
    {

        public BudgetBuddyPage()
        {
            InitializeComponent();
        }

<<<<<<< HEAD
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Budget Buddy", "Test", "OK");
=======

        void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            Rene.Text = string.Format("Je Bent {0:F2}% Rene!", e.NewValue);
            //throw new System.NotImplementedException();
>>>>>>> 8e8aebf... * BudgetBuddyPage.xaml: Ik ben 55.15% Rene!
        }
    }
}
