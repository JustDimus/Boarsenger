using Boarsenger.API.BLL.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.Services.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        private IHttpContextAccessor httpContext;

        public AuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContext = httpContextAccessor;
        }

        public string GetAccountToken()
        {
            var accountToken = this.httpContext.HttpContext.User.Claims
                .Where(p => p.Type == ClaimTypes.SerialNumber)
                .First()
                .Value;

            return accountToken;
        }
    }
}
