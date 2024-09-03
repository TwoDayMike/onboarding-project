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
    public class LogEntryConfiguration : IEntityTypeConfiguration<LogEntry>
    {
        public void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            builder.HasKey(x => new { x.Id, x.LogTypeId});
            builder.HasOne(x => x.LogType).WithMany(x => x.LogEntries).HasForeignKey(x => x.LogTypeId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
