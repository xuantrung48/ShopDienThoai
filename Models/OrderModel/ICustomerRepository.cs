using System.Collections.Generic;

namespace ShopDienThoai.Models.OrderModel
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();

        Customer Get(string id);

        Customer Create(Customer customer);

        Customer Edit(Customer customer);

        bool Remove(string id);
    }
}