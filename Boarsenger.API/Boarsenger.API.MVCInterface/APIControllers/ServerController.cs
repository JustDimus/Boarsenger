using Boarsenger.Libraries.Telemetry.Models;
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
    public class ServerController : ControllerBase
    {
        public async Task<IActionResult> CreateServer(CreateServer createServerData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(createServerData);
            }


        }
    }
}
