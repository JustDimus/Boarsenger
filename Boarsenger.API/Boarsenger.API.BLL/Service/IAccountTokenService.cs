using Boarsenger.API.BLL.Models;
using Boarsenger.API.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service
{
    public interface IAccountTokenService
    {
        Task<IServiceResult<AccountTokenDTO>> GenerateAccountTokenAsync(AccountDataDTO account);

        Task<IServiceResult<Guid>> GetAccountIdByTokenAsync(AccountTokenDTO account);

        Task<IServiceResult> ClearAccountTokenAsync(AccountTokenDTO accountToken);

        Task<IServiceResult> ClearAllAccountTokenAsync(AccountDataDTO account);
    }
}
