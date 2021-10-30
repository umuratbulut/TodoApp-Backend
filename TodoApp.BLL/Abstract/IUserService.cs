using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Entities.Concrete;

namespace TodoApp.BLL.Abstract
{
    public interface IUserService
    {
        User Find(int id);
        List<User> GetList();
        void Add(User user);
        void Remove(User user);
        void Update(User user);

        void AddWithHash(User user);
        object Login(User user);
        bool ChangePassword(User user, string NewPassword);

    }
}
