using MediatR;
using Application.TemplateExampleOrders.Commands.CreateTemplateExampleOrder;
using Domain.Entities;
using Application.Common.Interfaces;
using Application.Common.Security.Attributes;

namespace Application.TemplateTodo.Commands.CreateTodo
{
    [TODOAuthorize]
    public class CreateTemplateTodoCommand : IRequest<int>
    {
        public int TodoTypeId { get; set; }
        public required string Name { get; set; } = string.Empty;

        public required string Description { get; set; } = string.Empty;
        public class CreateTemplateExampleOrderCommandHandler : IRequestHandler<CreateTemplateTodoCommand, int>
        {

            private readonly IApplicationDbContext _applicationDbContext;

            public CreateTemplateExampleOrderCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<int> Handle(CreateTemplateTodoCommand request, CancellationToken cancellationToken)
            {
                var todo = new Todo
                {
                    Name = request.Name,
                    TodoTypeId = request.TodoTypeId,
                    Description = request.Description,
                };

                _applicationDbContext.Todos.Add(todo);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return todo.TodoId;
            }
        }
    }
}
