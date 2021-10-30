using Core.Aspects.Autofac.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.BLL.Abstract;
using TodoApp.BLL.ValidationRules.FluentValidation;
using TodoApp.DAL.Abstract;
using TodoApp.Entities.Concrete;

namespace TodoApp.BLL.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        [ValidationAspect(typeof(UserValidator))]
        public void Add(User user)
        {
            _userDal.Add(user);
        }
        [ValidationAspect(typeof(UserValidator))]
        public void AddWithHash(User user)
        {
            _userDal.AddWithHash(user);
        }

        public bool ChangePassword(User user, string NewPassword)
        {
           return _userDal.ChangePassword(user , NewPassword);
        }

        public User Find(int id)
        {
            return _userDal.Find(x => x.ID.Equals(id));
        }

        public List<User> GetList()
        {
            return _userDal.GetList().OrderByDescending(x=>x.ID).ToList();
        }

        public object Login(User user)
        {
            return _userDal.Login(user);
        }

        public void Remove(User user)
        {
            _userDal.Remove(user);
        }
        [ValidationAspect(typeof(UserValidator))]
        public void Update(User user)
        {
            _userDal.Update(user);
        }
    }
}
