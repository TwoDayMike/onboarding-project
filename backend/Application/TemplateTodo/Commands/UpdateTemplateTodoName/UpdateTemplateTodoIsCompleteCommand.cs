using Application.Common.Exceptions;
using Application.Common.Exceptions.Enums;
using Application.Common.Interfaces;
using Application.Common.Security.Attributes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodo.Commands.UpdateTemplateTodoName
{
    [TODOAuthorize]
    public class UpdateTemplateTodoIsCompleteCommand : IRequest<Unit>
    {
        public required int Id { get; set; }
        public required bool IsCompleted { get; set; }
        public class UpdateTemplateTodoIsCompleteCommandHandler : IRequestHandler<UpdateTemplateTodoIsCompleteCommand, Unit>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public UpdateTemplateTodoIsCompleteCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle(UpdateTemplateTodoIsCompleteCommand request, CancellationToken cancellationToken)
            {
                var todo = await _applicationDbContext.Todos.FirstOrDefaultAsync(x => x.TodoId == request.Id, cancellationToken);

                if (todo is null)
                    throw new CommandErrorCodeException(CommandErrorCode.TemplateExampleEntityNotFound);

                todo.IsCompleted = request.IsCompleted;
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                //if (request.TodoTypeId is int typeId)
                //{
                //    todo.TodoTypeId = typeId;
                //}

                //todo.Name = request.Name;


                return Unit.Value;
            }
        }

    }
}
