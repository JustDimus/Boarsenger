using Boarsenger.API.BLL.Models;
using Boarsenger.API.Core.Models;
using Boarsenger.API.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service.Implementations
{
    public class ServerTokenService : IServerTokenService
    {
        private IRepository<ServerToken> serverTokenRepository;

        private IEncryptionService encryptionService;

        public ServerTokenService(
            IRepository<ServerToken> serverTokenRepository,
            IEncryptionService encryptionService)
        {
            this.serverTokenRepository = serverTokenRepository;

            this.encryptionService = encryptionService;
        }

        public async Task<IServiceResult<ServerTokenDTO>> GenerateServerToken(Server serverData)
        {
            try
            {
                var serverTokenResult = await this.serverTokenRepository.GetAsync(s => s.ServerId == serverData.Id);

                if (serverTokenResult != null)
                {
                    await this.serverTokenRepository.DeleteAsync(serverTokenResult);

                    serverTokenResult = null;
                }

                ServerToken token = new ServerToken()
                {
                    ServerId = serverData.Id,
                    Token = $"{this.encryptionService.Encrypt(DateTime.Now.ToString())}{this.encryptionService.Encrypt(serverData.IP)}"
                };

                await this.serverTokenRepository.CreateAsync(token);

                await this.serverTokenRepository.SaveAsync();

                return ServiceResult<ServerTokenDTO>.FromResult(
                    true,
                    new ServerTokenDTO()
                    {
                        Token = token.Token
                    });
            }
            catch (Exception ex)
            {
                return ServiceResult<ServerTokenDTO>.FromResult(false, null, ex.Message);
            }
        }
    }
}
