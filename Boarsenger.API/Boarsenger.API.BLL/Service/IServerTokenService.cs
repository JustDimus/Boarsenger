using Boarsenger.API.BLL.Models;
using Boarsenger.API.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.API.BLL.Service
{
    public interface IServerTokenService
    {
        Task<IServiceResult<ServerTokenDTO>> GenerateServerToken(Server serverData); 
    }
}
