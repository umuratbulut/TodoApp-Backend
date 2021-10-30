using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Entities.Concrete
{
   public class Todo:IEntity
    {
        public Todo()
        {
           CreationDate = DateTime.Now;
            isActive = true;
        }
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool isActive { get; set; }
        public virtual User User { get; set; }
    }
}
