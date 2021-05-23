using Boarsenger.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.EF.Configurations
{
    public class ServerEntityTypeConfiguration : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder.HasKey(s => s.Id);
            builder
                .HasOne(s => s.Owner)
                .WithMany(s => s.OwnedServers)
                .HasForeignKey(s => s.OwnerId);
            builder
                .HasMany(s => s.ServerTokens)
                .WithOne(c => c.Server)
                .HasForeignKey(c => c.ServerId);
        }
    }
}
