using Boarsenger.Libraries.Telemetry.Parser;
using Boarsenger.WindowsApp.BoarsengerManager.Models;
using Boarsenger.WindowsApp.NetworkCommunications.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.BoarsengerManager.SendRequest
{
    public class GetAccountDataRequest : ISendRequest
    {
        public GetAccountDataRequest(AccountToken accountToken)
        {
            PayLoad = JsonParser.ParseToString(new Boarsenger.Libraries.Telemetry.Models.AccountToken()
            {
                Token = accountToken.Token,
                Email = accountToken.Email
            });
        }

        public string URL => "api/accountapi/getaccountdata";

        public RESTMETHOD Restmethod => RESTMETHOD.POST;

        public string PayLoad { get; private set; }
    }
}
