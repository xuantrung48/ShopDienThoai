using System.Collections.Generic;

namespace ShopDienThoai.Models.OrderModel
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> Get();

        IEnumerable<OrderDetail> Get(string orderId);

        OrderDetail Create(OrderDetail orderDetail);

        OrderDetail Edit(OrderDetail orderDetail);

        bool Remove(string orderId);
    }
}