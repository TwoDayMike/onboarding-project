using Application.Common.Interfaces;
using Application.Common.Security.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodo.Commands.UpdateTemplateTodoAssignee
{
    [TODOAuthorize]
    public class UpdateTemplateTodoAssigneeCommand : IRequest<Unit>
    {
        public int TodoId { get; set; }
        public class UpdateTemplateTodoAssigneeCommmandHandler : IRequestHandler<UpdateTemplateTodoAssigneeCommand, Unit>
        {
            private IApplicationDbContext _applicationDbContext;
            private IHttpContextAccessor _httpContextAccessor;
            public UpdateTemplateTodoAssigneeCommmandHandler(IApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
            {
                _applicationDbContext = applicationDbContext;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<Unit> Handle(UpdateTemplateTodoAssigneeCommand request, CancellationToken cancellationToken)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                
                if (httpContext is not null)
                {
                    var userId = Int32.Parse(httpContext.User.Claims.ToArray()[0].Value);

                    var userExists = await _applicationDbContext.Users.AnyAsync(x => x.Id == userId, cancellationToken);
                    if (!userExists)
                    {
                        throw new Exception("Not Exists user");
                    }

                    var todo = await _applicationDbContext.Todos.Where(x => x.TodoId == request.TodoId).ExecuteUpdateAsync(x => x.SetProperty(x => x.UserId, x => userId), cancellationToken);
                    await _applicationDbContext.SaveChangesAsync(cancellationToken);

                    return Unit.Value;
                }

                throw new Exception("Unable to get Http Context");
            }
        }
    }
}
