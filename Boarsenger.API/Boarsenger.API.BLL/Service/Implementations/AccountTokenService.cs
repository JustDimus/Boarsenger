using Boarsenger.API.BLL.Models;
using Boarsenger.API.Core.Models;
using Boarsenger.API.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service.Implementations
{
    public class AccountTokenService : IAccountTokenService
    {
        private IEncryptionService encryptionService;
        private IRepository<AccountToken> accountTokenRepository;

        public AccountTokenService(
            IRepository<AccountToken> accountToken,
            IEncryptionService encryptionService)
        {
            this.accountTokenRepository = accountToken;
            this.encryptionService = encryptionService;
        }

        public async Task<IServiceResult> ClearAccountTokenAsync(AccountTokenDTO accountToken)
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

        public async Task<IServiceResult> ClearAllAccountTokenAsync(AccountDataDTO account)
        {
            try
            {
                await this.accountTokenRepository.DeleteAsync(c => c.AccountId == account.Id);

                return ServiceResult.FromResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult.FromResult(false, ex.Message);
            }
        }

        public async Task<IServiceResult<AccountTokenDTO>> GenerateAccountTokenAsync(AccountDataDTO account)
        {
            try
            {
                DateTime currentTime = DateTime.Now;

                var accountToken = new AccountTokenDTO()
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

                return ServiceResult<AccountTokenDTO>.FromResult(
                    true,
                    accountToken);
            }
            catch (Exception ex)
            {
                return ServiceResult<AccountTokenDTO>.FromResult(
                    false,
                    null,
                    ex.Message);
            }
        }

        public async Task<IServiceResult<Guid>> GetAccountIdByTokenAsync(AccountTokenDTO account)
        {
            try
            {
                if (account == null)
                {
                    return ServiceResult<Guid>.FromResult(false, default, "Account is null");
                }

                var accountToken = await this.accountTokenRepository.GetAsync(c => c.Token == account.Token);

                if (accountToken == null)
                {
                    return ServiceResult<Guid>.FromResult(false, default, "Token doesn't exist");
                }

                return ServiceResult<Guid>.FromResult(true, accountToken.AccountId);
            }
            catch (Exception ex)
            {
                return ServiceResult<Guid>.FromResult(false, default, ex.Message);
            }
        }
    }
}
