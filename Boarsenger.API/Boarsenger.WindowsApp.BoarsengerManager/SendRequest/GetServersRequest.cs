using Boarsenger.WindowsApp.NetworkCommunications.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.BoarsengerManager.SendRequest
{
    public class GetServersRequest : ISendRequest
    {
        public GetServersRequest()
        {
            PayLoad = string.Empty;
        }

        public string URL => "api/serverapi/getserverlist";

        public RESTMETHOD Restmethod => RESTMETHOD.POST;

        public string PayLoad { get; private set; }
    }
}
