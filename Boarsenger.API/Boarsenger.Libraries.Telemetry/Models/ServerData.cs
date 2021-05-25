using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class ServerData
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("max_user_count")]
        public int MaxUserCount { get; set; }
        [JsonProperty("is_adult_only")]
        public bool IsAdultOnly { get; set; }
        [JsonProperty("server_ip")]
        public string ServerIP { get; set; }
    }
}
