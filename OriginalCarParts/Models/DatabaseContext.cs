namespace OriginalCarParts.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DatabaseContext : DbContext
    {
        // Your context has been configured to use a 'DatabaseContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Demo2.Models.DatabaseContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DatabaseContext' 
        // connection string in the application configuration file.
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<IncomeDetails> IncomeDetails { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

    }
}