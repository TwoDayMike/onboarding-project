using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Todo
    {
        public int TodoId { get; set; }

        public required string Name { get; set; } = string.Empty;

        public required string Description { get; set; } = string.Empty;

        public int TodoTypeId { get; set; }

        public TodoType? Type { get; set; }
    }
}
