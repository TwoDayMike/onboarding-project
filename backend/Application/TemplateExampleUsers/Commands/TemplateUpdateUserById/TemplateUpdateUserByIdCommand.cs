using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security.Attributes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TemplateExampleUsers.Commands.TemplateUpdateUserById
{
    [TODOAuthorize]
    public class TemplateUpdateUserByIdCommand : CreateUpdateTemplateUserDTO, IRequest<Unit>
    {
        public required int Id { get; set; }
        public class TemplateUpdateUserByIdCommandHandler : IRequestHandler<TemplateUpdateUserByIdCommand, Unit>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public TemplateUpdateUserByIdCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle(TemplateUpdateUserByIdCommand request, CancellationToken cancellationToken)
            {
                var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)  ?? throw new CommandErrorCodeException(Common.Exceptions.Enums.CommandErrorCode.TemplateExampleEntityNotFound);

                if (!request.Email.Equals(user.Email, StringComparison.CurrentCultureIgnoreCase))
                {
                    var emailInUse = await _applicationDbContext.Users.AnyAsync(x => request.Email.ToLower() == x.Email.ToLower(), cancellationToken);

                    if (emailInUse)
                    {
                        throw new CommandErrorCodeException(Common.Exceptions.Enums.CommandErrorCode.TemplateExampleEmailInUse, nameof(request.Email), $"{request.Email} is already in use");
                    }

                    user.Email = request.Email;
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;

                _applicationDbContext.Users.Update(user);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
