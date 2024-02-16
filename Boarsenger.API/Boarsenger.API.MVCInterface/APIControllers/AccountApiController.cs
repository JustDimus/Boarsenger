using Boarsenger.API.BLL.Models;
using Boarsenger.API.BLL.Service;
using Boarsenger.API.MVCInterface.ViewModels;
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
    public class AccountApiController : ControllerBase
    {
        private IAccountService accountService;

        public AccountApiController(
            IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AccountCreditionals accountCredentials)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ServerResult()
                {
                    StatusCode = 400
                });
            }

            AccountCredentialsDTO accountDTO = new AccountCredentialsDTO()
            {
                Email = accountCredentials.Email,
                Password = accountCredentials.Password
            };

            var serviceResult = await this.accountService.RegisterAsync(accountDTO);

            ServerResult result = new ServerResult()
            {
                StatusCode = serviceResult.IsSuccesful ? 200 : 400,
                Result = serviceResult.IsSuccesful 
                ? JsonParser.ParseToString(new AccountToken()
                {
                    Email = serviceResult.Result.Email,
                    Token = serviceResult.Result.Token
                }) : serviceResult.Message
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AccountCreditionals accountCredentials)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ServerResult()
                {
                    StatusCode = 400
                });
            }

            AccountCredentialsDTO accountDTO = new AccountCredentialsDTO()
            {
                Email = accountCredentials.Email,
                Password = accountCredentials.Password
            };

            var serviceResult = await this.accountService.TryLogInAsync(accountDTO);

            ServerResult result = new ServerResult()
            {
                StatusCode = serviceResult.IsSuccesful ? 200 : 400,
                Result = serviceResult.IsSuccesful
                ? JsonParser.ParseToString(new AccountToken()
                {
                    Email = serviceResult.Result.Email,
                    Token = serviceResult.Result.Token
                }) : serviceResult.Message
            };

            return Ok(result);
        }
         
        [HttpPost]
        public async Task<IActionResult> Logout(AccountToken accountToken)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new ServerResult()
                {
                    StatusCode = 400
                });
            }

            AccountTokenDTO accountTokenDTO = new AccountTokenDTO()
            {
                Email = accountToken.Email,
                Token = accountToken.Token
            };

            var serviceResult = await this.accountService.TryLogOutAsync(accountTokenDTO);

            ServerResult result = new ServerResult()
            {
                StatusCode = serviceResult.IsSuccesful ? 200 : 400,
                Result = serviceResult.IsSuccesful
                ? string.Empty : serviceResult.Message
            };

            return Ok(result);
        }
    }
}
