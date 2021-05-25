using Boarsenger.API.BLL.Models;
using Boarsenger.API.BLL.Service;
using Boarsenger.Libraries.Telemetry.Models;
using Boarsenger.Libraries.Telemetry.Parser;
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
        public async Task<IActionResult> CreateServer([FromBody] CreateServerData createServerData)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ServerResult()
                {
                    StatusCode = 400
                });
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

            var result = await this.serverService.CreateServerAsync(serverChangeData);

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
                return Ok(result.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PublishServer([FromBody] ServerOwnerData serverOwnerData)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ServerResult()
                {
                    StatusCode = 400
                });
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

            var result = await this.serverService.ChangeServerStatusAsync(data);

            if (result.IsSuccesful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    
        [HttpPost]
        public async Task<IActionResult> GetServerList()
        {
            var serviceResult = await this.serverService.GetServerListAsync(new PageDataDTO()
            {
                PageNumber = 0,
                PageSize = 10
            });

            var result = new ServerResult()
            {
                StatusCode = serviceResult.IsSuccesful ? 200 : 400,
                Result = serviceResult.IsSuccesful
                ? JsonParser.ParseToString(new ServerPageData()
                {
                    ServerList = serviceResult.Result.PageData.Select(c => 
                    new ServerData()
                    {
                        Title = c.Title,
                        Description = c.Description,
                        MaxUserCount = c.MaxUserCount,
                        IsAdultOnly = c.IsAdultOnly,
                        ServerIP = c.ServerIP
                    }).ToList(),
                    CanMoveBack = serviceResult.Result.CanMoveBack,
                    CanMoveForward = serviceResult.Result.CanMoveNext,
                    CurrentPage = serviceResult.Result.CurrentPage,
                    CurrentPageSize = serviceResult.Result.PageSize
                })
                : serviceResult.Message
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetMyServers([FromBody] AccountToken accountToken)
        {
            var serviceResult = await this.serverService.GetMyServersAsync(
                new PageDataDTO()
                {
                    PageNumber = 0,
                    PageSize = 10
                },
                new AccountTokenDTO()
                {
                    Email = accountToken.Email,
                    Token = accountToken.Token
                });

            var result = new ServerResult()
            {
                StatusCode = serviceResult.IsSuccesful ? 200 : 400,
                Result = serviceResult.IsSuccesful
                ? JsonParser.ParseToString(new ServerPageData()
                {
                    ServerList = serviceResult.Result.PageData.Select(c =>
                    new ServerData()
                    {
                        Title = c.Title,
                        Description = c.Description,
                        MaxUserCount = c.MaxUserCount,
                        IsAdultOnly = c.IsAdultOnly,
                        ServerIP = c.ServerIP
                    }).ToList(),
                    CanMoveBack = serviceResult.Result.CanMoveBack,
                    CanMoveForward = serviceResult.Result.CanMoveNext,
                    CurrentPage = serviceResult.Result.CurrentPage,
                    CurrentPageSize = serviceResult.Result.PageSize
                })
                : serviceResult.Message
            };

            return Ok(result);
        }
    }
}
