using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security.Attributes;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodoTypes.Commands.CreateTemplateTodoType
{
    [TODOAuthorize]
    public class CreateTemplateTodoTypeCommand : IRequest<int>
    {
        public required string Name { get; set; }


        public class CreateTemplateTodoTypeCommandHandler : IRequestHandler<CreateTemplateTodoTypeCommand, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            public CreateTemplateTodoTypeCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<int> Handle(CreateTemplateTodoTypeCommand request, CancellationToken cancellationToken)
            {
                var nameExists = await _applicationDbContext.TodoTypes.AnyAsync(t => t.Name == request.Name, cancellationToken);

                if (nameExists)
                {
                    throw new CommandErrorCodeException(Common.Exceptions.Enums.CommandErrorCode.TemplateTodoTypeNameInUse, $"{request.Name} already exists in database.");
                }

                var todoType = new TodoType
                {
                    Name = request.Name
                };

                _applicationDbContext.TodoTypes.Add(todoType);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return 0;
            }
        }
    }
}
