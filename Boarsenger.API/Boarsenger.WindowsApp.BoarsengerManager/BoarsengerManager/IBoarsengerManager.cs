using Boarsenger.Libraries.Telemetry.Models;
using Boarsenger.WindowsApp.BoarsengerManager.Models;
using Boarsenger.WindowsApp.BoarsengerManager.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.WindowsApp.BoarsengerManager.BoarsengerManager
{
    public interface IBoarsengerManager : IDisposable
    {
        IObservable<AccountAuthorizationData> AccountTokenObservable { get; }

        IAuthorizationService AuthorizationService { get; }

        Task<bool> TryLogInAsync(Models.AccountCreditionals accountCreditionals);

        Task<bool> TryRegisterAsync(Models.AccountCreditionals accountCreditionals);

        Task<ServerPageData> GetServerPage();

        Task<AccountInfo> GetAccountInfo();

        Task<bool> SetAccountInfo(AccountProfileData accountProfileData);

        Task<ServerInfo> CreateServer(Models.ServerData serverData);
    }
}
