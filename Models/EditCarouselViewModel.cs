using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ShopDienThoai.Models
{
    public class EditCarouselViewModel
    {
        public IEnumerable<IFormFile> ImageFiles { get; set; }
    }
}