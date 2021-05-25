using Boarsenger.Libraries.Telemetry.Parser;
using Boarsenger.WindowsApp.BoarsengerManager.Models;
using Boarsenger.WindowsApp.BoarsengerManager.SendRequest;
using Boarsenger.WindowsApp.BoarsengerManager.Services;
using Boarsenger.WindowsApp.BoarsengerManager.Services.Implementation;
using Boarsenger.WindowsApp.NetworkCommunications.Models;
using Boarsenger.WindowsApp.NetworkCommunications.Services;
using Boarsenger.WindowsApp.NetworkCommunications.Services.Implementation;
using Boarsenger.WindowsApp.SystemManager.RegistryHelper;
using Boarsenger.WindowsApp.SystemManager.RegistryHelper.Implementations;
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

        private ReplaySubject<AccountAuthorizationData> accountAuthorizationDataObservable = new ReplaySubject<AccountAuthorizationData>();

        public IAuthorizationService AuthorizationService => this.authorizationService;

        public IRequestSender requestSender;

        public IObservable<AccountAuthorizationData> AccountTokenObservable => this.accountAuthorizationDataObservable;

        public BoarsengerManager()
        {
            IServiceCollection collection = new ServiceCollection();

            collection.AddTransient<IRegistryManager, RegistryManager>();
            collection.AddSingleton<IAuthorizationService, AuthorizationService>();
            collection.AddTransient<IBoarsengerManager>(c => this);
            collection.AddTransient<IRequestConsumer, RequestConsumer>();
            collection.AddTransient<IRequestSender, RequestSender>();

            IServiceProvider provider = collection.BuildServiceProvider();

            this.authorizationService = provider.GetRequiredService<IAuthorizationService>();
            this.requestSender = provider.GetRequiredService<IRequestSender>();
        }

        public async Task<bool> TryLogInAsync(AccountCreditionals accountCreditionals)
        {
            var response = await this.requestSender.SendRequestAsync(new LoginRequest(new Libraries.Telemetry.Models.AccountCreditionals()
            {
                Email = accountCreditionals.Login,
                Password = accountCreditionals.Password
            }));

            if (response?.StatusCode != 200)
            {
                return false;
            }

            var token = JsonParser.ParseToObject<Libraries.Telemetry.Models.AccountToken>(
                    response.Message);

            AccountAuthorizationData authData = new AccountAuthorizationData()
            {
                AccountCreditionals = accountCreditionals,
                AccountToken = new AccountToken()
                {
                    Email = token.Email,
                    Token = token.Token
                }
            };

            this.accountAuthorizationDataObservable.OnNext(authData);

            return true;
        }

        public async Task<bool> TryRegisterAsync(AccountCreditionals accountCreditionals)
        {
            var response = await this.requestSender.SendRequestAsync(new RegisterRequest(new Libraries.Telemetry.Models.AccountCreditionals()
            {
                Email = accountCreditionals.Login,
                Password = accountCreditionals.Password
            }));

            if (response?.StatusCode != 200)
            {
                return false;
            }

            var token = JsonParser.ParseToObject<Libraries.Telemetry.Models.AccountToken>(
                    response.Message);

            AccountAuthorizationData authData = new AccountAuthorizationData()
            {
                AccountCreditionals = accountCreditionals,
                AccountToken = new AccountToken()
                {
                    Email = token.Email,
                    Token = token.Token
                }
            };

            this.accountAuthorizationDataObservable.OnNext(authData);

            return true;
        }

        #region Dispose

        public void Dispose()
        {
            this.accountAuthorizationDataObservable.Dispose();
            this.authorizationService.Dispose();
        }

        #endregion
    }
}
