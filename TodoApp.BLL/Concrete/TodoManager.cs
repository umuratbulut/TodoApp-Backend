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
using TodoApp.Entities.DTOs;

namespace TodoApp.BLL.Concrete
{
    public class TodoManager : ITodoService
    {
        private ITodoDal _todoDal;
        public TodoManager(ITodoDal todoDal)
        {
            _todoDal = todoDal;
        }
        [ValidationAspect(typeof(TodoValidator))]
        public void Add(Todo todo)
        {
            _todoDal.Add(todo);
        }
        [ValidationAspect(typeof(TodoValidator))]
        public void AddWithSendMail(Todo todo)
        {
            _todoDal.AddWithSendMail(todo);
        }

        public Todo Find(int id)
        {
            return _todoDal.Find(x => x.ID.Equals(id));
        }

        public List<Todo> GetList()
        {
            return _todoDal.GetList();
        }

        public List<GetTodoDetail> GetTodoDetails()
        {
            return _todoDal.GetTodoDetail().OrderByDescending(x => x.TodoID).ToList();
        }

        public void Remove(Todo todo)
        {
            _todoDal.Remove(todo);
        }

        public void SetStatus(int id)
        {
            _todoDal.SetStatus(id);
        }

        [ValidationAspect(typeof(TodoValidator))]
        public void Update(Todo todo)
        {
            _todoDal.Update(todo);
        }

        [ValidationAspect(typeof(TodoValidator))]
        public void UpdateWithSendMail(Todo todo)
        {
            _todoDal.UpdateWithSendMail(todo);
        }
    }
}
