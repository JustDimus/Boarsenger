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
    public class RegisterViewModel : ViewModelBase, IPageViewModel
    {
        private INavigationService navigationService;
        private IBoarsengerManager boarsengerManager;

        public RegisterViewModel(
            INavigationService navigationService,
            IBoarsengerManager boarsengerManager)
        {
            this.SwitchToLoginCommand = new RelayCommand(GotoLoginPageAction, (obj) => true);
            this.RegisterCommand = new RelayCommand(RegisterAction, (obj) => true);

            this.boarsengerManager = boarsengerManager;
            this.navigationService = navigationService;
        }

        public string Login { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; private set; }

        public ICommand SwitchToLoginCommand { get; private set; }

        public ICommand RegisterCommand { get; private set; }

        public void OnPageLoaded()
        {

        }

        public void OnPageUnloaded()
        {

        }

        private void RegisterAction(object obj)
        {
            if (string.IsNullOrEmpty(Login) 
                || string.IsNullOrEmpty(Password)
                || string.IsNullOrEmpty(ConfirmPassword))
            {
                this.ErrorMessage = "Заполните все поля!";
                OnPropertyChanged(nameof(ErrorMessage));
            }

            if (Password != ConfirmPassword)
            {
                this.ErrorMessage = "Пароли не совпадают!";
                OnPropertyChanged(nameof(ErrorMessage));
            }

            var result = this.boarsengerManager.TryRegisterAsync(new AccountCreditionals()
            {
                Login = this.Login,
                Password = this.Password
            }).GetAwaiter().GetResult();

            this.ErrorMessage = result ? "Успех" : "Ошибка";
        }

        private void GotoLoginPageAction(object obj)
        {
            this.navigationService.NavigateTo(Page.Login);
        }
    }
}
