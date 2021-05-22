using Boarsenger.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.EF.Configurations
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);
            builder
                .HasMany(a => a.OwnedServers)
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId);
        }
    }
}
