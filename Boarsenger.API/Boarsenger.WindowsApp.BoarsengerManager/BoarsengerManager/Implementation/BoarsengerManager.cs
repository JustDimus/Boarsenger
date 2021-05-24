using Boarsenger.WindowsApp.BoarsengerManager.Models;
using Boarsenger.WindowsApp.BoarsengerManager.Services;
using Boarsenger.WindowsApp.BoarsengerManager.Services.Implementation;
using Boarsenger.WindowsApp.System.RegistryHelper;
using Boarsenger.WindowsApp.System.RegistryHelper.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.WindowsApp.BoarsengerManager.BoarsengerManager.Implementation
{
    public class BoarsengerManager : IBoarsengerManager
    {
        private IAuthorizationService authorizationService;

        private ReplaySubject<AccountAuthorizationData> accountTokenObservable = new ReplaySubject<AccountAuthorizationData>();

        public IAuthorizationService AuthorizationService => this.authorizationService;

        public IObservable<AccountAuthorizationData> AccountTokenObservable => this.accountTokenObservable;

        public BoarsengerManager()
        {
            IServiceCollection collection = new ServiceCollection();

            collection.AddTransient<IRegistryManager, RegistryManager>();
            collection.AddSingleton<IAuthorizationService, AuthorizationService>();
            collection.AddTransient<IBoarsengerManager>(c => this);

            IServiceProvider provider = collection.BuildServiceProvider();

            this.authorizationService = provider.GetRequiredService<IAuthorizationService>();
        }


        public Task<bool> TryLogInAsync(AccountCreditionals accountCreditionals)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryRegisterAsync(AccountCreditionals accountCreditionals)
        {
            throw new NotImplementedException();
        }

        #region Dispose

        public void Dispose()
        {
            this.accountTokenObservable.Dispose();
            this.authorizationService.Dispose();
        }

        #endregion
    }
}
