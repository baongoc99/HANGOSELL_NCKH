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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)"); // Cấu hình kiểu dữ liệu cho Price
                // Hoặc sử dụng HasPrecision nếu bạn muốn
                // entity.Property(e => e.Price).HasPrecision(18, 2);
            });
        }
    }
}
