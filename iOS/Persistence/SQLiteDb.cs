using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using BudgetBuddy.iOS;

[assembly: Dependency(typeof(SQLiteDb))]

namespace BudgetBuddy.iOS
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 
			var path = Path.Combine(documentsPath, "Database.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}

