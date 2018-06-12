using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SQLite;
using BudgetBuddy.Properties;
using BudgetBuddy.Views;
using System.Threading.Tasks;

namespace BudgetBuddy
{

    public partial class App : Application
    {
		private SQLiteAsyncConnection _connection;
        private DateTime Datum;
        private string hex1 = "#303030";
        

        public App()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            //don't delete these lines, this creates the tables before anything is shown.
            _connection.CreateTableAsync<SQL_Buttons>();
            _connection.CreateTableAsync<SQL_Transacties>();
            _connection.CreateTableAsync<SQL_Budget>();




            InitializeComponent();
            // checks if color theme has been adjusted
            if (App.Current.Properties.ContainsKey("savedPropB"))
            {
                App.Current.Resources["backgroundColor"] = Color.FromHex(hex1);
                App.Current.Resources["textColor"] = Color.White;

            }

            // checks if this is first time starting app or not
            if (App.Current.Properties.ContainsKey("savedPropA"))
            {
                //happens if this is not the first time
                MainPage = new WelcomeBack();

            }
            else
            {
                //happens if this is the first time
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

                Category.Name = "Inleg Spaardoel";
                Category.Income = false;
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

        public async void BudgetPlayback(int manual = 0)
        {
            //Local Variables
            double _budget = 0.00;
            double total = 0.00;



        }

        public async void DailyBudgetAdd(int menual = 0)
        {
            //some lcal variables
            double _budgett = 0.00;
            double total = 0.00;

            //loop through the budget to get the values we need to work with
            var recur = await _connection.QueryAsync<SQL_Budget>("SELECT * FROM SQL_Budget WHERE NAME = 'Budget'");
            foreach (var item in recur)
            {
                Datum = item.Date;
                _budgett += item.Value;
            }

            if (((DateTime.Now.Date - Datum.Date).TotalDays >= 1) || (menual > 0))
            {
                //some local variables
                double transvalue = 0;
                double days = (DateTime.Now.Date - Datum.Date).TotalDays;
                int dayss = Convert.ToInt32(Math.Floor(days));

                if(dayss == 0)
                {
                    dayss = menual;
                }


                _budgett = await DailyBudgetSpaardoelAdd(dayss, _budgett);
                total += _budgett;

                while (dayss > 0)
                {
                    transvalue = 0;
                    int s = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddDays((-dayss + 1)).Month);
                    var recurring = await _connection.QueryAsync<SQL_Transacties>("SELECT Value FROM SQL_Transacties WHERE Recurring");
                    foreach (var item in recurring)
                    {
                        transvalue += item.Value / s;
                        total += item.Value / s;
                    }
                    

                    //add new transaction for overviews
                    var Transaction = new SQL_Transacties();
                    Transaction.Date = DateTime.Now.AddDays((-dayss + 1));
                    Transaction.Value = transvalue;
                    Transaction.Category = "Budget";
                    Transaction.Name = "Budget";
                    Transaction.Recurring = false;
                    await _connection.InsertAsync(Transaction);


                    
                    dayss--;
                }
                await _connection.ExecuteAsync("Update SQL_Budget SET Value = ?, Date = ? Where Name = ?", total, DateTime.Now, "Budget");
                await _connection.ExecuteAsync("Update SQL_Budget SET Date = ? Where Name = ?", DateTime.Now, "Budget");

            }
        }

        private async Task<double> DailyBudgetSpaardoelAdd(int dayss, double budget)
        {


            while (dayss > 0)
            {
                CheckIfSpaardoelCompleted();
                //Defining some local Variables
                var Spaardoelen = await _connection.QueryAsync<SQL_Transacties>("SELECT * FROM SQL_SpaarDoelen WHERE NOT Completed");
                var PlaybackDate = DateTime.Now.AddDays(-dayss + 1);

                //Loop trough all the Spaardoelen, selected by the quarry above
                foreach (var item in Spaardoelen)
                {
                    //Prepairing Transaction
                    var Transaction = new SQL_Transacties();
                    Transaction.Date = PlaybackDate;
                    Transaction.Value = item.Value;
                    Transaction.Category = "Inleg Spaardoel";
                    Transaction.Name = "Inleg Spaardoel: " + item.Name;
                    Transaction.Recurring = false;
                    await _connection.InsertAsync(Transaction);


                    //Doing Lame shit because they did not make function
                    budget += Transaction.Value;


                    

                }
                //Lower Days in Spaardoelen Table, by 1
                

                //Lower While loop by 1
                dayss--;
            }
            return budget;
        }

        private async void CheckIfSpaardoelCompleted()
        {
            //Get date from database
            var Spaardoelen = await _connection.QueryAsync<SQL_SpaarDoelen>("SELECT * FROM SQL_SpaarDoelen WHERE NOT Completed");

            //loop trough the data
            foreach (var item in Spaardoelen)
            {

                //check if spaardoel is completed
                if (item.Days <= 0)
                {
                    //change Completed to True
                    await _connection.ExecuteAsync("Update SQL_SpaarDoelen SET Completed = 1 Where Name = ?", item.Name);
                }
                else
                {
                    //lower day by 1
                    await _connection.ExecuteAsync("Update SQL_SpaarDoelen SET Days = ? Where Name = ?", item.Days, item.Name);
                }
                
            }
            
        }

    }
}
