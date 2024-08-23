using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TemplateTodo.Commands.DeleteTemplateTodoById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodoTypes.Commands.DeleteTemplateTodoTypeById
{
    public class DeleteTemplateTodoTypeByIdCommand : IRequest<Unit>
    {
        public required int TodoTypeId { get; set; }

        public class DeleteTemplateTodoTypeByIdCommandHandler : IRequestHandler<DeleteTemplateTodoTypeByIdCommand, Unit>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public DeleteTemplateTodoTypeByIdCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<Unit> Handle(DeleteTemplateTodoTypeByIdCommand request, CancellationToken cancellationToken)
            {
                var foundTodoType = await _applicationDbContext.TodoTypes.FirstOrDefaultAsync(x => x.Id == request.TodoTypeId, cancellationToken);

                if (foundTodoType is null)
                {
                    throw new CommandErrorCodeException(Common.Exceptions.Enums.CommandErrorCode.TemplateExampleEntityNotFound, nameof(request.TodoTypeId), $"Todo Type with {request.TodoTypeId} doesn't exist.");
                }

                _applicationDbContext.TodoTypes.Remove(foundTodoType);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
