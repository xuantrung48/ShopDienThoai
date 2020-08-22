using System.ComponentModel.DataAnnotations;

namespace ShopDienThoai.ViewModels.Role
{
    public class EditRoleViewModel
    {
        public string RoleId { get; set; }

        [Required(ErrorMessage = "Nhập vào tên vai trò")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Độ dài trong khoảng từ 3 đến 20 ký tự")]
        public string Name { get; set; }
    }
}