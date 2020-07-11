using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TheGioiDienThoai.Models.UserModel;

namespace TheGioiDienThoai.Models
{
    public class AppSetting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Logo { get; set; }
        public string Icon { get; set; }
        [Required(ErrorMessage = "Chọn vai trò mặc định cho người dùng!")]
        public string DefaultRoleId { get; set; }
    }
}
