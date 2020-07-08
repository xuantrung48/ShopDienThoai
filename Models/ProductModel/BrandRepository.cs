using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheGioiDienThoai.Models.ProductModel
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext context;
        public BrandRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Brand Create(Brand brand)
        {
            context.Brands.Add(brand);
            context.SaveChanges();
            return brand;
        }

        public Brand Edit(Brand brand)
        {
            var editBrand = context.Brands.Attach(brand);
            editBrand.State = EntityState.Modified;
            context.SaveChanges();
            return brand;
        }

        public IEnumerable<Brand> Get()
        {
            return context.Brands;
        }

        public Brand Get(int id)
        {
            var brand = (from e in context.Brands
                         where e.BrandId == id
                         select e).FirstOrDefault();
            return brand;
        }

        public bool Remove(int id)
        {
            var brandToRemove = context.Brands.Find(id);
            if(brandToRemove != null)
            {
                context.Brands.Remove(brandToRemove);
                return context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
