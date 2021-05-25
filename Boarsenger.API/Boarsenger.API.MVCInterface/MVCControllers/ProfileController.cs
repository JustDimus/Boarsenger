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
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult ProfileEdit()
        {
            return View();
        }

        public IActionResult ServerManagement()
        {
            return View();
        }

        public IActionResult PremiumAccess()
        {
            return View();
        }
    }
}
