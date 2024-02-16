using Boarsenger.Libraries.Telemetry.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.FluentValidation
{
    public class ServerOwnerDataValidationRules : AbstractValidator<ServerOwnerData>
    {
        public ServerOwnerDataValidationRules()
        {
            this.RuleFor(c => c.AccountToken)
                .NotNull();
            this.RuleFor(c => c.ServerToken)
                .NotNull();
        }
    }
}
