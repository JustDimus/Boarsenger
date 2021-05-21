using Boarsenger.API.BLL.Model;
using Boarsenger.API.Core.Models;
using Boarsenger.API.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
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

        public Task<IServiceResult<Guid>> LoginAsync(AccountLoginDataDTO accountData)
        {
            this.repository.GetAsync(x => x.Email == accountData.Email && x.Password == accountData.Password);

            throw new NotImplementedException();
        }

        public Task<IServiceResult<Guid>> RegisterAsync(AccountLoginDataDTO accountData)
        {
            throw new NotImplementedException();
        }
    }
}
