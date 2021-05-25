using Boarsenger.API.BLL.Models;
using Boarsenger.API.BLL.Service;
using Boarsenger.API.MVCInterface.Services.Implementation;
using Boarsenger.API.MVCInterface.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.MVCControllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IHttpContextAccessor contextAccessor;

        public ProfileController(IAccountService accountService, IHttpContextAccessor contextAccessor)
        {
            this.accountService = accountService;
            this.contextAccessor = contextAccessor;
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProfileEdit()
        {
            var resultAccount = await this.accountService.GetAccountDataAsync();

            var profileEdit = new ProfileEditViewModel()
            {
                Age = resultAccount.Result.Age,
                FirstName = resultAccount.Result.FirstName,
                SecondName = resultAccount.Result.SecondName,
                Email = resultAccount.Result.Email,
                RegistrationDate = resultAccount.Result.RegistrationDate,
                Password = resultAccount.Result.Password
            };

            return View(profileEdit);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileEdit([FromForm]ProfileEditViewModel profileEdit)
        {
            var accound = await this.accountService.GetAccountDataAsync();

            var profileDTO = new AccountDataDTO()
            {
                RegistrationDate = profileEdit.RegistrationDate,
                Age = profileEdit.Age,
                SecondName = profileEdit.SecondName,
                Email = profileEdit.Email,
                FirstName = profileEdit.FirstName
            };

            return this.RedirectToAction("ServerManagement", "Profile");
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
