﻿using System;
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
    
	public class SQL_Transacties
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

	public class SQL_Category 
	{
        [PrimaryKey]
        public string Name { get; set; }

        public bool Income { get; set; }
    }

	public class SQL_SpaarDoelen
    {
		[PrimaryKey]
        public DateTime Date { get; set; }

        public string Name { get; set; }
       
        public double Value { get; set; }

		public double Goal { get; set; }

    }
    public class SQL_Buttons
    {
        [MaxLength(255)]
        public string Value { get; set; }
        
        [PrimaryKey]
        public string Name { get; set; }
    }
}

