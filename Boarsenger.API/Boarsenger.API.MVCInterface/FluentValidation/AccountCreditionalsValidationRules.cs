using Boarsenger.Libraries.Telemetry.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.FluentValidation
{
    public class AccountCreditionalsValidationRules : AbstractValidator<AccountCreditionals>
    {
        public AccountCreditionalsValidationRules()
        {
            base.RuleFor(c => c.Email)
                .NotEmpty();

            base.RuleFor(c => c.Password)
                .NotEmpty();
        }
    }
}
