using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;
using SQLite;
using BudgetBuddy.Properties;
using BudgetBuddy.Views;
using System.Threading.Tasks;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;

namespace BudgetBuddy
{

    public partial class App : Application
    {
		private SQLiteAsyncConnection _connection; //Used for SQL Connection

        private DateTime _LastSeen; //Used in BudgetPlayback();

       // private DateTime Datum; //Used in DailyBudgetAdd();
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
			AppCenter.LogLevel = LogLevel.Verbose;

            //Connect to Analytics to collect those juicy statss
			AppCenter.Start("android=a9636b16-80a9-46ca-8381-91c6f1b44948;" + "ios=8fc92317-8677-4269-b461-548ebb3469f8", typeof(Analytics), typeof(Crashes), typeof(Push));


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

            BudgetPlayback();
            //DailyBudgetAdd();
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
            double budget = 0.00;
            double total = 0.00;
            

            //Get the current Budget from the budget Table
            var budgetTable = await _connection.QueryAsync<SQL_Budget>("SELECT * FROM SQL_Budget WHERE NAME = 'Budget'");

            foreach (var item in budgetTable) // Loop trough all returned rows and count them together because its more work to just select 1 entry :P
            {
                _LastSeen = item.Date; //Place lastseen DateTime Inside Global _LastSeen
                budget += item.Value; //Load the current value inside Local budget
            }

            //Check if the current date is diffrent then the _LastSeen Value, or if its called inside the app for debugging.
            //All Logic relating to calculating budget for new days happens here.
            if (((DateTime.Now.Date - _LastSeen.Date).TotalDays >= 1) || (manual > 0))
            {
                int days = Convert.ToInt32(Math.Floor((DateTime.Now.Date - _LastSeen.Date).TotalDays)); //Calculate how many days it has been since the app was last opened.

                if (days == 0) //Quick check if the function is called because of new day or for debugging.
                {
                    days = manual; //Override the actual day count, for debug value.
                }

                budget = await BudgetPlaybackSpaardoel(days, budget);
                budget = await BudgetPlaybackBudget(days, budget);

                await _connection.ExecuteAsync("Update SQL_Budget SET Value = ?, Date = ? Where Name = ?", budget, DateTime.Now, "Budget");

				Analytics.TrackEvent("Dagelijkse Update");
                


            }

            await _connection.ExecuteAsync("Update SQL_Budget SET Date = ? Where Name = ?", DateTime.Now, "Budget"); //Update Lastseen

            MessagingCenter.Send<App>(this, "BudgetUpdate");  //Force UI Update on BudgetBuddyPage
        }

        public async Task<double> BudgetPlaybackSpaardoel(int days, double budget)
        {
            while (days > 0) //This is the main loop responsible for calculating all spaardoel progression.
            {
                //Defining some local variables
                var playbackDate = DateTime.Now.AddDays(-days + 1); //Want to make sure to calculate for the right day.
                var spaardoelen = await _connection.QueryAsync<SQL_SpaarDoelen>("SELECT * FROM SQL_SpaarDoelen WHERE NOT Completed"); //Load all not completed Spaardoelen from the database.

                foreach (var item in spaardoelen) //Loop to make sure we don't forget a Spaardoel, selected from query above.
                {
                    //This loop contains 2 tasks:
                    //Task 1, is to add all the deposits to the transaction table
                    //Task 2, is to check if we have completed the spaardoel

                    //Task 1: Prepairing new transaction
                    var transaction = new SQL_Transacties  
                    {
                        Date = playbackDate,
                        Value = item.Value,
                        Category = "Inleg Spaardoel",
                        Name = "Inleg Spaardoel: " + item.Name,
                        Recurring = false
                    };
                    Debug.WriteLine("Updating budget, ? lowered the budget with ?", item.Name, item.Value);
                    await _connection.InsertAsync(transaction); //Push transaction to database
                    
                    budget += transaction.Value; //Updating the budget, with the daily deposit amount


                    //Task 2: Check if spaardoel is completed
                    if (item.Days <= 0)
                    {
                        //change Completed to True
                        await _connection.ExecuteAsync("Update SQL_SpaarDoelen SET Completed = 1 Where Name = ?", item.Name);
                        Debug.WriteLine("Spaardoel ? is completed, Updating to completed!", item.Name);
                    }
                    else
                    {
                        //lower day by 1
                        //item.Days--;
                        await _connection.ExecuteAsync("Update SQL_SpaarDoelen SET Days = ? Where Name = ?", (item.Days-1), item.Name);
                        Debug.WriteLine("Spaardoel ? Days have been lowered by 1, ? Remaining", item.Name, item.Days);
                    }
                }
                days--; //Lower the day count by 1 because otherwise we'll be stuck here for a looooooong while
            }

            return budget; //Return new budget
        }

        public async Task<double> BudgetPlaybackBudget(int days, double budget)
        {
            while (days > 0) //This is the main loop responsible for the daily budget calculations.
            {
                double transvalue = 0;
                int s = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddDays((-days + 1)).Month);

                var recurringTransacties = await _connection.QueryAsync<SQL_Transacties>("SELECT Value FROM SQL_Transacties WHERE Recurring");
                foreach (var item in recurringTransacties)
                {
                    transvalue += item.Value / s;
                    budget += item.Value / s;
                }


                //add new transaction for overviews
                var transaction = new SQL_Transacties();
                transaction.Date = DateTime.Now.AddDays((-days + 1));
                transaction.Value = transvalue;
                transaction.Category = "Budget";
                transaction.Name = ("Budget voor " + transaction.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                transaction.Recurring = false;
                await _connection.InsertAsync(transaction);


                days--; //Lower the day count by 1 because otherwise we'll be stuck here
            }

            return budget; //Return new budget
        }
    }
}
