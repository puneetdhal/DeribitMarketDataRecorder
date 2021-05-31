using DeribitDAL.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DeribitDAL
{
    public class DeribitDbContext : DbContext
    {
        private static readonly DbConnection _dbConnection =
            new SqliteConnection("Data Source=InMemoryDeribitDb;Mode=Memory;Cache=Shared");

        public DbSet<DeribitInstrumentPriceHistoryItem> DeribitInstrumentPriceHistory { get; set; }

        static DeribitDbContext()
        {
            _dbConnection.Open();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_dbConnection);
            options.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeribitInstrumentPriceHistoryItem>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<DeribitInstrumentPriceHistoryItem>().ToTable(nameof(DeribitInstrumentPriceHistory));
        }
    }
}
