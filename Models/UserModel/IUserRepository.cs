using System.Collections.Generic;

namespace ShopDienThoai.Models.UserModel
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();

        User Get(string id);

        User Create(User product);

        User Edit(User product);

        bool Remove(string id);
    }
}