using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.Core.Models
{
    public class ServerToken : IDbEntity
    {
        public Server Server { get; set; }

        public Guid ServerId { get; set; }

        public string Token { get; set; }
    }
}
