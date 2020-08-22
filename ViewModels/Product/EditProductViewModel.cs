using ShopDienThoai.Models.ProductModel;
using System.Collections.Generic;

namespace ShopDienThoai.ViewModels.Product
{
    public class EditProductViewModel : CreateProductViewModel
    {
        public string ProductId { get; set; }
        public IEnumerable<Image> ImagesFileName { get; set; }
    }
}