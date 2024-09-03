using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security.Attributes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateExampleUsers.Queries.TemplateGetAllUsers
{
    [TODOAuthorize]
    public class TemplateGetAllUsersQuery : IRequest<List<TemplateUserDTO>>
    {
        public class TemplateGetAllUsersQueryHandler : IRequestHandler<TemplateGetAllUsersQuery, List<TemplateUserDTO>>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public TemplateGetAllUsersQueryHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<List<TemplateUserDTO>> Handle(TemplateGetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var users = await _applicationDbContext.Users.Include(x => x.Role).Select(x => TemplateUserDTO.MapToDTO(x)).ToListAsync(cancellationToken: cancellationToken);
                return users;
            }
        }
    }
}
