using System.Collections.Generic;

namespace ShopDienThoai.Models.OrderModel
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Get();
        Order Get(string id);
        Order Create(Order order);
        Order Edit(Order order);
        bool Remove(string id);
    }
}