using Microsoft.EntityFrameworkCore;
using NCKH_HANGOSELL.Models;

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
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình kiểu dữ liệu cho Price
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)").HasPrecision(18, 2);
            });

            // Dữ liệu sẵn cho Category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Electronics" },
                new Category { Id = 2, CategoryName = "Books" },
                new Category { Id = 3, CategoryName = "Clothing" }
            );

            // Dữ liệu sẵn cho Product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Smartphone",
                    Description = "Latest model smartphone",
                    Price = 699.99m,
                    Quantity = 50,
                    Status = "Available",
                    CategoryId = 1,
                    Image = "images/default.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Laptop",
                    Description = "High performance laptop",
                    Price = 1199.99m,
                    Quantity = 30,
                    Status = "Available",
                    CategoryId = 1,
                    Image = "images/default.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name = "Fiction Book",
                    Description = "Bestselling fiction book",
                    Price = 19.99m,
                    Quantity = 100,
                    Status = "Available",
                    CategoryId = 2,
                    Image = "images/default.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "T-Shirt",
                    Description = "Comfortable cotton t-shirt",
                    Price = 9.99m,
                    Quantity = 200,
                    Status = "Available",
                    CategoryId = 3,
                    Image = "images/default.jpg"
                }
            );

            // Dữ liệu sẵn cho Supplier
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, SupplyId = "SUP001", Name = "ABC Supplies", Email = "contact@abcsupplies.com", PhoneNumber = "123-456-7890", Address = "123 Main St, City, Country", CreatedAt = DateTime.UtcNow },
                new Supplier { Id = 2, SupplyId = "SUP002", Name = "XYZ Distributors", Email = "contact@xyzdistributors.com", PhoneNumber = "098-765-4321", Address = "456 Elm St, City, Country", CreatedAt = DateTime.UtcNow }
            );

            // Dữ liệu sẵn cho User
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserId = "admin", Password = "admin123", Name = "Admin", Email = "admin@example.com", DateOfBirth = new DateTime(1980, 1, 1), JoinDate = DateTime.UtcNow, PhoneNumber = "111-222-3333", Status = "Active", RoleName = "Admin", Position = "Administrator", Avatar = "images/default.jpg", RecordCreatedOn = DateTime.UtcNow },
                new User { Id = 2, UserId = "john_doe", Password = "password123", Name = "John Doe", Email = "john@example.com", DateOfBirth = new DateTime(1990, 5, 15), JoinDate = DateTime.UtcNow, PhoneNumber = "222-333-4444", Status = "Active", RoleName = "User", Position = "User", Avatar = "images/default.jpg", RecordCreatedOn = DateTime.UtcNow },
                new User { Id = 3, UserId = "guest_user", Password = "guest123", Name = "Guest User", Email = "guest@example.com", DateOfBirth = new DateTime(2000, 7, 20), JoinDate = DateTime.UtcNow, PhoneNumber = "333-444-5555", Status = "Inactive", RoleName = "Guest", Position = "Guest", Avatar = "images/default.jpg", RecordCreatedOn = DateTime.UtcNow }
            );
        }
    }
}
