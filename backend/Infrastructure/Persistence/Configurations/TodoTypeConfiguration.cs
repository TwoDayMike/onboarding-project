using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class TodoTypeConfiguration : IEntityTypeConfiguration<TodoType>
    {
        public void Configure(EntityTypeBuilder<TodoType> builder)
        {
            builder.HasData(
                new TodoType { Id = 1, Name = "Software Udvikling"},
                new TodoType { Id = 2, Name = "Hjemmelige pligter" }
            );
        }
    }
}

