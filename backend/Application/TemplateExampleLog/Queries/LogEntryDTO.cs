using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateExampleLog.Queries
{
    public class LogEntryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public string OperationName { get; set; }
        public static LogEntryDTO MapDTO(LogEntry source)
        {
            return new LogEntryDTO
            {
                Created = source.Created.Date,
                Name = source.Name,
                Description = source.Description,
                Id = source.Id,
                OperationName = source?.LogType.Name
            };
        }
    }
}
