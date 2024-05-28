using Microsoft.EntityFrameworkCore;
using ProjectApi.Entities;

namespace ProjectApi.Database
{
    public class DBInvestmentContext: DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Currency> Currency { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Orders> Orders { get; set; }
  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-DJFJHDI;Database=Investment;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}