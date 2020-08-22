using System.ComponentModel.DataAnnotations;

namespace ShopDienThoai.Models
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