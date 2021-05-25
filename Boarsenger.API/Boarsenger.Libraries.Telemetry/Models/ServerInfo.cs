using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class ServerInfo
    {
        [JsonProperty("server_data")]
        public ServerData ServerData { get; set; }
        [JsonProperty("server_token")]
        public ServerToken ServerToken { get; set; }
    }
}
