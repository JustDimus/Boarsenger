using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class CreateServerData
    {
        [JsonProperty("server_owner_data")]
        public ServerOwnerData ServerOwnerData { get; set; }
        [JsonProperty("server_data")]
        public ServerData ServerData { get; set; }
    }
}
