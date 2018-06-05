using Xamarin.Forms;
using BudgetBuddy.Views;
using SQLite;
using BudgetBuddy.Properties;

namespace BudgetBuddy
{
    public partial class BudgetBuddyPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        public string Button1Val = "Inkomsten";
        public string Button2Val = "Uitgaven";
        public string Button3Val = "Spaardoelen";
        public string Button4Val = "Overzicht";


        public BudgetBuddyPage()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            InitializeComponent();


            
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



        async void Button_Pressed1(object sender, System.EventArgs e)
        {
            if (Button1.Text == "Uitgaven")
            {
                await Navigation.PushAsync(new Uitgaven());
            }
            if (Button1.Text == "Inkomsten")
            {
                await Navigation.PushAsync(new Inkomsten());
            }
            if (Button1.Text == "Spaardoelen")
            {
                await Navigation.PushAsync(new Spaardoelen());
            }
            if (Button1.Text == "Overzicht")
            {
                await Navigation.PushAsync(new Overzicht());
            }
        }   

        async void Button_Pressed2(object sender, System.EventArgs e)
        {
            if (Button2.Text == "Uitgaven")
            {
                await Navigation.PushAsync(new Uitgaven());
            }
            if (Button2.Text == "Inkomsten")
            {
                await Navigation.PushAsync(new Inkomsten());
            }
            if (Button2.Text == "Spaardoelen")
            {
                await Navigation.PushAsync(new Spaardoelen());
            }
            if (Button2.Text == "Overzicht")
            {
                await Navigation.PushAsync(new Overzicht());
            }
        }

        async void Button_Pressed3(object sender, System.EventArgs e)
        {
            if (Button3.Text == "Uitgaven")
            {
                await Navigation.PushAsync(new Uitgaven());
            }
            if (Button3.Text == "Inkomsten")
            {
                await Navigation.PushAsync(new Inkomsten());
            }
            if (Button3.Text == "Spaardoelen")
            {
                await Navigation.PushAsync(new Spaardoelen());
            }
            if (Button3.Text == "Overzicht")
            {
                await Navigation.PushAsync(new Overzicht());
            }
        }

        async void Button_Pressed4(object sender, System.EventArgs e)
        {
            if (Button4.Text == "Uitgaven")
            {
                await Navigation.PushAsync(new Uitgaven());
            }
            if (Button4.Text == "Inkomsten")
            {
                await Navigation.PushAsync(new Inkomsten());
            }
            if (Button4.Text == "Spaardoelen")
            {
                await Navigation.PushAsync(new Spaardoelen());
            }
            if (Button4.Text == "Overzicht")
            {
                await Navigation.PushAsync(new Overzicht());
            }
        }
    }
}