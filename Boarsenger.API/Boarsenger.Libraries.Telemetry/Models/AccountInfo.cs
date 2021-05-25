using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class AccountInfo
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("second_name")]
        public string SecondName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
