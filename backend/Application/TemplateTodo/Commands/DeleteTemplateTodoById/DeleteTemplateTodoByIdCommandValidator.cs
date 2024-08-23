using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodo.Commands.DeleteTemplateTodoById
{
    public class DeleteTemplateTodoByIdCommandValidator : AbstractValidator<DeleteTemplateTodoByIdCommand>
    {
        public DeleteTemplateTodoByIdCommandValidator()
        {
            RuleFor(x => x.TodoId).NotNull().NotEmpty();
        }
    }
}
