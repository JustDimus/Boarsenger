using Boarsenger.API.BLL.Models;
using Boarsenger.API.Core.Models;
using Boarsenger.API.DAL.Repository;
using System;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private IRepository<Account> accountRepository;

        private IEncryptionService encryptionService;

        private IAccountTokenService accountTokenService;

        private IRepository<AccountToken> accountTokenRepository;

        public AccountService(
            IRepository<Account> accountRepository,
            IRepository<AccountToken> accountTokenRepository,
            IEncryptionService encryptionService,
            IAccountTokenService accountTokenService)
        {
            this.accountRepository = accountRepository;
            this.encryptionService = encryptionService;
            this.accountTokenRepository = accountTokenRepository;
            this.accountTokenService = accountTokenService;
        }

        public async Task<IServiceResult<AccountTokenDTO>> RegisterAsync(AccountCredentialsDTO registrationModel)
        {
            try
            {
                var isAccountExist = await this.accountRepository.AnyAsync(account => account.Email == registrationModel.Email);

                if (isAccountExist)
                {
                    return ServiceResult<AccountTokenDTO>.FromResult(false, null, "Email already exists");
                }

                Account account = new Account()
                {
                    Email = registrationModel.Email,
                    Password = encryptionService.Encrypt(registrationModel.Password)
                };

                await this.accountRepository.CreateAsync(account);

                await this.accountRepository.SaveAsync();

                var accountToken = await this.accountTokenService.GenerateAccountToken(new AccountDataDTO()
                {
                    Id = account.Id,
                    Email = account.Email
                });

                return ServiceResult<AccountTokenDTO>.FromResult(
                    accountToken.IsSuccesful,
                    accountToken.Result);
            }
            catch(Exception ex)
            {
                return ServiceResult<AccountTokenDTO>.FromResult(false, null, ex.Message);
            }
        }

        public async Task<IServiceResult<AccountTokenDTO>> TryLogInAsync(AccountCredentialsDTO loginModel)
        {
            try
            {
                var account = await this.accountRepository
                    .GetAsync(account => 
                    account.Email == loginModel.Email
                    && account.Password == this.encryptionService.Encrypt(loginModel.Password));

                if (account == null)
                {
                    return ServiceResult<AccountTokenDTO>.FromResult(false, null, "Account with this credentials doesn't exist");
                }

                var accountToken = await this.accountTokenService.GenerateAccountToken(new AccountDataDTO()
                {
                    Id = account.Id,
                    Email = account.Email
                });

                return ServiceResult<AccountTokenDTO>.FromResult(
                    accountToken.IsSuccesful,
                    accountToken.Result);
            }
            catch (Exception ex)
            {
                return ServiceResult<AccountTokenDTO>.FromResult(false, null, ex.Message);
            }
        }

        public Task<IServiceResult<string>> ClearAccountTokenAsync(AccountTokenDTO accountToken)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult<AccountTokenDTO>> GenerateAccountTokenAsync(AccountCredentialsDTO loginModel)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> TryLogOutAsync(AccountTokenDTO accountToken)
        {
            throw new NotImplementedException();
        }
    }
}
