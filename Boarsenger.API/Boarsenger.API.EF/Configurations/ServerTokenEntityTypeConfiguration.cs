using Boarsenger.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.EF.Configurations
{
    public class ServerTokenEntityTypeConfiguration : IEntityTypeConfiguration<ServerToken>
    {
        public void Configure(EntityTypeBuilder<ServerToken> builder)
        {
            builder.HasKey(c => c.ServerId);
            builder.HasOne(c => c.Server)
                .WithMany(s => s.ServerTokens)
                .HasForeignKey(c => c.ServerId);
        }
    }
}
