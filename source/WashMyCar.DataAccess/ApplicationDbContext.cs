using Microsoft.EntityFrameworkCore;
using WashMyCar.Core.Domain;

namespace WashMyCar.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Color> Colors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
        }
    }
}
