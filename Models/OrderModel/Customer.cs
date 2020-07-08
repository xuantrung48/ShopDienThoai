using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheGioiDienThoai.Models.UserModel;

namespace TheGioiDienThoai.Models.OrderModel
{
    public class Customer
    {
        [Required]
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string CustomerName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
