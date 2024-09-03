using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateExampleLog.Commands.CreateTemplateExampleLog
{
    public class CreateTemplateExampleLogCommand : IRequest<int>
    {
        public required string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public class CreateTemplateExamleLogCommandHandler : IRequestHandler<CreateTemplateExampleLogCommand, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public CreateTemplateExamleLogCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }
            public async Task<int> Handle(CreateTemplateExampleLogCommand request, CancellationToken cancellationToken)
            {
                return 1;
            }
        }
    }
}
