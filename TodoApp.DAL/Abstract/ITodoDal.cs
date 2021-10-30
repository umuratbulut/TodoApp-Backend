using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Entities.Concrete;
using TodoApp.Entities.DTOs;

namespace TodoApp.DAL.Abstract
{
    public interface ITodoDal:IRepository<Todo>
    {
        void AddWithSendMail(Todo todo);
        void UpdateWithSendMail(Todo todo);
        List<GetTodoDetail> GetTodoDetail();
        void SetStatus(int id);
    }
}
