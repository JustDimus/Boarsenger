using Boarsenger.WindowsApp.BoarsengerManager.BoarsengerManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.UI.Services.Implementation
{
    public class SubscriptionService : ISubscriptionService
    {
        public IBoarsengerManager BoarsengerManager { get; private set; }

        public SubscriptionService(
            IBoarsengerManager BoarsengerManager)
        {
            this.BoarsengerManager = BoarsengerManager;

        }

        public void Dispose()
        {
            this.BoarsengerManager?.Dispose();
        }
    }
}
