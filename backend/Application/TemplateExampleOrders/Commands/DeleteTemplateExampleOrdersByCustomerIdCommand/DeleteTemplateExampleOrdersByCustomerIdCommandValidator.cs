using FluentValidation;

namespace Application.TemplateExampleOrders.Commands.DeleteTemplateExampleOrdersByCustomerIdCommand
{

    public class DeleteTemplateExampleOrdersByCustomerIdCommandValidator : AbstractValidator<DeleteTemplateExampleOrdersByCustomerIdCommand>
    {
        public DeleteTemplateExampleOrdersByCustomerIdCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotNull();
        }
    }
}
