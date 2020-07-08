using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheGioiDienThoai.Models.ProductModel;
using TheGioiDienThoai.Models.Validation;

namespace TheGioiDienThoai.ViewModels.Product
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Chưa nhập tên sản phẩm!")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Chưa nhập giá!")]
        [Range(1, int.MaxValue, ErrorMessage = "Nhập sai giá!")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Chưa chọn thương hiệu!")]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Chưa nhập vào số lượng trong kho!")]
        [Range(1, int.MaxValue, ErrorMessage = "Nhập sai số lượng!")]
        public int? Remain { get; set; }
        [Required(ErrorMessage = "Chưa chọn loại sản phẩm!")]
        public int CategoryId { get; set; }
        public IEnumerable<IFormFile> ImageFiles { get; set; }
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
