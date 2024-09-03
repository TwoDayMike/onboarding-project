using Application.Common.Exceptions.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TemplateTodoTypes.Commands.CreateTemplateTodoType;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Security.Attributes;

namespace Application.TemplateAuthentication.Commands.LoginTemplate
{
    [TODOAuthorize]
    public class LoginTemplateCommand : IRequest<string>
    {
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;

        public class LoginTemplateCommandHandler : IRequestHandler<LoginTemplateCommand, string>
        {

            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IJwtProvider _jwtProvider;

            public LoginTemplateCommandHandler(IApplicationDbContext applicationDbContext, IJwtProvider jwtProvider)
            {
                _applicationDbContext = applicationDbContext;
                _jwtProvider = jwtProvider;
            }
            public async Task<string> Handle(LoginTemplateCommand request, CancellationToken cancellationToken)
            {
                // Get Member
                var user = await _applicationDbContext.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email.Equals(request.Email), cancellationToken);

                if (user is null)
                {
                    throw new CommandErrorCodeException(CommandErrorCode.TemplateAuthenticationInvalidCredentials, nameof(request.Email), "Invalid Credentials");
                }

                // Generate JWT
                string token = _jwtProvider.Generate(user);

                // Return JWT
                return token;
            }
        }
    }
}
