using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class FullAccountData
    {
        [JsonProperty("account_token")]
        public AccountToken AccountToken { get; set; }

        [JsonProperty("Account_info")]
        public AccountInfo AccountInfo { get; set; }
    }
}
