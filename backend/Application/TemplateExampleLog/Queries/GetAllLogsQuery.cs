using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateExampleLog.Queries
{
    public class GetAllLogsQuery : IRequest<List<LogEntryDTO>>
    {
        public class GetAllLogsQueryHandler : IRequestHandler<GetAllLogsQuery, List<LogEntryDTO>>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public GetAllLogsQueryHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<List<LogEntryDTO>> Handle(GetAllLogsQuery request, CancellationToken cancellationToken)
            {
                var logs = await _applicationDbContext.LogEntries.Include(x => x.LogType).Select(x => LogEntryDTO.MapDTO(x)).ToListAsync();
                return logs;
            }
        }
    }
}
