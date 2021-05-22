using Boarsenger.API.BLL.Models;
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
        private IRepository<Account> accountRepository;

        private IEncryptionService encryptionService;

        private IRepository<AccountToken> accountTokenRepository;

        public AccountService(
            IRepository<Account> accountRepository,
            IRepository<AccountToken> accountTokenRepository,
            IEncryptionService encryptionService)
        {
            this.accountRepository = accountRepository;
            this.encryptionService = encryptionService;
            this.accountTokenRepository = accountTokenRepository;
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

                return ServiceResult<AccountTokenDTO>.FromResult(
                    true,
                    await this.GenerateAccountToken(account));
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

                return ServiceResult<AccountTokenDTO>.FromResult(
                    true,
                    await this.GenerateAccountToken(account));
            }
            catch (Exception ex)
            {
                return ServiceResult<AccountTokenDTO>.FromResult(false, null, ex.Message);
            }
        }

        public async Task<IServiceResult> TryLogOutAsync(AccountTokenDTO accountToken)
        {
            try
            {
                var isTokenExist = await this.accountTokenRepository.AnyAsync(at => at.Token == accountToken.Token);

                if (!isTokenExist)
                {
                    return ServiceResult.FromResult(false, "Token doesn't exist");
                }

                await this.accountTokenRepository.DeleteAsync(at => at.Token == accountToken.Token);

                await this.accountTokenRepository.SaveAsync();

                return ServiceResult.FromResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult.FromResult(false, ex.Message);
            }
        }


        private async Task<AccountTokenDTO> GenerateAccountToken(Account account)
        {
            DateTime currentTime = DateTime.Now;

            var accountToken =  new AccountTokenDTO()
            {
                Email = account.Email,
                Token = $"{this.encryptionService.Encrypt(account.Email)}{this.encryptionService.Encrypt(currentTime.ToString("d"))}"
            };

            await this.accountTokenRepository.CreateAsync(new AccountToken()
            {
                AccountId = account.Id,
                TokenDate = currentTime,
                Token = accountToken.Token
            });

            await this.accountTokenRepository.SaveAsync();

            return accountToken;
        }
    }
}
