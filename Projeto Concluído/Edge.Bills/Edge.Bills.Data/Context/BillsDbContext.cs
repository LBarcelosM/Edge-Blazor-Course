using Microsoft.EntityFrameworkCore;

namespace Edge.Bills.Data.Context
{
    public class BillsDbContext : DbContext
    {
        public BillsDbContext(DbContextOptions<BillsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillsDbContext).Assembly);
        }
    }
}
