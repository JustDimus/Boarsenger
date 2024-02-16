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

        Task<bool> TryLogInAsync(AccountCreditionals accountCreditionals);

        Task<bool> TryRegisterAsync(AccountCreditionals accountCreditionals);
    }
}
