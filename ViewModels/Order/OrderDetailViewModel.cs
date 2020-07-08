using System;
using System.Collections.Generic;
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
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
    }
}
