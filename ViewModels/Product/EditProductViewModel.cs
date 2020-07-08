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
    public class EditProductViewModel : CreateProductViewModel
    {
        public string ProductId { get; set; }
        public IEnumerable<Image> ImagesFileName { get; set; }
    }
}
