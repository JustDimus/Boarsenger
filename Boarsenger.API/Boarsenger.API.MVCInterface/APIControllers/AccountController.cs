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
    public class AccountController : ControllerBase
    {
        private IAccountService accountService;

        public AccountController(
            IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountCreditionals accountCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(accountCredentials);
            }

            AccountCredentialsDTO accountDTO = new AccountCredentialsDTO()
            {
                Email = accountCredentials.Email,
                Password = accountCredentials.Password
            };

            var serviceResult = await this.accountService.RegisterAsync(accountDTO);

            if (serviceResult.IsSuccesful)
            {
                return Ok(JsonParser.ParseToString(new AccountToken()
                {
                    Email = serviceResult.Result.Email,
                    Token = serviceResult.Result.Token
                }));
            }
            else
            {
                return BadRequest(serviceResult.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountCreditionals accountCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(accountCredentials);
            }

            AccountCredentialsDTO accountDTO = new AccountCredentialsDTO()
            {
                Email = accountCredentials.Email,
                Password = accountCredentials.Password
            };

            var serviceResult = await this.accountService.TryLogInAsync(accountDTO);

            if (serviceResult.IsSuccesful)
            {
                return new JsonResult(new AccountToken()
                {
                    Email = serviceResult.Result.Email,
                    Token = serviceResult.Result.Token
                });
            }
            else
            {
                return BadRequest(serviceResult.Message);
            }
        }
         
        [HttpPost]
        public async Task<IActionResult> Logout(AccountToken accountToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            AccountTokenDTO accountTokenDTO = new AccountTokenDTO()
            {
                Email = accountToken.Email,
                Token = accountToken.Token
            };

            var serviceResult = await this.accountService.TryLogOutAsync(accountTokenDTO);

            if (serviceResult.IsSuccesful)
            {
                return Ok();
            }
            else
            {
                return BadRequest(serviceResult.Message);
            }
        }
    }
}
