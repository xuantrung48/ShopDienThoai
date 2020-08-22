using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ShopDienThoai.Models.ProductModel
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
            brand.IsDeleted = false;
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
            return from b in context.Brands where b.IsDeleted == false select b;
        }

        public Brand Get(int id)
        {
            var brand = (from e in context.Brands
                         where e.IsDeleted == false
                               && e.BrandId == id
                         select e).FirstOrDefault();
            return brand;
        }

        public bool Remove(int id)
        {
            var brandToRemove = context.Brands.Find(id);
            if (brandToRemove != null)
            {
                var productsInBrand = from p in context.Products where p.BrandId == id select p;
                if (productsInBrand.ToList().Count > 0)
                    return false;
                brandToRemove.IsDeleted = true;
                return context.SaveChanges() > 0;
            }

            return false;
        }
    }
}