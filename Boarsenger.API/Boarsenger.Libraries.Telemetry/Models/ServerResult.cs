using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class ServerResult
    {
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }

        [JsonProperty("server_result")]
        public string Result { get; set; }
    }
}
