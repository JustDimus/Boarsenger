using Boarsenger.Libraries.Telemetry.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.FluentValidation
{
    public class CreateServerValidationRules : AbstractValidator<CreateServer>
    {
        public CreateServerValidationRules()
        {
            this.RuleFor(c => c.AccountToken)
                .NotNull();
            this.RuleFor(c => c.AccountToken.Email)
                .NotEmpty();
            this.RuleFor(c => c.AccountToken.Token)
                .NotEmpty();
            this.RuleFor(c => c.ServerData)
                .NotNull();
            this.RuleFor(c => c.ServerData.Title)
                .NotEmpty();
            this.RuleFor(c => c.ServerData.ServerIP)
                .NotEmpty();
        }
    }
}
