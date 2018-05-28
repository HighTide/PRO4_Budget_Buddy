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
            InitializeComponent();
            MainPage = new MainPage();

            
			_connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }


        protected async override void OnStart()
        {
            //Create SQL Connection
            await _connection.CreateTableAsync<SQL_Settings>();

            int allItems = await _connection.Table<SQL_Settings>().CountAsync();
            System.Diagnostics.Debug.WriteLine(allItems);

            if (allItems == 0)
            {
                // only insert the data if it doesn't already exist
                var settings = new SQL_Settings { };
                settings.Name = "DB_Version";
                settings.Value = "1";
                await _connection.InsertAsync(settings);

                settings.Name = "Name";
                settings.Value = "Jurre";
                await _connection.InsertAsync(settings);

                settings.Name = "Lastname";
                settings.Value = "Koetse";
                await _connection.InsertAsync(settings);

                settings.Name = "BirthYear";
                settings.Value = "1996";
                await _connection.InsertAsync(settings);

                settings.Name = "Hungry";
                settings.Value = "Yes";
                await _connection.InsertAsync(settings);

            }

            //Create SQL Connection
            await _connection.CreateTableAsync<SQL_Uitgaven>();

            int allItems_Uitgaven = await _connection.Table<SQL_Uitgaven>().CountAsync();
            System.Diagnostics.Debug.WriteLine(allItems);

            if (allItems_Uitgaven == 0)
            {
                var uitgaven = new SQL_Uitgaven { };
                uitgaven.Date = DateTime.Now;
                uitgaven.Value = 23.10;
                uitgaven.Category = "Kleding";
                uitgaven.Name = "Blauwe Jas bij de Hennes und Maurits van het Merk: Armoeni";
                await _connection.InsertAsync(uitgaven);


                uitgaven.Date = DateTime.Now;
                uitgaven.Value = 30.10;
                uitgaven.Category = "Drinken";
                uitgaven.Name = "Cola bij de mac";
                await _connection.InsertAsync(uitgaven);
            }

            await _connection.CreateTableAsync<SQL_Inkomsten>();

			int allItems_Inkomsten = await _connection.Table<SQL_Inkomsten>().CountAsync();
            System.Diagnostics.Debug.WriteLine(allItems);

   
            if (allItems_Inkomsten == 0)
            {
                var uitgaven = new SQL_Uitgaven { };
                uitgaven.Date = DateTime.Now;
                uitgaven.Value = 300;
                uitgaven.Category = "Salaris";
				uitgaven.Name = "Salaris CocaCola Coke Plantage";
                await _connection.InsertAsync(uitgaven);

                
            }

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
                Category.Income = true;
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
