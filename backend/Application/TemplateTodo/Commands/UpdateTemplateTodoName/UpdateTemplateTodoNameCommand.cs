using Application.Common.Exceptions;
using Application.Common.Exceptions.Enums;
using Application.Common.Interfaces;
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
    public class UpdateTemplateTodoNameCommand : IRequest<Unit>
    {
        public required int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? TodoTypeId { get; set; }
        public class UpdateTemplateTodoCommandHandler : IRequestHandler<UpdateTemplateTodoNameCommand, Unit>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public UpdateTemplateTodoCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle(UpdateTemplateTodoNameCommand request, CancellationToken cancellationToken)
            {
                var todo = await _applicationDbContext.Todos.FirstOrDefaultAsync(x => x.TodoId == request.Id, cancellationToken);

                if (todo is null)
                    throw new CommandErrorCodeException(CommandErrorCode.TemplateExampleEntityNotFound);

                if (request.TodoTypeId is int typeId)
                {
                    todo.TodoTypeId = typeId;
                }

                todo.Name = request.Name;

                return Unit.Value;
            }
        }

    }
}
