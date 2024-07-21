using Microsoft.EntityFrameworkCore;
using NCKH_HANGOSELL.Models;
using System.Threading;
using System.Threading.Tasks;

namespace NCKH_HANGOSELL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<User>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    var user = entry.Entity;
                    if (string.IsNullOrEmpty(user.Avatar))
                    {
                        user.Avatar = "/default-avatar.jpg";
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
