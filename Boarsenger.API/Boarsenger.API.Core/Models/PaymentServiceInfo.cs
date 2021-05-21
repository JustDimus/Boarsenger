using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.Core.Models
{
    public class PaymentServiceInfo : IDbEntity
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }
    }
}
