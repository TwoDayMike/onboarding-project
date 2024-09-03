using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(x => new { x.TodoTypeId, x.TodoId });
            builder.HasOne(x => x.Type).WithMany(x => x.TodoTypes).HasForeignKey(x => x.TodoTypeId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany(x => x.Todos).HasForeignKey(x => x.UserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            builder.HasData(
                new Todo { TodoId = 1, Description = "Hente vasketøj og gøre rent", IsCompleted = false, Name = "Husholdning", TodoTypeId = 2 },
                new Todo { TodoId = 2, Description = "Implementere en controller som lave en integration til IMDB", IsCompleted = false, Name = "Integration", TodoTypeId = 1 }
            );
        }
    }
}
