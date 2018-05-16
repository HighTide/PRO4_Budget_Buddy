using System;
using SQLite;
namespace BudgetBuddy.Properties
                     
                     
{
	public class SQLSettings
    {

        public string Name { get; set; }

        [MaxLength(255)]
        public string Value { get; set; }
    }

}
