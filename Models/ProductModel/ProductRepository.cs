using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ShopDienThoai.ViewModels.Product;

namespace ShopDienThoai.Models.ProductModel
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IImageRepository imageRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductRepository(AppDbContext context, IImageRepository imageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.imageRepository = imageRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public Product Create(Product product)
        {
            product.IsDeleted = false;
            product.CreatedTime = DateTime.Now;
            product.ProductId = Guid.NewGuid().ToString();
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product Edit(Product product)
        {
            var editProduct = context.Products.Attach(product);
            editProduct.State = EntityState.Modified;
            context.SaveChanges();
            return product;
        }

        public IEnumerable<Product> Get()
        {
            return (from p in context.Products where p.IsDeleted == false select p).OrderByDescending(
                p => p.CreatedTime);
        }

        public ProductDetailViewModel Get(string id)
        {
            var data = (from e in context.Products
                where e.IsDeleted == false
                join d in context.Categories on e.CategoryId equals d.CategoryId
                join f in context.Brands on e.BrandId equals f.BrandId
                where e.ProductId == id
                select new ProductDetailViewModel
                {
                    ProductId = e.ProductId,
                    BrandId = e.BrandId,
                    BrandName = f.Name,
                    CategoryId = e.CategoryId,
                    CategoryName = d.Name,
                    CPU = e.CPU,
                    Description = e.Description,
                    FrontCamera = e.FrontCamera,
                    Name = e.Name,
                    OS = e.OS,
                    Price = e.Price,
                    Ram = e.Ram,
                    RearCamera = e.RearCamera,
                    Remain = e.Remain,
                    Rom = e.Rom,
                    Screen = e.Screen
                }).FirstOrDefault();
            return data;
        }

        public bool Remove(string id)
        {
            var productToRemove = context.Products.Find(id);
            if (productToRemove != null)
            {
                /*var images = (from e in context.Images
                              where e.ProductId == productToRemove.ProductId
                              select e).ToList();
                foreach (var image in images)
                {
                    imageRepository.Remove(image.ImageId);

                    string delFile = Path.Combine(webHostEnvironment.WebRootPath, "images\\products", image.ImageName);
                    File.Delete(delFile);
                }*/
                productToRemove.IsDeleted = true;
                return context.SaveChanges() > 0;
            }

            return false;
        }
    }
}