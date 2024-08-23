using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Todo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TodoId { get; set; }

        public required string Name { get; set; } = string.Empty;

        public required string Description { get; set; } = string.Empty;

        public required bool IsCompleted { get; set; } 

        public int TodoTypeId { get; set; }

        public TodoType? Type { get; set; }

        public int? UserId { get; set; }

        public User? User { get; set; }
    }
}
