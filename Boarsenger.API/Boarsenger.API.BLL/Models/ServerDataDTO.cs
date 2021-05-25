using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.BLL.Models
{
    public class ServerDataDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsAdultOnly { get; set; }

        public string ServerIP { get; set; }

        public int MaxUserCount { get; set; }
    }
}
