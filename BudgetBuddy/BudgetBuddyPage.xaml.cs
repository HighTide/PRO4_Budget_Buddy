using Xamarin.Forms;
using BudgetBuddy.Views;
using SQLite;
using BudgetBuddy.Properties;
using System;

namespace BudgetBuddy
{
    public partial class BudgetBuddyPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        public string Button1Val = "Inkomsten";
        public string Button2Val = "Uitgaven";
        public string Button3Val = "Overzicht";
        public string Button4Val = "Settings";


        public BudgetBuddyPage()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            InitializeComponent();

            int month = System.DateTime.Now.Month;
            int year = System.DateTime.Now.Year;
            int days_this_month = System.DateTime.DaysInMonth(year, month);
            int days_left = days_this_month - System.DateTime.Now.Day;
            
        }

        protected override async void OnAppearing()
        {

            

            var buttons = await _connection.Table<SQL_Buttons>().ToListAsync();
            foreach (var item in buttons){
                if (item.Name == "Button1")
                {
                    Button1Val = item.Value;            
                }

                if (item.Name == "Button2")
                {
                    Button2Val = item.Value;
                }

                if (item.Name == "Button3")
                {
                    Button3Val = item.Value;
                }

                if (item.Name == "Button4")
                {
                    Button4Val = item.Value;
                }
            }
            Button1.Text = Button1Val;
            Button2.Text = Button2Val;
            Button3.Text = Button3Val;
            Button4.Text = Button4Val;
            base.OnAppearing();
        }


        void Text_Click(object sender, System.EventArgs e)
        {
            DisplayAlert("Budget Buddy", "Test", "OK");
            
        }



        async void Naar_Inkomen(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Inkomsten());
        }

        async void Naar_Uitgaven(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Uitgaven());
        }

        async void Overzicht(object sender, System.EventArgs e)
        {
			await Navigation.PushAsync(new Overzicht());
        }

        async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Settings());
        }
    }
}