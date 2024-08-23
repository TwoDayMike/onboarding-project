using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodo.Commands.CreateTodo
{
    public class CreateTemplateTodoDTO
    {
        public int TodoTypeId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
