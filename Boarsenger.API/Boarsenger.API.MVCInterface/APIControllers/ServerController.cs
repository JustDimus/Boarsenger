using Boarsenger.API.BLL.Models;
using Boarsenger.API.BLL.Service;
using Boarsenger.Libraries.Telemetry.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.APIControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServerApiController : ControllerBase
    {
        private IServerService serverService;

        public ServerApiController(IServerService serverService)
        {
            this.serverService = serverService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateServer(CreateServerData createServerData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(createServerData);
            }

            ChangeServerSettingsDTO serverChangeData = new ChangeServerSettingsDTO()
            {
                ServerInfoUpdateData = new ServerInfoUpdate()
                {
                    AccountToken = new AccountTokenDTO()
                    {
                        Email = createServerData.ServerOwnerData.AccountToken.Email,
                        Token = createServerData.ServerOwnerData.AccountToken.Token
                    },
                    ServerToken = null
                },
                ServerData = new ServerDataDTO()
                {
                    Description = createServerData.ServerData.Description,
                    IsAdultOnly = createServerData.ServerData.IsAdultOnly,
                    MaxUserCount = createServerData.ServerData.MaxUserCount,
                    ServerIP = createServerData.ServerData.ServerIP,
                    Title = createServerData.ServerData.Title
                }
            };

            var result = await this.serverService.CreateServer(serverChangeData);

            if (result.IsSuccesful)
            {
                return Ok(new ServerInfo()
                {
                    ServerData = new ServerData()
                    {
                        Title = result.Result.ServerData.Title,
                        Description = result.Result.ServerData.Description,
                        IsAdultOnly = result.Result.ServerData.IsAdultOnly,
                        MaxUserCount = result.Result.ServerData.MaxUserCount,
                        ServerIP = result.Result.ServerData.ServerIP
                    },
                    ServerToken = new ServerToken()
                    {
                        Token = result.Result.ServerToken.Token
                    }
                });
            }    
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PublishServer(ServerOwnerData serverOwnerData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(serverOwnerData);
            }

            UpdateServerPublicationStatusDTO data = new UpdateServerPublicationStatusDTO()
            {
                ServerToken = new ServerTokenDTO()
                {
                    Token = serverOwnerData.ServerToken.Token
                },
                PublicationStatus = true,
                AccountToken = new AccountTokenDTO()
                {
                    Token = serverOwnerData.AccountToken.Token,
                    Email = serverOwnerData.AccountToken.Email
                }
            };

            var result = await this.serverService.ChangeServerStatus(data);

            if (result.IsSuccesful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
