using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class AccountInfo : AccountToken
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }
    }
}
