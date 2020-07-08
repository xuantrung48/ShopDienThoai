using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheGioiDienThoai.Models.OrderModel;

namespace TheGioiDienThoai.ViewModels.Order
{
    public class OrderDetailViewModel
    {
        public string OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime CompleteTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        [Required(ErrorMessage = "Nhập vào tên người nhận hàng!")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Nhập vào địa chỉ giao hàng!")]
        public string CustomerAddress { get; set; }
        [Required(ErrorMessage = "Nhập vào số điện thoại người nhận hàng!")]
        [RegularExpression(@"^\(?(0|[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string CustomerPhoneNumber { get; set; }
    }
}
