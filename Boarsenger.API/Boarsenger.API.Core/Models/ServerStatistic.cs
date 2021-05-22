using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.Core.Models
{
    public class ServerStatistic : IDbEntity
    {
        public Guid Id { get; set; }

        public Guid ServerId { get; set; }

        public Server Server { get; set; }

        public DateTime Date { get; set; }

        public int JoinedUsers { get; set; }

        public float TotalIserCoefficient { get; set; }
    }
}
