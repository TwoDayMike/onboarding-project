using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(x => new { x.TodoId, x.TodoTypeId});
            builder.HasOne(x => x.Type).WithMany(x => x.TodoTypes).HasForeignKey(x => x.TodoTypeId).IsRequired(true);
        }
    }
}
