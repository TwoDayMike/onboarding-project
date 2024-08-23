using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodoTypes.Commands.CreateTemplateTodoType
{
    public class CreateTemplateTodoTypeCommandValidator : AbstractValidator<CreateTemplateTodoTypeCommand>
    {
        public CreateTemplateTodoTypeCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
