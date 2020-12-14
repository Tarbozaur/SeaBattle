using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SeaBattle.Models
{
    public class MyAppContext : DbContext
    {
        public DbSet<HistoryItem> History { get; set; }
        public MyAppContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionStringBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = "Sea_Battle_History.db"
            }.ToString();

            var connection = new SqliteConnection(connectionStringBuilder);
            optionsBuilder.UseSqlite(connection);
        }

    }
}
