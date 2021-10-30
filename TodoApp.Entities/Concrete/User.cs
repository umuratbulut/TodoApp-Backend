using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Entities.Concrete
{
   public class User:IEntity
    {
        public User()
        {
            Todos = new List<Todo>();
            RegistrationDate = DateTime.Now;
            isActive = true;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool isActive { get; set; }
        public virtual List<Todo> Todos { get; set; }

    }
}
