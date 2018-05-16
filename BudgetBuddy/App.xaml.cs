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
			await _connection.CreateTableAsync<SQL_Settings>();

			var settings_name = new SQL_Settings { Name = "Name123", Value = "Jurre123" };
            await _connection.InsertAsync(settings_name);

			var settings_Lastname = new SQL_Settings { Name = "Lastname", Value = "Koetse" };
            await _connection.InsertAsync(settings_Lastname);

			var settings_Age = new SQL_Settings { Name = "BirthYear", Value = "1996" };
            await _connection.InsertAsync(settings_Age);

			var settings_Hungry = new SQL_Settings { Name = "Hungry", Value = "Yes" };
            await _connection.InsertAsync(settings_Hungry);

            
                          
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
