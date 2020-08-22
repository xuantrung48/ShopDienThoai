using System.Collections.Generic;

namespace ShopDienThoai.Models.ProductModel
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> Get();
        Brand Get(int id);
        Brand Create(Brand brand);
        Brand Edit(Brand brand);
        bool Remove(int id);
    }
}