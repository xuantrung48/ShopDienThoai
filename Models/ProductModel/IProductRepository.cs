using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheGioiDienThoai.ViewModels.Product;

namespace TheGioiDienThoai.Models.ProductModel
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
