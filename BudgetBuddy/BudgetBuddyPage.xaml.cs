using Xamarin.Forms;

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


        void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            Rene.Text = string.Format("Je Bent {0:F2}% Rene!", e.NewValue);
            //throw new System.NotImplementedException();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new stfuPage());
        }
    }
}
