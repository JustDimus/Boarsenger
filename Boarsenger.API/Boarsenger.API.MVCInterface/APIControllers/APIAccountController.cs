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

        public async Task<IActionResult> Authorize(LoginViewModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
