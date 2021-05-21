using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.Core.Models
{
    public class Server : IDbEntity
    {
        public Guid ID { get; set; }

        public Account Owner { get; set; }

        public string Title { get; set; }

        public bool IsPublished { get; set; }

        public bool IsBanned { get; set; }

        public string IP { get; set; }

        public IEnumerable<ServerStatistic> ServerInfoCollection { get; set; }

        public IEnumerable<AuthorizationAccount> JoinedUsers { get; set; }
    }
}
