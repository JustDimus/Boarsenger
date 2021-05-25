using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class AccountToken
    {
        [JsonProperty("account_login")]
        public string Email { get; set; }

        [JsonProperty("account_token")]
        public string Token { get; set; }
    }
}
