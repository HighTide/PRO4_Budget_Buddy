using System;
using Xamarin.Forms;
using SQLite;
using BudgetBuddy.Properties;
using BudgetBuddy.Views;


namespace BudgetBuddy
{

    public partial class App : Application
    {
		private SQLiteAsyncConnection _connection;
        private DateTime Datum;

        public App()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            //don't delete this line
            _connection.CreateTableAsync<SQL_Buttons>();
            _connection.CreateTableAsync<SQL_Transacties>();
            _connection.CreateTableAsync<SQL_Budget>();


            InitializeComponent();
            if (App.Current.Properties.ContainsKey("savedPropA"))
            {
                MainPage = new WelcomeBack();

            }
            else
            {
                MainPage = new First_Use();

            }
            


        }


        protected async override void OnStart()
        {            
            //Create SQL Connection
            await _connection.CreateTableAsync<SQL_Buttons>();

            int allItems_Buttons = await _connection.Table<SQL_Buttons>().CountAsync();
            System.Diagnostics.Debug.WriteLine(allItems_Buttons);

            if (allItems_Buttons == 0)
            {
                var buttons = new SQL_Buttons { };
                buttons.Value = "Inkomsten";
                buttons.Name = "Button1";
                await _connection.InsertAsync(buttons);

                buttons.Value = "Uitgaven";
                buttons.Name = "Button2";
                await _connection.InsertAsync(buttons);

                buttons.Value = "Spaardoelen";
                buttons.Name = "Button3";
                await _connection.InsertAsync(buttons);

                buttons.Value = "Overzicht";
                buttons.Name = "Button4";
                await _connection.InsertAsync(buttons);
            }


            int allItems_Budget = await _connection.Table<SQL_Budget>().CountAsync();
            System.Diagnostics.Debug.WriteLine(allItems_Buttons);

            if (allItems_Budget == 0)
            {
                var Budget = new SQL_Budget();
                Budget.Value = 0.00;
                Budget.Name = "Budget";
                Budget.Date = DateTime.Now;
                await _connection.InsertAsync(Budget);

            }

            await _connection.CreateTableAsync<SQL_Settings>();

            int allItems = await _connection.Table<SQL_Settings>().CountAsync();
            System.Diagnostics.Debug.WriteLine(allItems);

            if (allItems == 0)
            {
                // only insert the data if it doesn't already exist
                var settings = new SQL_Settings { };
                settings.Name = "DB_Version";
                settings.Value = "2";
                await _connection.InsertAsync(settings);

                settings.Name = "Last_Seen";
				settings.Value = DateTime.Now.ToString();
                await _connection.InsertAsync(settings);            

            }

            //Create SQL Connection
            await _connection.CreateTableAsync<SQL_Transacties>();

            int allItems_Uitgaven = await _connection.Table<SQL_Transacties>().CountAsync();
            System.Diagnostics.Debug.WriteLine(allItems);

            
            await _connection.CreateTableAsync<SQL_SpaarDoelen>();
            int allItems_SpaarDoelen = await _connection.Table<SQL_SpaarDoelen>().CountAsync();
            System.Diagnostics.Debug.WriteLine(allItems);



            await _connection.CreateTableAsync<SQL_Category>();
            int allItems_Category = await _connection.Table<SQL_Category>().CountAsync();
            System.Diagnostics.Debug.WriteLine(allItems);

            if (allItems_Category == 0)
            {
                var Category = new SQL_Category { };
                Category.Name = "Kleding";
                Category.Income = false;
                await _connection.InsertAsync(Category);

                Category.Name = "Drinken";
                Category.Income = false;
                await _connection.InsertAsync(Category);

                Category.Name = "Eten";
                Category.Income = false;
                await _connection.InsertAsync(Category);

                Category.Name = "Boodschappen";
                Category.Income = false;
                await _connection.InsertAsync(Category);

                Category.Name = "Amusement";
                Category.Income = false;
                await _connection.InsertAsync(Category);

                Category.Name = "Abonnementen";
                Category.Income = false;
                await _connection.InsertAsync(Category);

                Category.Name = "Loon";
                Category.Income = true;
                await _connection.InsertAsync(Category);

                Category.Name = "Cadeau";
                Category.Income = true;
                await _connection.InsertAsync(Category);

                Category.Name = "Winst";
                Category.Income = true;
                await _connection.InsertAsync(Category);

                Category.Name = "Uitkering";
                Category.Income = true;
                await _connection.InsertAsync(Category);

                Category.Name = "Creditering";
                Category.Income = true;
                await _connection.InsertAsync(Category);

            }

            DailyBudgetAdd();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private async void DailyBudgetAdd()
        {
            double total = 0.00;
            var recur = await _connection.QueryAsync<SQL_Budget>("SELECT * FROM SQL_Budget WHERE NAME = 'Budget'");
            foreach (var item in recur)
            {
                Datum = item.Date;
                total += item.Value;

            }

            if ((DateTime.Now.Date - Datum.Date).TotalDays >= 1)
            {
                double days = (DateTime.Now.Date - Datum.Date).TotalDays;
                int dayss = Convert.ToInt32(Math.Floor(days));
                while(dayss > 0)
                {
                    dayss --;
                    int s = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    var recurring = await _connection.QueryAsync<SQL_Transacties>("SELECT Value FROM SQL_Transacties WHERE Recurring");
                    foreach (var item in recurring)
                    {
                        total += item.Value / s;
                    }
                    await _connection.ExecuteAsync("Update SQL_Budget SET Value = ? Where Name = ?", total, "Budget");
                }
                await _connection.ExecuteAsync("Update SQL_Budget SET Date = ? Where Name = ?", DateTime.Now, "Budget");

            }
        }
    }
}
