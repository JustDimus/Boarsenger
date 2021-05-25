using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class ServerOwnerData
    {
        [JsonProperty("server_token")]
        public ServerToken ServerToken { get; set; }
        [JsonProperty("account_token")]
        public AccountToken AccountToken { get; set; }
    }
}
