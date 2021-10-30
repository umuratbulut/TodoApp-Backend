using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.BLL.Abstract;
using TodoApp.Entities.Concrete;

namespace TodoApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        
        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        private ITodoService _todoService;

        /// <summary>
        /// Get All Todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetList()
        {

            var result = _todoService.GetTodoDetails();
            return Ok(result);
        }

        /// <summary>
        /// Get Todo By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _todoService.Find(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create a Todo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(Todo todo)
        {
            _todoService.AddWithSendMail(todo);
            return Ok();
        }
        /// <summary>
        /// Update Todo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(Todo todo)
        {
            _todoService.UpdateWithSendMail(todo);
            return Ok();
        }

        /// <summary>
        /// Delete Todo
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(Todo todo)
        {
            _todoService.Remove(todo);
            return Ok();
        }

        /// <summary>
        /// Change Todo Status By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public IActionResult SetStatus(int id)
        {
            _todoService.SetStatus(id);
            return Ok();
        }

    }
}
