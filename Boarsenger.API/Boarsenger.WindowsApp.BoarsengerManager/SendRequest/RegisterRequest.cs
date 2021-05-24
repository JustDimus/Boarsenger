using Boarsenger.Libraries.Telemetry.Models;
using Boarsenger.Libraries.Telemetry.Parser;
using Boarsenger.WindowsApp.NetworkCommunications.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.BoarsengerManager.SendRequest
{
    public class RegisterRequest : ISendRequest
    {
        public RegisterRequest(AccountCreditionals creditionals)
        {
            this.PayLoad = JsonParser.ParseToString(creditionals);
        }

        public Uri URL => new Uri("api/account/register");

        public RESTMETHOD Restmethod => RESTMETHOD.POST;

        public string PayLoad { get; private set; }
    }
}
