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
        Task<IServiceResult<AccountTokenDTO>> GenerateAccountToken(AccountDataDTO account);

        Task<IServiceResult<Guid>> GetAccountIdByToken(AccountTokenDTO account);

        Task<IServiceResult> ClearAccountToken(AccountTokenDTO accountToken);

        Task<IServiceResult> ClearAllAccountToken(AccountDataDTO account);
    }
}
