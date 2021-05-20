using Boarsenger.API.BLL.Model;
using Boarsenger.API.BLL.Service;
using Boarsenger.API.MVCInterface.ViewModels;
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

            AccountLoginDataDTO accountDTO = new AccountLoginDataDTO()
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

        public async Task<IActionResult> Authorize(LoginViewModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
