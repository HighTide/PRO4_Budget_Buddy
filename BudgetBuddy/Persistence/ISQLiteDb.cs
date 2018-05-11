using SQLite;

namespace BudgetBuddy
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

