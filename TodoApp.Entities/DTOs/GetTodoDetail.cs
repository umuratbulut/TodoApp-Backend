using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Entities.DTOs
{
    public class GetTodoDetail:IDto
    {
        public int TodoID { get; set; }
        public int TodoUserID { get; set; }
        public string UserName { get; set; }
        public string UserNickname { get; set; }
        public string TodoDescription { get; set; }
        public string TodoDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool TodoStatus { get; set; }
    }
}
