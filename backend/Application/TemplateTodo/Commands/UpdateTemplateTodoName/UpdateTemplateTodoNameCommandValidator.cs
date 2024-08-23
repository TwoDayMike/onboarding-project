using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodo.Commands.UpdateTemplateTodoName
{
    public class UpdateTemplateTodoNameCommandValidator : AbstractValidator<UpdateTemplateTodoIsCompleteCommand>
    {
        public UpdateTemplateTodoNameCommandValidator() 
        {
            RuleFor(x => x.IsCompleted).NotNull();
        }
    }
}
