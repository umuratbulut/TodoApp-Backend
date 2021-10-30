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
    public class UsersController : ControllerBase
    {
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        private IUserService _userService;

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetList()
        {

            var result = _userService.GetList();
            return Ok(result);
        }
        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Find(int id)
        {
            return Ok(_userService.Find(id));
        }
        /// <summary>
        /// Create a User
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(User user)
        {

            _userService.AddWithHash(user);
            return Ok();
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public object Login(User user)
        {
            if (!_userService.Login(user).Equals(false))
            {
                return Ok(_userService.Login(user));
            }


            return false;
        }
        /// <summary>
        /// Update a User
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(User user)
        {
            _userService.Update(user);
            return Ok();
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <returns></returns>
        [HttpPost("{newPassword}")]
        public IActionResult ChancePassword(User user, string newPassword)
        {
            if (_userService.ChangePassword(user, newPassword))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
