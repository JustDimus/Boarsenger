using Boarsenger.Libraries.Telemetry.Models;
using Boarsenger.Libraries.Telemetry.Parser;
using Boarsenger.WindowsApp.BoarsengerManager.Models;
using Boarsenger.WindowsApp.NetworkCommunications.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.BoarsengerManager.SendRequest
{
    public class SetAccountDataRequest : ISendRequest
    {
        public SetAccountDataRequest(
            Models.AccountToken accountToken,
            AccountProfileData accountProfileData)
        {
            PayLoad = JsonParser.ParseToString(new FullAccountData()
            {
                AccountInfo = new AccountInfo()
                {
                    Age = accountProfileData.Age,
                    Email = accountProfileData.Email,
                    FirstName = accountProfileData.FirstName,
                    Password = accountProfileData.Password,
                    SecondName = accountProfileData.SecondName
                },
                AccountToken = new Libraries.Telemetry.Models.AccountToken()
                {
                    Email = accountToken.Email,
                    Token = accountToken.Token
                }
            });
        }

        public string URL => "api/accountapi/setaccountdata";

        public RESTMETHOD Restmethod => RESTMETHOD.POST;

        public string PayLoad { get; private set; }
    }
}
