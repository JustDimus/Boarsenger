using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.Core.Models
{
    public class ServerExpandPurpose :  Purpose, IDbEntity
    {
        public int AdditionalPlaces { get; set; }

        public decimal PricePerPlace { get; set; }
    }
}
