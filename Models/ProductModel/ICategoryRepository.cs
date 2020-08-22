using System.Collections.Generic;

namespace ShopDienThoai.Models.ProductModel
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Get();
        Category Get(int id);
        Category Create(Category category);
        Category Edit(Category category);
        bool Remove(int id);
    }
}