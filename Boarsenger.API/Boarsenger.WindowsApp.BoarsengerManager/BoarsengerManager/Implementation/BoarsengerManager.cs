using Boarsenger.Libraries.Telemetry.Parser;
using Boarsenger.WindowsApp.BoarsengerManager.Models;
using Boarsenger.WindowsApp.BoarsengerManager.SendRequest;
using Boarsenger.WindowsApp.BoarsengerManager.Services;
using Boarsenger.WindowsApp.BoarsengerManager.Services.Implementation;
using Boarsenger.WindowsApp.NetworkCommunications.Services;
using Boarsenger.WindowsApp.NetworkCommunications.Services.Implementation;
using Boarsenger.WindowsApp.System.RegistryHelper;
using Boarsenger.WindowsApp.System.RegistryHelper.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boarsenger.WindowsApp.BoarsengerManager.BoarsengerManager.Implementation
{
    public class BoarsengerManager : IBoarsengerManager
    {
        private IAuthorizationService authorizationService;

        private ReplaySubject<AccountAuthorizationData> accountTokenObservable = new ReplaySubject<AccountAuthorizationData>();

        public IAuthorizationService AuthorizationService => this.authorizationService;

        public IRequestConsumer requestConsumer;

        public IObservable<AccountAuthorizationData> AccountTokenObservable => this.accountTokenObservable;

        public BoarsengerManager()
        {
            IServiceCollection collection = new ServiceCollection();

            collection.AddTransient<IRegistryManager, RegistryManager>();
            collection.AddSingleton<IAuthorizationService, AuthorizationService>();
            collection.AddTransient<IBoarsengerManager>(c => this);
            collection.AddTransient<IRequestConsumer, RequestConsumer>();

            IServiceProvider provider = collection.BuildServiceProvider();

            this.authorizationService = provider.GetRequiredService<IAuthorizationService>();
            this.requestConsumer = provider.GetRequiredService<IRequestConsumer>();
        }


        public async Task<bool> TryLogInAsync(AccountCreditionals accountCreditionals)
        {
            using (CancellationTokenSource source = new CancellationTokenSource(30000))
            {
                var result = await this.requestConsumer.AddRequestAsync(new LoginRequest(new Libraries.Telemetry.Models.AccountCreditionals()
                {
                    Email = accountCreditionals.Login,
                    Password = accountCreditionals.Password
                }), source.Token);

                if (result.StatusCode != 200)
                {
                    return false;
                }

                Libraries.Telemetry.Models.AccountToken token = JsonParser.ParseToObject<Libraries.Telemetry.Models.AccountToken>(
                    result.Message);

                AccountAuthorizationData authData = new AccountAuthorizationData()
                {
                    AccountCreditionals = accountCreditionals,
                    AccountToken = new AccountToken()
                    {
                        Email = token.Email,
                        Token = token.Token
                    }
                };

                this.accountTokenObservable.OnNext(authData);

                return true;
            }
        }

        public async Task<bool> TryRegisterAsync(AccountCreditionals accountCreditionals)
        {
            using (CancellationTokenSource source = new CancellationTokenSource(30000))
            {
                var result = await this.requestConsumer.AddRequestAsync(new RegisterRequest(new Libraries.Telemetry.Models.AccountCreditionals()
                {
                    Email = accountCreditionals.Login,
                    Password = accountCreditionals.Password
                }), source.Token);

                if (result.StatusCode != 200)
                {
                    return false;
                }

                Libraries.Telemetry.Models.AccountToken token = JsonParser.ParseToObject<Libraries.Telemetry.Models.AccountToken>(
                    result.Message);

                AccountAuthorizationData authData = new AccountAuthorizationData()
                {
                    AccountCreditionals = accountCreditionals,
                    AccountToken = new AccountToken()
                    {
                        Email = token.Email,
                        Token = token.Token
                    }
                };

                this.accountTokenObservable.OnNext(authData);

                return true;
            }
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
