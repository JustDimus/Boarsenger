using Boarsenger.WindowsApp.BoarsengerManager.BoarsengerManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.UI.Services
{
    public interface ISubscriptionService : IDisposable
    {
        IBoarsengerManager BoarsengerManager { get; }
    }
}
