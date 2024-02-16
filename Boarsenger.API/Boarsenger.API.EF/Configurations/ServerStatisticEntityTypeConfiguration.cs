using Boarsenger.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.EF.Configurations
{
    public class ServerStatisticEntityTypeConfiguration : IEntityTypeConfiguration<ServerStatistic>
    {
        public void Configure(EntityTypeBuilder<ServerStatistic> builder)
        {
            builder.HasKey(c => c.Id);
            builder
                .HasOne(s => s.Server)
                .WithMany(s => s.ServerInfoCollection)
                .HasForeignKey(s => s.ServerId);
        }
    }
}
