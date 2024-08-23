using Application.Common.Interfaces;
using Application.Common.Security.Attributes;
using Application.TemplateExampleItems.Queries.GetTemplateExampleItems;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodo.Queries.GetTemplateTodo
{
    [TODOAuthorize]
    public class GetTemplateTodoQuery : IRequest<List<TodoExampleDTO>>
    {
        public int? TodoId { get; set; }

        public class GetTemplateTodoQueryHandler : IRequestHandler<GetTemplateTodoQuery, List<TodoExampleDTO>>
        {

            private readonly IApplicationDbContext _applicationDbContext;

            public GetTemplateTodoQueryHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<List<TodoExampleDTO>> Handle(GetTemplateTodoQuery request, CancellationToken cancellationToken)
            {
                var todos = await _applicationDbContext.Todos
                    .Include(x => x.Type)
                    .Select(x => TodoExampleDTO.MapToDTO(x))
                    .ToListAsync();

                return todos;

            }
        }
    }
}
