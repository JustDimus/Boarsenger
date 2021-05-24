using Boarsenger.API.MVCInterface.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boarsenger.API.MVCInterface.MVCControllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginViewModel loginModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromForm]RegisterViewModel registrationModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        
        public IActionResult Profile()
        {
            return View();
        }

        
        public IActionResult ProfileEdit()
        {
            return View("");
        }
    }
}
