using Boarsenger.Libraries.Telemetry.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.FluentValidation
{
    public class ServerDataValidationRules : AbstractValidator<ServerData>
    {
        public ServerDataValidationRules()
        {
            this.RuleFor(c => c.Title)
                .NotEmpty();
            this.RuleFor(c => c.ServerIP)
                .NotEmpty();
        }
    }
}
