using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class CreateServer
    {
        [JsonProperty("account_token")]
        public AccountToken AccountToken { get; set; }

        [JsonProperty("server_data")]
        public ServerData ServerData { get; set; }
    }
}
