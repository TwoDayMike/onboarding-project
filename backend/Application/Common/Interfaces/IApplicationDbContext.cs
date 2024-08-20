﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IDbContextTransaction BeginTransaction();

        DbSet<TemplateExampleCustomer> TemplateExampleCustomers { get; set; }
        DbSet<TemplateExampleItem> TemplateExampleItems { get; set; }
        DbSet<TemplateExampleOrder> TemplateExampleOrders { get; set; }

    }
}
