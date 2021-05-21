using Boarsenger.API.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service
{
    public interface IAccountService
    {
        Task<IServiceResult<Guid>> TryLogInAsync(AccountCredentials loginModel);

        Task<IServiceResult<Guid>> RegisterAsync(AccountCredentials registrationModel);

        Task<IServiceResult<string>> ReCreateAccountToken(AccountCredentials loginModel);

        Task<IServiceResult<string>> 
    }
}
