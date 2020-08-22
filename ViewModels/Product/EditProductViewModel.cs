using System.Collections.Generic;
using ShopDienThoai.Models.ProductModel;

namespace ShopDienThoai.ViewModels.Product
{
    public class EditProductViewModel : CreateProductViewModel
    {
        public string ProductId { get; set; }
        public IEnumerable<Image> ImagesFileName { get; set; }
    }
}