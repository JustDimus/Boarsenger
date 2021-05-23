using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class ServerToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
