using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodoTypes.Queries.GetTemplateTodoTypes
{
    public class TodoTypeExampleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;


        public static TodoTypeExampleDTO MapToDTO(TodoType source)
        {
            return new TodoTypeExampleDTO
            {
                Id = source.Id,
                Name = source.Name,
            };
        }
    }
}
