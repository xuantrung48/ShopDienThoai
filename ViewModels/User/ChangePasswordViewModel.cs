using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheGioiDienThoai.ViewModels.User
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Nhập vào mật khẩu cũ!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Nhập vào mật khẩu mới!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu không khớp!")]
        public string ConfirmNewPassword { get; set; }
    }
}
