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
			await _connection.CreateTableAsync<SQLSettings>();

			var settings_name = new SQLSettings { Name = "Name123", Value = "Jurre123" };
            await _connection.InsertAsync(settings_name);

			var settings_Lastname = new SQLSettings { Name = "Lastname", Value = "Koetse" };
            await _connection.InsertAsync(settings_Lastname);

			var settings_Age = new SQLSettings { Name = "BirthYear", Value = "1996" };
            await _connection.InsertAsync(settings_Age);

			var settings_Hungry = new SQLSettings { Name = "Hungry", Value = "Yes" };
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
