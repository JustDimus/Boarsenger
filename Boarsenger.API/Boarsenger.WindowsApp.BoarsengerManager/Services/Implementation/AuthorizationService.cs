using Boarsenger.WindowsApp.BoarsengerManager.BoarsengerManager;
using Boarsenger.WindowsApp.BoarsengerManager.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Newtonsoft.Json;
using System.Text;
using Boarsenger.WindowsApp.SystemManager.RegistryHelper;
using Boarsenger.WindowsApp.SystemManager.Constants;

namespace Boarsenger.WindowsApp.BoarsengerManager.Services.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        private IBoarsengerManager boarsengerManager;
        private IRegistryManager registryManager;

        private ReplaySubject<AccountAuthorizationData> accountTokenSubject = new ReplaySubject<AccountAuthorizationData>();

        public AuthorizationService(
            IBoarsengerManager boarsengerManager,
            IRegistryManager registryManager)
        {
            this.boarsengerManager = boarsengerManager ?? throw new ArgumentNullException();
            this.registryManager = registryManager ?? throw new ArgumentNullException();

            this.boarsengerManager.AccountTokenObservable.Subscribe(OnNext);
        }

        public AccountToken AccountToken
        {
            get
            {
                this.registryManager.GetValue(RegistryConstants.SETTINGS_DIRECTORY, RegistryConstants.LOGIN, out string login);
                this.registryManager.GetValue(RegistryConstants.SETTINGS_DIRECTORY, RegistryConstants.TOKEN, out string token);

                AccountToken result = new AccountToken()
                {
                    Email = login,
                    Token = token
                };

                return result;
            }
        }

        public bool IsAuthorized
        {
            get
            {
                try
                {
                    this.registryManager.GetValue(RegistryConstants.SETTINGS_DIRECTORY, RegistryConstants.IS_AUTHORIZED, out bool result);

                    return result;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public AccountCreditionals AccountCreditionals
        {
            get
            {
                try
                {
                    this.registryManager.GetValue(RegistryConstants.SETTINGS_DIRECTORY, RegistryConstants.LOGIN, out string login);
                    this.registryManager.GetValue(RegistryConstants.SETTINGS_DIRECTORY, RegistryConstants.PASSWORD, out string password);

                    AccountCreditionals accountCreditionals = new AccountCreditionals()
                    {
                        Login = login,
                        Password = password
                    };

                    return accountCreditionals;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public void LogOut()
        {
            this.registryManager.SetValue(
                RegistryConstants.SETTINGS_DIRECTORY,
                RegistryConstants.IS_AUTHORIZED,
                false);
        }

        public void OnNext(AccountAuthorizationData accountAuthorizationData)
        {
            this.registryManager.SetValue(
                RegistryConstants.SETTINGS_DIRECTORY, 
                RegistryConstants.TOKEN, 
                accountAuthorizationData.AccountToken.Token);

            this.registryManager.SetValue(
                RegistryConstants.SETTINGS_DIRECTORY,
                RegistryConstants.IS_AUTHORIZED,
                true);

            this.registryManager.SetValue(
                RegistryConstants.SETTINGS_DIRECTORY,
                RegistryConstants.LOGIN,
                accountAuthorizationData.AccountToken.Email);

            this.registryManager.SetValue(
                RegistryConstants.SETTINGS_DIRECTORY,
                RegistryConstants.PASSWORD,
                accountAuthorizationData.AccountCreditionals.Password);

            this.accountTokenSubject.OnNext(accountAuthorizationData);
        }

        public IDisposable Subscribe(IObserver<AccountAuthorizationData> observer)
        {
            return this.accountTokenSubject.Subscribe(observer);
        }

        public void Dispose()
        {
            this.accountTokenSubject.Dispose();
        }
    }
}
