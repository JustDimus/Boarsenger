using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.BLL.Models
{
    public class UpdateServerDataDTO
    {
        public ServerDataDTO ServerData { get; set; }

        public AccountTokenDTO AccountToken { get; set; }
    }
}
