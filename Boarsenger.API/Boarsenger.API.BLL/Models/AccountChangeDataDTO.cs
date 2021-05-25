using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.BLL.Models
{
    public class AccountChangeDataDTO
    {
        public AccountTokenDTO AccountToken { get; set; }

        public AccountDataDTO AccountData { get; set; }
    }
}
