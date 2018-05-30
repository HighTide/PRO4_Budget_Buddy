using Xamarin.Forms;
using BudgetBuddy.Views;
using SQLite;
using BudgetBuddy.Properties;

namespace BudgetBuddy
{
    public partial class BudgetBuddyPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        public string Button1Val;
        public string Button2Val;
        public string Button3Val;
        public string Button4Val;

        public BudgetBuddyPage()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {           
            var buttons = await _connection.Table<SQL_Buttons>().ToListAsync();
            foreach (var item in buttons){
                if (item.Name == "Button1")
                {
                    var Button1Val = item.Value;            
                }

                if (item.Name == "Button2")
                {
                    var Button2Val = item.Value;
                }

                if (item.Name == "Button3")
                {
                    var Button3Val = item.Value;
                }

                if (item.Name == "Button4")
                {
                    var Button4Val = item.Value;
                }
            }
            Button1.Text = Button1Val;
            base.OnAppearing();
        }


        void Text_Click(object sender, System.EventArgs e)
        {
            DisplayAlert("Budget Buddy", "Test", "OK");
            
        }


       

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SQL());
        }
    }
}