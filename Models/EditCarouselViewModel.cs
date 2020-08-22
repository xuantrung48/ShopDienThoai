using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ShopDienThoai.Models
{
    public class EditCarouselViewModel
    {
        public IEnumerable<IFormFile> ImageFiles { get; set; }
    }
}