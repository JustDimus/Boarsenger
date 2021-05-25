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

        private IAuthorizationService authorizationService;

        public AccountService(
            IRepository<Account> accountRepository,
            IEncryptionService encryptionService,
            IAccountTokenService accountTokenService,
            IAuthorizationService authorizationService)
        {
            this.accountRepository = accountRepository;
            this.encryptionService = encryptionService;
            this.accountTokenService = accountTokenService;
            this.authorizationService = authorizationService;
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

                var accountToken = await this.accountTokenService.GenerateAccountTokenAsync(new AccountDataDTO()
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

                var accountToken = await this.accountTokenService.GenerateAccountTokenAsync(new AccountDataDTO()
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

        public async Task<IServiceResult<AccountDataDTO>> GetAccountDataAsync(AccountTokenDTO accountToken)
        {
            try
            {
                var accountId = await this.accountTokenService.GetAccountIdByTokenAsync(new AccountTokenDTO()
                {
                    Token = accountToken.Token
                });

                if (accountId.IsSuccesful)
                {
                    return ServiceResult<AccountDataDTO>.FromResult(false, null, accountId.Message);
                }

                var accountData = await this.accountRepository.GetAsync(a => a.Id == accountId.Result, a =>
                    new AccountDataDTO()
                    {
                        Email = a.Email,
                        Age = a.Age,
                        FirstName = a.FirstName,
                        Password = string.Empty,
                        SecondName = a.SecondName
                    });

                return ServiceResult<AccountDataDTO>.FromResult(true, accountData);
            }
            catch (Exception ex)
            {
                return ServiceResult<AccountDataDTO>.FromResult(false, null, ex.Message);
            }
        }

        public Task<IServiceResult> TryLogOutAsync()
        {
            var token = this.authorizationService.GetAccountToken();

            return TryLogOutAsync(new AccountTokenDTO()
            {
                Token = token
            });
        }

        public async Task<IServiceResult> TryLogOutAsync(AccountTokenDTO accountToken)
        {
            try
            {
                await this.accountTokenService.ClearAccountTokenAsync(accountToken);

                return ServiceResult.FromResult(true);
            }
            catch(Exception ex)
            {
                return ServiceResult.FromResult(false, ex.Message);
            }
        }

        public Task<IServiceResult<AccountDataDTO>> GetAccountDataAsync()
        {
            var accountToken = this.authorizationService.GetAccountToken();

            return GetAccountDataAsync(new AccountTokenDTO()
            {
                Token = accountToken
            });
        }

        public async Task<IServiceResult> SetAccountDataAsync(AccountDataDTO accountData)
        {
            try
            {
                var accountId = await this.accountTokenService.GetAccountIdByTokenAsync(new AccountTokenDTO()
                {
                    Token = this.authorizationService.GetAccountToken()
                });

                if (accountId.IsSuccesful)
                {
                    return ServiceResult.FromResult(false, accountId.Message);
                }

                var accountDataResult = await this.accountRepository.GetAsync(a => a.Id == accountId.Result);

                accountDataResult.Password = string.IsNullOrWhiteSpace(accountData.Password)
                    ? accountDataResult.Password
                    : this.encryptionService.Encrypt(accountData.Password);

                accountDataResult.SecondName = accountData.SecondName;
                accountDataResult.Age = accountData.Age;
                accountDataResult.FirstName = accountData.FirstName;

                await this.accountRepository.UpdateAsync(accountDataResult);

                await this.accountRepository.SaveAsync();

                return ServiceResult.FromResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult.FromResult(false, ex.Message);
            }
        }
    }
}
