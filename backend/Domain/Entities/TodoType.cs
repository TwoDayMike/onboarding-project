using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TodoType
    {
        public int Id { get; set; }

        public required string Name { get; set; } = string.Empty;

        public ICollection<Todo>? TodoTypes { get; set; }
    }
}
