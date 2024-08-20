using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodo.Commands.UpdateTemplateTodoName
{
    public class UpdateTemplateTodoNameCommandValidator : AbstractValidator<UpdateTemplateTodoNameCommand>
    {
        public UpdateTemplateTodoNameCommandValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
