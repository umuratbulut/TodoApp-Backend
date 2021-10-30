using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Entities.Concrete;

namespace TodoApp.BLL.ValidationRules.FluentValidation
{
    public class TodoValidator:AbstractValidator<Todo>
    {
        public TodoValidator()
        {
            RuleFor(x => x.UserID).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();

        }
    }
}
