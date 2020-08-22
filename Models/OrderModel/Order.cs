using System;
using System.ComponentModel.DataAnnotations;

namespace ShopDienThoai.Models.OrderModel
{
    public class Order
    {
        [Required] public string OrderId { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus Status { get; set; }

        [Required] public DateTime OrderTime { get; set; }

        public DateTime CompleteTime { get; set; }
        public string Note { get; set; }
    }
}