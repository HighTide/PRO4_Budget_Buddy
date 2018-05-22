using System;
using SQLite;
namespace BudgetBuddy.Properties
                     
                     
{
	public class SQL_Settings
	{
		[PrimaryKey]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Value { get; set; }
    }
    
	public class SQL_Uitgaven
    {
        [PrimaryKey]
		public DateTime Date { get; set; }

        [MaxLength(255)]
        public double Value { get; set; }

		[MaxLength(255)]
		public string Category { get; set; }

		[MaxLength(255)]
        public string Name { get; set; }
    }

    public class SQL_Inkomsten
    {
        [PrimaryKey]
        public DateTime Date { get; set; }

        [MaxLength(255)]
        public double Value { get; set; }

        [MaxLength(255)]
        public string Category { get; set; }
    }

	public class SQL_Category 
	{
        [PrimaryKey]
        public string Name { get; set; }
    }
}

