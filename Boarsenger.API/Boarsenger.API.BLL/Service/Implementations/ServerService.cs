using Boarsenger.API.BLL.Models;
using Boarsenger.API.Core.Models;
using Boarsenger.API.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service.Implementations
{
    public class ServerService : IServerService
    {
        private IRepository<Server> serverRepository;
        private IAccountTokenService accountTokenService;
        private IEncryptionService encryptionService;
        private IServerTokenService serverTokenService;

        public ServerService(
            IRepository<Server> serverRepository,
            IAccountTokenService accountTokenService,
            IEncryptionService encryptionService,
            IServerTokenService serverTokenService)
        {
            this.serverRepository = serverRepository;
            this.serverTokenService = serverTokenService;

            this.accountTokenService = accountTokenService;
            this.encryptionService = encryptionService;
        }

        public Task<IServiceResult> ChangeServerStatus(UpdateServerPublicationStatusDTO publicationData)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult<ServerTokenDTO>> ChangeServerToken(ServerInfoUpdate changeServerInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<UpdateServerDataDTO>> CreateServer(ChangeServerSettingsDTO createServerData)
        {
            try
            {
                var accountGuidResult = await this.accountTokenService
                    .GetAccountIdByToken(createServerData.ServerInfoUpdateData.AccountToken);

                if (!accountGuidResult.IsSuccesful)
                {
                    return ServiceResult<UpdateServerDataDTO>.FromResult(false, null, accountGuidResult.Message);
                }

                var accountServerCount = await this.serverRepository.CountAsync(c => c.OwnerId == accountGuidResult.Result);

                if (accountServerCount > 3)
                {
                    return ServiceResult<UpdateServerDataDTO>.FromResult(false, null, "Exceeded max server's count");
                }

                Server server = new Server()
                {
                    IP = createServerData.ServerData.ServerIP,
                    IsAdultOnly = createServerData.ServerData.IsAdultOnly,
                    IsBanned = false,
                    IsPublished = false,
                    Title = createServerData.ServerData.Title,
                    OwnerId = accountGuidResult.Result
                };

                await this.serverRepository.CreateAsync(server);

                await this.serverRepository.SaveAsync();

                var serverTokenResult = await this.serverTokenService.GenerateServerToken(server);

                if (!serverTokenResult.IsSuccesful)
                {
                    await this.serverRepository.DeleteAsync(server);

                    await this.serverRepository.SaveAsync();

                    return ServiceResult<UpdateServerDataDTO>.FromResult(false, null, serverTokenResult.Message);
                }

                return ServiceResult<UpdateServerDataDTO>.FromResult(
                    true,
                    new UpdateServerDataDTO()
                    {
                        ServerData = createServerData.ServerData,
                        ServerToken = new ServerTokenDTO()
                        {
                            Token = serverTokenResult.Result.Token
                        }
                    });
            }
            catch (Exception ex)
            {
                return ServiceResult<UpdateServerDataDTO>.FromResult(false, null, ex.Message);
            }
        }

        public Task<IServiceResult> DeleteServer(ServerInfoUpdate deleteServerInfo)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult<EntityPage<ServerDataDTO>>> GetServerData(PageDataDTO pageData)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> UpdateServerData(UpdateServerDataDTO updateServerData)
        {
            throw new NotImplementedException();
        }
    }
}
