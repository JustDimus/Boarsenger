using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.Core.Models
{
    public class AuthorizationAccount : Account, IDbEntity
    {
        public string Password { get; set; }
    }
}
