using Boarsenger.Libraries.Telemetry.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.FluentValidation
{
    public class CreateServerValidationRules : AbstractValidator<CreateServerData>
    {
        public CreateServerValidationRules()
        {
            this.RuleFor(c => c.ServerOwnerData)
                .NotNull();
            this.RuleFor(c => c.ServerData)
                .NotNull();
        }
    }
}
