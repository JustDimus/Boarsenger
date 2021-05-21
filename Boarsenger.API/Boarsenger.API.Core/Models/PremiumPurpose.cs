using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.Core.Models
{
    public class PremiumPurpose : Purpose, IDbEntity
    {
        public DateTime PremiumTimeDuration { get; set; }

        public decimal PricePerMonth { get; set; }
    }
}
