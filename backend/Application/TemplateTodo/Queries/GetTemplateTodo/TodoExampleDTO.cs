using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TemplateTodo.Queries.GetTemplateTodo
{
    public class TodoExampleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? TypeName { get; set; }

        public bool IsCompleted { get; set; }


        /// <summary>
        /// Helps mapping into DTO
        /// </summary>
        /// <param name="source"></param>
        /// <returns>Returns dto type of entity.</returns>
        public static TodoExampleDTO MapToDTO(Todo source)
        {
            return new TodoExampleDTO
            {
                Description = source.Description,
                Id = source.TodoId,
                Name = source.Name,
                TypeName = source?.Type?.Name,
                IsCompleted = source.IsCompleted
            };
        }
    }
}
