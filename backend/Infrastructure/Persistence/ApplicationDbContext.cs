using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        private readonly IEncryptionService _encryptionService;

        public ApplicationDbContext(DbContextOptions options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor, IEncryptionService encryptionService) : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
            _encryptionService = encryptionService;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return BeginTransaction();
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.UseEncryption(_encryptionService);

            base.OnModelCreating(builder);

            #region Cascading Todos on TodoType Deletion

            

            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }


        public DbSet<TemplateExampleCustomer> TemplateExampleCustomers { get; set; }
        public DbSet<TemplateExampleItem> TemplateExampleItems { get; set; }
        public DbSet<TemplateExampleOrderItem> TemplateExampleOrderItems { get; set; }
        public DbSet<TemplateExampleOrder> TemplateExampleOrders { get; set; }

        public DbSet<Todo> Todos { get; set; }

        public DbSet<TodoType> TodoTypes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<LogEntry> LogEntries { get; set; }

        public DbSet<LogType> LogTypes { get; set; }

        public DbSet<Role> Role { get; set; }

    }
}
