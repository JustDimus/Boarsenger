using Boarsenger.API.BLL.Models;
using Boarsenger.API.BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.MVCControllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IAccountService accountService;

        public ProfileController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public async IActionResult ProfileEdit()
        {
            var resultAccount = await this.accountService.GetAccountDataAsync();



            return View();
        }

        [HttpPost]
        public IActionResult ProfileEdit([FromForm] AccountDataDTO accountData)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ServerManagement()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PremiumAccess()
        {
            return View();
        }
    }
}
