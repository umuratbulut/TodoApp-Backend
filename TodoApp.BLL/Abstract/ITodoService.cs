using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Entities.Concrete;
using TodoApp.Entities.DTOs;

namespace TodoApp.BLL.Abstract
{
    public interface ITodoService
    {
        Todo Find(int id);
        List<Todo> GetList();
        void Add(Todo todo);
        void Remove(Todo todo);
        void Update(Todo todo);

        void AddWithSendMail(Todo todo);
        void UpdateWithSendMail(Todo todo);
        List<GetTodoDetail> GetTodoDetails();
        void SetStatus(int id);
    }
}
