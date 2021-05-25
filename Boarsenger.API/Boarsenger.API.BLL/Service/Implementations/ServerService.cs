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
        private IAuthorizationService authorizationService;

        public ServerService(
            IRepository<Server> serverRepository,
            IAccountTokenService accountTokenService,
            IEncryptionService encryptionService,
            IServerTokenService serverTokenService,
            IAuthorizationService authorizationService)
        {
            this.serverRepository = serverRepository;
            this.serverTokenService = serverTokenService;
            this.authorizationService = authorizationService;

            this.accountTokenService = accountTokenService;
            this.encryptionService = encryptionService;
        }

        public Task<IServiceResult> ChangeServerStatusAsync(
            UpdateServerPublicationStatusDTO publicationData)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult<ServerTokenDTO>> ChangeServerTokenAsync(
            ServerInfoUpdate changeServerInfo)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult<ServerTokenDTO>> ChangeServerTokenAsync(ServerTokenDTO serverToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<UpdateServerDataDTO>> CreateServerAsync(
            ChangeServerSettingsDTO createServerData)
        {
            try
            {
                var accountGuidResult = await this.accountTokenService
                    .GetAccountIdByTokenAsync(createServerData.ServerInfoUpdateData.AccountToken);

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
                    IsPublished = true,
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

        public Task<IServiceResult<UpdateServerDataDTO>> CreateServerAsync(
            ServerDataDTO createServerData)
        {
            return CreateServerAsync(new ChangeServerSettingsDTO()
            {
                ServerData = createServerData,
                ServerInfoUpdateData = new ServerInfoUpdate()
                {
                    AccountToken = new AccountTokenDTO()
                    {
                        Token = this.authorizationService.GetAccountToken()
                    }
                }
            });
        }

        public Task<IServiceResult> DeleteServerAsync(ServerInfoUpdate deleteServerInfo)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> DeleteServerAsync(ServerTokenDTO serverToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<EntityPage<ServerDataDTO>>> GetMyServersAsync(PageDataDTO pageData, AccountTokenDTO accountToken)
        {
            try
            {
                var accountId = await this.accountTokenService.GetAccountIdByTokenAsync(accountToken);

                if (!accountId.IsSuccesful)
                {
                    return ServiceResult<EntityPage<ServerDataDTO>>.FromResult(false, null, accountId.Message);
                }

                var serverPage = await this.serverRepository.GetPageAsync<ServerDataDTO>(
                    c => c.OwnerId == accountId.Result,
                    c => new ServerDataDTO()
                    {
                        Title = c.Title,
                        IsAdultOnly = c.IsAdultOnly,
                        ServerIP = c.IP
                    },
                    pageData.PageNumber,
                    pageData.PageSize);

                return ServiceResult<EntityPage<ServerDataDTO>>.FromResult(
                    true,
                    new EntityPage<ServerDataDTO>()
                    {
                        PageData = serverPage,
                        CurrentPage = pageData.PageNumber,
                        PageSize = pageData.PageSize,
                        CanMoveBack = false,
                        CanMoveNext = false
                    });
            }
            catch (Exception ex)
            {
                return ServiceResult<EntityPage<ServerDataDTO>>.FromResult(false, null, ex.Message);
            }
        }

        public Task<IServiceResult<EntityPage<ServerDataDTO>>> GetMyServersAsync(PageDataDTO pageData)
        {
            return GetMyServersAsync(pageData, new AccountTokenDTO()
            {
                Token = this.authorizationService.GetAccountToken()
            });
        }

        public Task<IServiceResult<EntityPage<ServerDataDTO>>> GetServerDataAsync(PageDataDTO pageData)
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResult<EntityPage<ServerDataDTO>>> GetServerListAsync(PageDataDTO pageData)
        {
            try
            {
                var serverPage = await this.serverRepository.GetPageAsync<ServerDataDTO>(
                    c => true,
                    c => new ServerDataDTO()
                    {
                        Title = c.Title,
                        IsAdultOnly = c.IsAdultOnly,
                        ServerIP = c.IP
                    },
                    pageData.PageNumber,
                    pageData.PageSize);

                return ServiceResult<EntityPage<ServerDataDTO>>.FromResult(
                    true,
                    new EntityPage<ServerDataDTO>()
                    {
                        PageData = serverPage,
                        CurrentPage = pageData.PageNumber,
                        PageSize = pageData.PageSize,
                        CanMoveBack = false,
                        CanMoveNext = false
                    });

            }
            catch (Exception ex)
            {
                return ServiceResult<EntityPage<ServerDataDTO>>.FromResult(false, null, ex.Message);
            }
        }

        public Task<IServiceResult> UpdateServerDataAsync(UpdateServerDataDTO updateServerData)
        {
            throw new NotImplementedException();
        }
    }
}
