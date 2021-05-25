using Boarsenger.API.EF.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.EF
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions dbOptions)
            : base(dbOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountEntityTypeConfiguration).Assembly);
        }
    }
}
