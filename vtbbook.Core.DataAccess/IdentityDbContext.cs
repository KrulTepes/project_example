using Microsoft.EntityFrameworkCore;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Core.DataAccess
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<DbSome> Somes { get; set; }

        public IdentityDbContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
