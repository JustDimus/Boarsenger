using Boarsenger.API.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service
{
    public interface IAccountService
    {
        Task<IServiceResult<AccountTokenDTO>> TryLogInAsync(AccountCredentialsDTO loginModel);

        Task<IServiceResult> TryLogOutAsync(AccountTokenDTO accountToken);

        Task<IServiceResult<AccountTokenDTO>> RegisterAsync(AccountCredentialsDTO registrationModel);
    }
}
