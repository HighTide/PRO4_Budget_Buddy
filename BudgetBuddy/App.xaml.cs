using Xamarin.Forms;
using SQLite;

namespace BudgetBuddy
{
	public class Settings
    {

        public string Name { get; set; }

        [MaxLength(255)]
        public string Value { get; set; }
    }

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
			await _connection.CreateTableAsync<Settings>();

			var settings_name = new Settings { Name = "Name123", Value = "Jurre123" };
            await _connection.InsertAsync(settings_name);

			var settings_Lastname = new Settings { Name = "Lastname", Value = "Koetse" };
            await _connection.InsertAsync(settings_Lastname);

			var settings_Age = new Settings { Name = "BirthYear", Value = "1996" };
            await _connection.InsertAsync(settings_Age);

			var settings_Hungry = new Settings { Name = "Hungry", Value = "Yes" };
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
