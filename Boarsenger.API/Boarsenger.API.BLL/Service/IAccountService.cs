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

        Task<IServiceResult> TryLogOutAsync();

        Task<IServiceResult<AccountTokenDTO>> RegisterAsync(AccountCredentialsDTO registrationModel);

        Task<IServiceResult<AccountDataDTO>> GetAccountDataAsync();

        Task<IServiceResult<AccountDataDTO>> GetAccountDataAsync(AccountTokenDTO accountToken);

        Task<IServiceResult> SetAccountDataAsync(AccountDataDTO accountData);
    }
}
