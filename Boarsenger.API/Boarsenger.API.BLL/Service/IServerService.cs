using Boarsenger.API.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service
{
    public interface IServerService
    {
        Task<IServiceResult<UpdateServerDataDTO>> CreateServerAsync(ChangeServerSettingsDTO createServerData);
        
        Task<IServiceResult<UpdateServerDataDTO>> CreateServerAsync(ServerDataDTO createServerData);

        Task<IServiceResult> UpdateServerDataAsync(UpdateServerDataDTO updateServerData);

        Task<IServiceResult<EntityPage<ServerDataDTO>>> GetServerDataAsync(PageDataDTO pageData);

        Task<IServiceResult> ChangeServerStatusAsync(UpdateServerPublicationStatusDTO publicationData);

        Task<IServiceResult<ServerTokenDTO>> ChangeServerTokenAsync(ServerInfoUpdate changeServerInfo);

        Task<IServiceResult<ServerTokenDTO>> ChangeServerTokenAsync(ServerTokenDTO serverToken);

        Task<IServiceResult> DeleteServerAsync(ServerInfoUpdate deleteServerInfo);

        Task<IServiceResult> DeleteServerAsync(ServerTokenDTO serverToken);

        Task<IServiceResult<EntityPage<ServerDataDTO>>> GetServerListAsync(PageDataDTO pageData);

        Task<IServiceResult<EntityPage<ServerDataDTO>>> GetMyServersAsync(PageDataDTO pageData, AccountTokenDTO accountToken);

        Task<IServiceResult<EntityPage<ServerDataDTO>>> GetMyServersAsync(PageDataDTO pageData);
    }
}
