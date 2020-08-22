using ShopDienThoai.Models.UserModel;
using System.ComponentModel.DataAnnotations;

namespace ShopDienThoai.Models.OrderModel
{
    public class Customer
    {
        [Required] public string CustomerId { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public string CustomerName { get; set; }

        [Required] public string PhoneNumber { get; set; }

        [Required] public string Address { get; set; }
    }
}