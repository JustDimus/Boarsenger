using Boarsenger.WindowsApp.BoarsengerManager.BoarsengerManager;
using Boarsenger.WindowsApp.UI.Commands;
using Boarsenger.WindowsApp.UI.Navigation;
using Boarsenger.WindowsApp.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Boarsenger.WindowsApp.UI.ViewModels.Pages
{
    public class HomeViewModel : ViewModelBase, IPageViewModel
    {
        private INavigationService navigationService;

        private IBoarsengerManager boarsengerManager;

        public HomeViewModel(
            INavigationService navigationService,
            IBoarsengerManager boarsengerManager)
        {
            this.LogOutCommand = new RelayCommand(LogOut, (obj) => true);

            this.navigationService = navigationService;
            this.boarsengerManager = boarsengerManager;

        }

        public ICommand LogOutCommand { get; private set; }

        public void OnPageLoaded()
        {

        }

        public void OnPageUnloaded()
        {
            
        }

        private void LogOut(object obj)
        {
            this.boarsengerManager.AuthorizationService.LogOut();

            this.navigationService.NavigateTo(Page.Login);
        }
    }
}
