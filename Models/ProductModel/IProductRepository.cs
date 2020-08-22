using ShopDienThoai.ViewModels.Product;
using System.Collections.Generic;

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