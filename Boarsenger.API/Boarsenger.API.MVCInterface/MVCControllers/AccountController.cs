﻿using Boarsenger.API.BLL.Models;
using Boarsenger.API.BLL.Service;
using Boarsenger.API.MVCInterface.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.MVCControllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm]LoginViewModel loginModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(loginModel);
            }

            AccountCredentialsDTO accountDTO = new AccountCredentialsDTO()
            {
                Email = loginModel.Email,
                Password = loginModel.Password
            };

            var loginResult = await this.accountService.TryLogInAsync(accountDTO);

            if (!loginResult.IsSuccesful)
            {
                this.ModelState.AddModelError(String.Empty, loginResult.Message);

                return this.View(loginModel);
            }
            else
            {
                await AuthenticateAsync(loginResult.Result.Token);

                return this.RedirectToAction("ServerManagement", "Profile");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration([FromForm]RegisterViewModel registrationModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(registrationModel);
            }

            AccountCredentialsDTO accountDTO = new AccountCredentialsDTO()
            {
                Email = registrationModel.Email,
                Password = registrationModel.Password
            };

            var registerResult = await this.accountService.RegisterAsync(accountDTO);

            if (!registerResult.IsSuccesful)
            {
                this.ModelState.AddModelError(String.Empty, registerResult.Message);

                return this.View(registrationModel);
            }
            else
            {
                return this.RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ProfileEdit()
        {
            return View("");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return this.RedirectToAction("Index", "Home");
        }

        private async Task AuthenticateAsync(string accountToken)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.SerialNumber, accountToken)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await this.HttpContext.SignInAsync(principal);
        }
    }
}
