using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateAuthentication.Commands.RegisterTemplate
{
    public class RegisterTemplateValidator : AbstractValidator<RegisterTemplateCommand>
    {
        public RegisterTemplateValidator() {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
        }
    }
}
