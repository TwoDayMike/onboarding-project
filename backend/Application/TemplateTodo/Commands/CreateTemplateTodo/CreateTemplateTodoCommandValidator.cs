using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodo.Commands.CreateTodo
{
    public class CreateTemplateTodoCommandValidator : AbstractValidator<CreateTemplateTodoCommand>
    {
        public CreateTemplateTodoCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

        }
    }
}
