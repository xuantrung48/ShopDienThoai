using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopDienThoai.Models.OrderModel;
using ShopDienThoai.Models.ProductModel;
using ShopDienThoai.Models.UserModel;

namespace ShopDienThoai.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<CarouselImage> CarouselImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Smartphone"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Tablet"
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Laptop"
                }
            );
            modelBuilder.Entity<Brand>().HasData(
                new Brand
                {
                    BrandId = 1,
                    Name = "Apple"
                },
                new Brand
                {
                    BrandId = 2,
                    Name = "Samsung"
                },
                new Brand
                {
                    BrandId = 3,
                    Name = "VSmart"
                },
                new Brand
                {
                    BrandId = 4,
                    Name = "Oppo"
                },
                new Brand
                {
                    BrandId = 5,
                    Name = "Sony"
                }
            );
        }
    }
}