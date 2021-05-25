using Boarsenger.Libraries.Telemetry.Models;
using Boarsenger.Libraries.Telemetry.Parser;
using Boarsenger.WindowsApp.BoarsengerManager.Models;
using Boarsenger.WindowsApp.NetworkCommunications.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.BoarsengerManager.SendRequest
{
    public class CreateServerRequest : ISendRequest
    {
        public CreateServerRequest(Models.ServerData serverData, Models.AccountToken accountToken)
        {
            PayLoad = JsonParser.ParseToString(new CreateServerData()
            {
                ServerOwnerData = new ServerOwnerData()
                {
                    AccountToken = new Libraries.Telemetry.Models.AccountToken()
                    {
                        Email = accountToken.Email,
                        Token = accountToken.Token
                    }
                },
                ServerData = new Libraries.Telemetry.Models.ServerData()
                {
                    Description = serverData.Description,
                    IsAdultOnly = serverData.IsAdultOnly,
                    MaxUserCount = serverData.MaxUserCount,
                    ServerIP = serverData.ServerIP,
                    Title = serverData.Title
                }
            });
        }

        public string URL => "api/serverapi/createserver";

        public RESTMETHOD Restmethod => RESTMETHOD.POST;

        public string PayLoad { get; private set; }
    }
}
