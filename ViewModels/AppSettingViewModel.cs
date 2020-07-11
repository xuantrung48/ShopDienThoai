using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TheGioiDienThoai.Models.UserModel;
using TheGioiDienThoai.Models.Validation;

namespace TheGioiDienThoai.ViewModels
{
    public class AppSettingViewModel
    {
        [Required(ErrorMessage = "Nhập vào tên trang web!")]
        [StringLength(30, MinimumLength = 5)]
        public string Title { get; set; }
        [StringLength(100, MinimumLength = 5)]
        public string ShortDesc { get; set; }
        public string Logo { get; set; }
        public string Icon { get; set; }
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [MaxFileSize(1 * 1024 * 1024)]
        public IFormFile LogoFile { get; set; }
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [MaxFileSize(1 * 1024 * 1024)]
        public IFormFile IconFile { get; set; }
        [Required(ErrorMessage = "Chọn vai trò mặc định cho người dùng!")]
        public string DefaultRoleId { get; set; }
    }
}
