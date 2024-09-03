using MediatR;
using Application.TemplateExampleOrders.Commands.CreateTemplateExampleOrder;
using Domain.Entities;
using Application.Common.Interfaces;
using Application.Common.Security.Attributes;
using Microsoft.EntityFrameworkCore;

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

            private const string LogName = "Created Todo";

            public CreateTemplateExampleOrderCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<int> Handle(CreateTemplateTodoCommand request, CancellationToken cancellationToken)
            {
                // Detach the TodoType from the current DbContext to prevent automatic tracking
                var todoType = await _applicationDbContext.TodoTypes.FindAsync(request.TodoTypeId, cancellationToken);

                if (todoType is null)
                {
                    Console.WriteLine("SKIPPING");
                }

                var todo = new Todo
                {
                    Name = request.Name,
                    Type = todoType, // Since todoType is detached, EF Core won't track changes to it
                    Description = request.Description,
                    IsCompleted = false,
                };


                _applicationDbContext.Todos.Add(todo);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                var logEntry = new LogEntry
                {
                    Name = LogName,
                    Description = $"{todo.TodoId} was just created",
                    LogTypeId = 1

                };

                _applicationDbContext.LogEntries.Add(logEntry);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return todo.TodoId;
            }
        }
    }
}
