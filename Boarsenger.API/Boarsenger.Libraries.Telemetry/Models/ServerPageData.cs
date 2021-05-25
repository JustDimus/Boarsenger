using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Models
{
    public class ServerPageData
    {
        [JsonProperty("server_list")]
        public List<ServerData> ServerList { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("current_page_size")]
        public int CurrentPageSize { get; set; }

        [JsonProperty("can_move_forward")]
        public bool CanMoveForward { get; set; }

        [JsonProperty("can_move_back")]
        public bool CanMoveBack { get; set; }
    }
}
