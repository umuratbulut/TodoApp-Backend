using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Entities.Concrete;

namespace TodoApp.DAL.Abstract
{
    public interface IUserDal : IRepository<User>
    {
        void AddWithHash(User user);
        object Login(User user);
        bool ChangePassword(User user,string NewPassword);
    }
}
