using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using BudgetBuddy.Droid;

[assembly: Dependency(typeof(SQLiteDb))]

namespace BudgetBuddy.Droid
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

