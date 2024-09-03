using Application.Common.Interfaces;
using Application.Common.Security.Attributes;
using Application.TemplateTodo.Queries.GetTemplateTodo;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodo.Queries.GetTemplateTodoByUserId
{
    [TODOAuthorize]
    public class GetTemplateTodoByUserIdQuery : IRequest<List<TodoExampleDTO>>
    {
        public int UserId { get; set; }
        public class GetTemplateTodoByUserIdQueryHandler : IRequestHandler<GetTemplateTodoByUserIdQuery, List<TodoExampleDTO>>
        {
            private IApplicationDbContext _applicationDbContext;
            public GetTemplateTodoByUserIdQueryHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<List<TodoExampleDTO>> Handle(GetTemplateTodoByUserIdQuery request, CancellationToken cancellationToken)
            {
                var todos = await _applicationDbContext.Todos.Where(x => x.UserId == request.UserId).Select(x => TodoExampleDTO.MapToDTO(x)).ToListAsync();

                return todos;
            }
        }
    }
}
