using Application.Common.Interfaces;
using Application.Common.Security.Attributes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodoTypes.Queries.GetTemplateTodoTypes
{
    [TODOAuthorize]
    public class GetTemplateTodoTypeQuery : IRequest<List<TodoTypeExampleDTO>>
    {

        public class GetTemplateTodoTypeQueryHandler : IRequestHandler<GetTemplateTodoTypeQuery, List<TodoTypeExampleDTO>>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public GetTemplateTodoTypeQueryHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<List<TodoTypeExampleDTO>> Handle(GetTemplateTodoTypeQuery request, CancellationToken cancellationToken)
            {
                var todoTypes = await _applicationDbContext.TodoTypes
                    .Select(t => TodoTypeExampleDTO.MapToDTO(t))
                    .ToListAsync();

                return todoTypes;
            }
        }
    }
}
