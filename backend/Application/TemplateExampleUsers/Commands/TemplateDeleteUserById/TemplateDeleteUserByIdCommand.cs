using Application.Common.Exceptions.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Security.Attributes;

namespace Application.TemplateExampleUsers.Commands.TemplateDeleteUserById
{
    [TODOAuthorize]
    public class TemplateDeleteUserByIdCommand : IRequest<Unit>
    {
        public required int Id { get; set; }
        public class TemplateDeleteUserByIdCommandHandler : IRequestHandler<TemplateDeleteUserByIdCommand, Unit>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public TemplateDeleteUserByIdCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle(TemplateDeleteUserByIdCommand request, CancellationToken cancellationToken)
            {
                var foundUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (foundUser is null)
                {
                    throw new CommandErrorCodeException(CommandErrorCode.TemplateExampleEntityNotFound, nameof(request.Id), "User to be deleted was not found.");
                }

                _applicationDbContext.Users.Remove(foundUser);

                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
