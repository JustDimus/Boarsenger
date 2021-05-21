using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.Core.Models
{
    public class Purpose : IDbEntity
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public decimal TotalPrice()
        {
            return 0;
        }
    }
}
