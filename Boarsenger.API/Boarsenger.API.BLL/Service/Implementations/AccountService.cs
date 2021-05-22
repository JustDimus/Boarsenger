
using Boarsenger.API.BLL.Models;
using Boarsenger.API.Core.Models;
using Boarsenger.API.DAL.Repository;
using System;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<AuthorizationAccount> repository;

        public AccountService(IRepository<AuthorizationAccount> repository)
        {
            this.repository = repository;
        }

        public Task<IServiceResult<string>> ClearAccountTokenAsync(AccountTokenDTO accountToken)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult<AccountTokenDTO>> GenerateAccountTokenAsync(AccountCredentialsDTO loginModel)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult<Guid>> RegisterAsync(AccountCredentialsDTO registrationModel)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult<Guid>> TryLogInAsync(AccountCredentialsDTO loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
