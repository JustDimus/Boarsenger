using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.BoarsengerManager.Models
{
    public class ServerData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxUserCount { get; set; }
        public bool IsAdultOnly { get; set; }
        public string ServerIP { get; set; }
    }
}
