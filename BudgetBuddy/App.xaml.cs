using System;
using Xamarin.Forms;
using SQLite;
using BudgetBuddy.Properties;


namespace BudgetBuddy
{

    public partial class App : Application
    {
		private SQLiteAsyncConnection _connection;

        public App()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            //don't delete this line
            _connection.CreateTableAsync<SQL_Buttons>();
            _connection.CreateTableAsync<SQL_Transacties>();


            InitializeComponent();
            MainPage = new MainPage();


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

            if (allItems_Uitgaven == 0)
            {
                var uitgaven = new SQL_Transacties { };
                uitgaven.Date = DateTime.Now;
                uitgaven.Value = -230.10;
                uitgaven.Category = "Kleding";
                uitgaven.Name = "Blauwe Jas bij de Hennes und Maurits van het Merk: Armoeni";
                await _connection.InsertAsync(uitgaven);


                uitgaven.Date = DateTime.Now;
                uitgaven.Value = -30.10;
                uitgaven.Category = "Drinken";
                uitgaven.Name = "Cola bij de mac";
                await _connection.InsertAsync(uitgaven);
            }
                      
            
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


        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
