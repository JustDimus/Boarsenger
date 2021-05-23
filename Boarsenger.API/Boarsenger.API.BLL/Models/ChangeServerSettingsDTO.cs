using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.BLL.Models
{
    public class ChangeServerSettingsDTO
    {
        public ServerInfoUpdate ServerInfoUpdateData { get; set; }

        public ServerDataDTO ServerData { get; set; }
    }
}
