using Boarsenger.API.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service
{
    public interface IAccountService
    {
        Task<IServiceResult<Guid>> TryLogInAsync(AccountCredentialsDTO loginModel);

        Task<IServiceResult<Guid>> RegisterAsync(AccountCredentialsDTO registrationModel);

        Task<IServiceResult<AccountTokenDTO>> GenerateAccountTokenAsync(AccountCredentialsDTO loginModel);

        Task<IServiceResult<string>> ClearAccountTokenAsync(AccountTokenDTO accountToken);
    }
}
