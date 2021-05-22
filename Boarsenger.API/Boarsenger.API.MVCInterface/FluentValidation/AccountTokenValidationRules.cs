using Boarsenger.Libraries.Telemetry.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.FluentValidation
{
    public class AccountTokenValidationRules : AbstractValidator<AccountToken>
    {
        public AccountTokenValidationRules()
        {
            this.RuleFor(at => at.Email)
                .NotEmpty();

            this.RuleFor(at => at.Token)
                .NotEmpty();
        }
    }
}
