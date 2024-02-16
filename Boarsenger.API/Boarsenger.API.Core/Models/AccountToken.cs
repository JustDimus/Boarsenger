using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.Core.Models
{
    public class AccountToken : IDbEntity
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Account Account { get; set; }

        public string Token { get; set; }

        public DateTime TokenDate { get; set; }
    }
}
