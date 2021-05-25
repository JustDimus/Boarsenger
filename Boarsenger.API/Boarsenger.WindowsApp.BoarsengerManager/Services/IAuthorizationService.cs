using Boarsenger.WindowsApp.BoarsengerManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.BoarsengerManager.Services
{
    public interface IAuthorizationService : IObservable<AccountAuthorizationData>, IDisposable
    {
        AccountToken AccountToken { get; }

        AccountCreditionals AccountCreditionals { get; }

        void LogOut();

        bool IsAuthorized { get; }
    }
}
