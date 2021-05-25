using Boarsenger.WindowsApp.BoarsengerManager.BoarsengerManager;
using Boarsenger.WindowsApp.BoarsengerManager.Models;
using Boarsenger.WindowsApp.UI.Commands;
using Boarsenger.WindowsApp.UI.Navigation;
using Boarsenger.WindowsApp.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Boarsenger.WindowsApp.UI.ViewModels.Pages
{
    public class LoginViewModel : ViewModelBase, IPageViewModel
    {
        private INavigationService navigationService;
        private IBoarsengerManager boarsengerManager;

        public LoginViewModel(
            INavigationService navigationService,
            IBoarsengerManager boarsengerManager)
        {
            this.AuthorizeCommand = new RelayCommand(AuthorizeAction, (obj) => true);
            this.SwitchToRegisterCommand = new RelayCommand(GotoRegisterPageAction, (obj) => true);

            this.navigationService = navigationService;
            this.boarsengerManager = boarsengerManager;
        }

        public string Login { get; set; }

        public string Password { get; set; }

        public string ErrorMessage { get; private set; }

        public ICommand AuthorizeCommand { get; private set; }

        public ICommand SwitchToRegisterCommand { get; private set; }

        public void OnPageLoaded()
        {
            bool isAuthorized = this.boarsengerManager.AuthorizationService.IsAuthorized;

            if (isAuthorized)
            {
                this.navigationService.NavigateTo(Page.Home);
                return;
            }

            this.Login = this.boarsengerManager.AuthorizationService.AccountCreditionals.Login;
            this.Password = this.boarsengerManager.AuthorizationService.AccountCreditionals.Password;

            OnPropertyChanged(nameof(this.Login));
            OnPropertyChanged(nameof(this.Password));
        }

        public void OnPageUnloaded()
        {

        }

        private void GotoRegisterPageAction(object obj)
        {
            this.navigationService.NavigateTo(Page.Register);
        }

        private async void AuthorizeAction(object obj)
        {
            if (string.IsNullOrEmpty(this.Login) || string.IsNullOrEmpty(this.Password))
            {
                this.ErrorMessage = "Заполните все поля!";
                OnPropertyChanged(nameof(ErrorMessage));
                return;
            }

            var result = await this.boarsengerManager.TryLogInAsync(new AccountCreditionals()
            {
                Login = this.Login,
                Password = this.Password
            });

            this.ErrorMessage = result ? "Успех" : "Ошибка";

            OnPropertyChanged(nameof(this.ErrorMessage));

            if (result)
            {
                this.navigationService.NavigateTo(Page.Home);
            }
        }
    }
}
