using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheGioiDienThoai.Models.UserModel
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }
        public User Create(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public User Edit(User user)
        {
            var editUser = context.Users.Attach(user);
            editUser.State = EntityState.Modified;
            context.SaveChanges();
            return user;
        }

        public IEnumerable<User> Get()
        {
            return context.Users;
        }

        public User Get(string id)
        {
            return context.Users.Find(id);
        }

        public bool Remove(string id)
        {
            var userToRemove = context.Users.Find(id);
            if (userToRemove != null)
            {
                context.Users.Remove(userToRemove);
                return context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
