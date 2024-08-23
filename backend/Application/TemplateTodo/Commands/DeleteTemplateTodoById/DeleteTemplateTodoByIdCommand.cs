using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security.Attributes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TemplateTodo.Commands.DeleteTemplateTodoById
{
    [TODOAuthorize]
    public class DeleteTemplateTodoByIdCommand : IRequest<Unit>
    {
        public required int TodoId {  get; set; }

        public class DeleteTemplateTodoByIdCommandHandler : IRequestHandler<DeleteTemplateTodoByIdCommand, Unit>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public DeleteTemplateTodoByIdCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;    
            }

            public async Task<Unit> Handle(DeleteTemplateTodoByIdCommand request, CancellationToken cancellationToken)
            {
                var foundTodo = await _applicationDbContext.Todos.FirstOrDefaultAsync(x => x.TodoId == request.TodoId, cancellationToken);

                if (foundTodo is null)
                {
                    throw new CommandErrorCodeException(Common.Exceptions.Enums.CommandErrorCode.TemplateExampleEntityNotFound, nameof(request.TodoId), $"{request.TodoId} was not found in database");
                }

                _applicationDbContext.Todos.Remove(foundTodo);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
