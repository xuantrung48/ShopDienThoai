using System.Collections.Generic;
using ShopDienThoai.ViewModels.Product;

namespace ShopDienThoai.Models.ProductModel
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get();
        ProductDetailViewModel Get(string id);
        Product Create(Product product);
        Product Edit(Product product);
        bool Remove(string id);
    }
}