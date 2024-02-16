using Boarsenger.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.EF.Configurations
{
    public class AccountTokenEntityTypeConfiguration : IEntityTypeConfiguration<AccountToken>
    {
        public void Configure(EntityTypeBuilder<AccountToken> builder)
        {
            builder.HasKey(t => t.Id);
            builder
                .HasOne(t => t.Account)
                .WithMany(a => a.Tokens)
                .HasForeignKey(t => t.AccountId);
                
        }
    }
}
