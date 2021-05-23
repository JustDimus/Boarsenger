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

        public ServerService(IRepository<Server> serverRepository)
        {
            this.serverRepository = serverRepository;
        }

        public Task<IServiceResult> ChangeServerStatus(UpdateServerPublicationStatusDTO publicationData)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult<ServerTokenDTO>> ChangeServerToken(ChangeServerSettingsDTO changeServerInfo)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult<ServerTokenDTO>> CreateServer(UpdateServerDataDTO createServerData)
        {
            throw new NotImplementedException();
        }

        public Task<IServiceResult> DeleteServer(ChangeServerSettingsDTO deleteServerInfo)
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
