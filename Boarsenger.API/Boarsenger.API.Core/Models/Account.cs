using Boarsenger.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.Core.Models
{
    public class Account : IDbEntity
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        //public IEnumerable<Payment> Payments { get; set; }

        public IEnumerable<Server> OwnedServers { get; set; }

        //public IEnumerable<Server> ListOfServers { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int Age { get; set; }
    }
}
