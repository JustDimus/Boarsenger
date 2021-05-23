using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.BLL.Models
{
    public class ServerOwnerDataDTO
    {
        public AccountTokenDTO AccountToken { get; set; }

        public ServerTokenDTO ServerToken { get; set; }
    }
}
