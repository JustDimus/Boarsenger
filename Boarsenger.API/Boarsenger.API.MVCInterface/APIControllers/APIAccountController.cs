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
    [Route("api/[controller]")]
    [ApiController]
    public class APIAccountController : ControllerBase
    {
        private IAccountService accountService;

        public APIAccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(registerVM);
            }

            AccountCredentialsDTO accountDTO = new AccountCredentialsDTO()
            {
                Email = registerVM.Email,
                Password = registerVM.Password
            };

            var serviceResult = await this.accountService.RegisterAsync(accountDTO);

            if (!serviceResult.IsSuccesful)
            {
                return Ok(serviceResult.Result);
            }
            else
            {
                return BadRequest(serviceResult.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateAccountToken([FromBody]AccountCreditionals accountData)
        {
            if (!base.ModelState.IsValid)
            {
                return base.BadRequest(JsonParser
                    .ParseToString(new AccountToken() { Token = null }));
            }

            var accountCreditionalsDTO = new AccountCredentialsDTO()
            {
                Email = accountData.Email,
                Password = accountData.Password
            };

            var result = await this.accountService.GenerateAccountTokenAsync(accountCreditionalsDTO);

            if (!result.IsSuccesful)
            {
                return base.BadRequest(JsonParser
                    .ParseToString(new AccountToken() { Token = null }));
            }

            return base.Ok(JsonParser.ParseToString(new AccountToken()
            {
                Token = result.Result.Token
            }));
        }

        public async Task<IActionResult> Authorize(LoginViewModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
