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
                settings.Name = "DB_Version"; settings.Value = "1";
                await _connection.InsertAsync(settings);

                settings.Name = "Name"; settings.Value = "Jurre";
                await _connection.InsertAsync(settings);

                settings.Name = "Lastname"; settings.Value = "Koetse";
                await _connection.InsertAsync(settings);

                settings.Name = "BirthYear"; settings.Value = "1996";
                await _connection.InsertAsync(settings);

                settings.Name = "Hungry"; settings.Value = "Yes";
                await _connection.InsertAsync(settings);
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
