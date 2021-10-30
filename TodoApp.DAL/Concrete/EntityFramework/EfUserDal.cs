using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DAL.Abstract;
using TodoApp.Entities.Concrete;

namespace TodoApp.DAL.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, TodoAppContext>, IUserDal
    {
        public void AddWithHash(User user)
        {
            using (TodoAppContext context = new TodoAppContext())
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var encrypedPassword = crypto.Compute(user.Password);
                user.Password = encrypedPassword;
                user.PasswordSalt = crypto.Salt;

                var entity = context.Entry(user);
                entity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public bool ChangePassword(User user, string NewPassword)
        {
            using (TodoAppContext context=new TodoAppContext())
            {
                var _user = context.Users.Find(user.ID);
                var crypto = new SimpleCrypto.PBKDF2();

                if (_user.Password.Equals(crypto.Compute(user.Password, _user.PasswordSalt)))
                {
                    var encrypedPassword = crypto.Compute(NewPassword);
                    _user.Password = encrypedPassword;
                    _user.PasswordSalt = crypto.Salt;
                    var entity = context.Entry(_user);
                    entity.State = EntityState.Modified;
                    context.SaveChanges();

                    return true;
                }
                else {
                    return false;
                }
            }
        }

        public object Login(User user)
        {
            using (TodoAppContext context = new TodoAppContext())
            {
                var crypto = new SimpleCrypto.PBKDF2();
                var _user = context.Users.FirstOrDefault(x=>x.Email.Equals(user.Email));
                if (_user!=null && _user.isActive.Equals(true))
                {
                    if (_user.Email.Equals(user.Email) && _user.Password.Equals(crypto.Compute(user.Password, _user.PasswordSalt)))
                    {
                        return _user;
                    }
                }
                
            }
            return false;

        }
    }
}
