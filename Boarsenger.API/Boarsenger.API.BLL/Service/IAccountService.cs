using Boarsenger.API.BLL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service
{
    public interface IAccountService
    {
        Task<IServiceResult<Guid>> LoginAsync(AccountLoginDataDTO accountData);

        Task<IServiceResult<Guid>> RegisterAsync(AccountLoginDataDTO accountData);
    }
}
