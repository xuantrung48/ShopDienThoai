using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheGioiDienThoai.Models
{
    public class EditCarouselViewModel
    {
        public IEnumerable<IFormFile> ImageFiles { get; set; }
    }
}
