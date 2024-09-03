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

namespace Application.TemplateAuthentication.Commands.RegisterTemplate
{
    [TODOAuthorize]
    public class RegisterTemplateCommand : IRequest<Unit>
    {
        public string Email { get; set; } = string.Empty;

        public required string Password { get; set; } = string.Empty;

        public required string ConfirmPassword { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public class RegisterTemplateCommandHandler : IRequestHandler<RegisterTemplateCommand, Unit>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private const string DefaultRoleName = "User";

            public RegisterTemplateCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle(RegisterTemplateCommand request, CancellationToken cancellationToken)
            {
                var userWithEmailExists = await _applicationDbContext.Users.AnyAsync(x => x.Email.Equals(request.Email), cancellationToken);

                var defaultRole = await _applicationDbContext.Role.FirstOrDefaultAsync(x => x.Name == DefaultRoleName);

                if (userWithEmailExists)
                {
                    throw new Exception("User with email already exists");
                }

                if (defaultRole is null)
                {
                    throw new Exception("Default role is null");
                }

                var newUser = new User
                {
                    Email = request.Email,
                    PasswordHash = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.FirstName,
                    RoleId = defaultRole.Id
                };

                _applicationDbContext.Users.Add(newUser);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;

            }

            private bool ValidateConfirmPassword(string confirmPassword, string password)
            {
                return confirmPassword.Equals(password);
            }
        }
    }
}
