using Boarsenger.API.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service
{
    public interface IServerService
    {
        Task<IServiceResult<ServerTokenDTO>> CreateServer(UpdateServerDataDTO createServerData);

        Task<IServiceResult> UpdateServerData(UpdateServerDataDTO updateServerData);

        Task<IServiceResult<EntityPage<ServerDataDTO>>> GetServerData(PageDataDTO pageData);

        Task<IServiceResult> ChangeServerStatus(UpdateServerPublicationStatusDTO publicationData);

        Task<IServiceResult<ServerTokenDTO>> ChangeServerToken(ServerOwnerDataDTO changeServerInfo);

        Task<IServiceResult> DeleteServer(ServerOwnerDataDTO deleteServerInfo);
    }
}
