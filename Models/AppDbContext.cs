using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheGioiDienThoai.Models.OrderModel;
using TheGioiDienThoai.Models.ProductModel;
using TheGioiDienThoai.Models.UserModel;

namespace TheGioiDienThoai.Models
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
                    new Category()
                    {
                        CategoryId = 1,
                        Name = "Smartphone"
                    },
                    new Category()
                    {
                        CategoryId = 2,
                        Name = "Tablet"
                    },
                    new Category()
                    {
                        CategoryId = 3,
                        Name = "Laptop"
                    }
                );
            modelBuilder.Entity<Brand>().HasData(
                    new Brand()
                    {
                        BrandId = 1,
                        Name = "Apple"
                    },
                    new Brand()
                    {
                        BrandId = 2,
                        Name = "Samsung"
                    },
                    new Brand()
                    {
                        BrandId = 3,
                        Name = "VSmart"
                    },
                    new Brand()
                    {
                        BrandId = 4,
                        Name = "Oppo"
                    },
                    new Brand()
                    {
                        BrandId = 5,
                        Name = "Sony"
                    }
                );
            modelBuilder.Entity<Product>().HasData(
                    new Product()
                    {
                        ProductId = "1",
                        Name = "Galaxy A51",
                        Description = "Galaxy A51 8GB là phiên bản nâng cấp RAM của smartphone tầm trung đình đám Galaxy A51 từ Samsung. Sản phẩm nổi bật với thiết kế sang trọng, màn hình Infinity-O cùng cụm 4 camera đột phá.",
                        Price = 7790000,
                        //ImageFileName = "samsung-galaxy-a51-8gb-blue.png",
                        Screen = "Super AMOLED, \"6.5\",Full HD + ",
                        CPU = "Exynos 9611 8 nhân",
                        OS = "Android 10",
                        FrontCamera = "32 MP",
                        RearCamera = "Chính 48 MP & Phụ 12 MP, 5 MP, 5 MP",
                        Ram = 8,
                        Rom = 128,
                        Remain = 10
                    }
                );
        }
    }
}
