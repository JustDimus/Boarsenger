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

        public LoginViewModel(
            INavigationService navigationService)
        {
            this.AuthorizeCommand = new RelayCommand(AuthorizeAction, (obj) => true);
            this.SwitchToRegisterCommand = new RelayCommand(GotoRegisterPageAction, (obj) => true);

            this.navigationService = navigationService;
        }


        public string Login { get; set; }

        public string Password { get; set; }

        public string ErrorMessage { get; private set; }

        public ICommand AuthorizeCommand { get; private set; }

        public ICommand SwitchToRegisterCommand { get; private set; }

        public void OnPageLoaded()
        {

        }

        public void OnPageUnloaded()
        {

        }


        private void GotoRegisterPageAction(object obj)
        {
            this.navigationService.NavigateTo(Page.Register);
        }

        private void AuthorizeAction(object obj)
        {
            if (string.IsNullOrEmpty(this.Login) || string.IsNullOrEmpty(this.Password))
            {
                this.ErrorMessage = "Заполните все поля!";
                OnPropertyChanged(nameof(ErrorMessage));
                return;
            }
        }
    }
}
