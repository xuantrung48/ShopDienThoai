using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheGioiDienThoai.Models.ProductModel;

namespace TheGioiDienThoai.ViewModels.Product
{
    public class ProductDetailViewModel
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int? Remain { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Image> Images { get; set; }
        public string Description { get; set; }
        public string Screen { get; set; }
        public string CPU { get; set; }
        public string OS { get; set; }
        public string RearCamera { get; set; }
        public string FrontCamera { get; set; }
        public int? Ram { get; set; }
        public int? Rom { get; set; }
    }
}
