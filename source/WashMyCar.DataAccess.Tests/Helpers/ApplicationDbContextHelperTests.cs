using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WashMyCar.Core.Domain;

namespace WashMyCar.DataAccess.Tests.Helpers
{
    public static class ApplicationDbContextHelperTests
    { /// <summary>
      /// Use this to use an SQLite InMemory database
      /// </summary>
        public static DbContextOptions<ApplicationDbContext> ApplicationDbContextOptionsSQLiteInMemory()
        {
            var connectionStringBuilder =
                new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            return options;
        }

        /// <summary>
        /// Use this to use an EF Core InMemory database
        /// </summary>
        public static DbContextOptions<ApplicationDbContext> ApplicationDbContextOptionsEfCoreInMemory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"WashMyCarDb{Guid.NewGuid()}")
                .Options;

            return options;
        }

        /// <summary>
        /// Use this when using SQLite InMemory database
        /// </summary>
        public static async void CreateDataBaseSQLiteInMemory(DbContextOptions<ApplicationDbContext> options)
        {
            await using (var context = new ApplicationDbContext(options))
            {
                await context.Database.OpenConnectionAsync();
                await context.Database.EnsureCreatedAsync();              
            }
        }

        /// <summary>
        /// Use this when using EF Core InMemory database
        /// </summary>
        public static async void CreateDataBaseEfCoreInMemory(DbContextOptions<ApplicationDbContext> options)
        {
            await using var context = new ApplicationDbContext(options);
            CreateData(context);
        }

        public static async Task CleanDataBase(DbContextOptions<ApplicationDbContext> options)
        {
            await using var context = new ApplicationDbContext(options);
            foreach (var color in context.Colors)
                context.Colors.Remove(color);
            await context.SaveChangesAsync();

            //await using (var context = new ApplicationDbContext(options))
            //{
            //    foreach (var type in context.types)
            //        context.types.Remove(type);
            //    await context.SaveChangesAsync();
            //}
        }

        public static void CreateData(ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Colors.Add(new Color()
            {
                Id = 1,
                Description = "Color Test 1"
            });
            applicationDbContext.Colors.Add(new Color()
            {
                Id = 2,
                Description = "Color Test 2"
            });
            applicationDbContext.Colors.Add(new Color()
            {
                Id = 3,
                Description = "Color Test 3"
            });

            applicationDbContext.SaveChangesAsync();
        }
    }
}