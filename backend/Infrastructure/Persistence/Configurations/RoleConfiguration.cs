﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.HasMany(x => x.Users).WithOne(x => x.Role);
            builder.HasData(new Role
            {
                Id = 1,
                Name = "Admin"
            }, new Role
            {
                Id = 2,
                Name = "User"
            });
        }
    }
}
