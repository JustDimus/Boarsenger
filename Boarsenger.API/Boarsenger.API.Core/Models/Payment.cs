using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.Core.Models
{
    public class Payment : IDbEntity
    {
        public Guid ID { get; set; }

        public Account Account { get; set; }

        public PaymentServiceInfo PaymentServiceInfo { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public Purpose Purpose { get; set; }
    }
}
