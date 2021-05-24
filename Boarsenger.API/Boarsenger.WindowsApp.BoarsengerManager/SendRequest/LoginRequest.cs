using Boarsenger.Libraries.Telemetry.Models;
using Boarsenger.Libraries.Telemetry.Parser;
using Boarsenger.WindowsApp.NetworkCommunications.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.BoarsengerManager.SendRequest
{
    public class LoginRequest : ISendRequest
    {
        public LoginRequest(AccountCreditionals creditionals)
        {
            this.PayLoad = JsonParser.ParseToString(creditionals);
        }

        public string URL => "api/account/login";

        public RESTMETHOD Restmethod => RESTMETHOD.POST;

        public string PayLoad { get; private set; }
    }
}
