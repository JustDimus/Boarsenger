using Boarsenger.Libraries.Telemetry.Models;
using Boarsenger.WindowsApp.BoarsengerManager.BoarsengerManager;
using Boarsenger.WindowsApp.UI.Commands;
using Boarsenger.WindowsApp.UI.Navigation;
using Boarsenger.WindowsApp.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
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
            this.UpdateCommand = new RelayCommand(Update, (obj) => true);
            this.CreateServerCommand = new RelayCommand(CreateServer, (obj) => true);

            this.navigationService = navigationService;
            this.boarsengerManager = boarsengerManager;
        }

        public ICommand LogOutCommand { get; private set; }

        public List<ServerData> ServerData { get; private set; } = new List<ServerData>();

        public ICommand UpdateCommand { get; private set; }

        public ICommand CreateServerCommand { get; private set; }

        public string ServerTitle { get; set; }

        public string ServerIP { get; set; }

        public void OnPageLoaded()
        {
            Update(null);
        }

        public void OnPageUnloaded()
        {
            
        }

        private void LogOut(object obj)
        {
            this.boarsengerManager.AuthorizationService.LogOut();

            this.navigationService.NavigateTo(Page.Login);
        }

        private void CreateServer(object obj)
        {
            if (string.IsNullOrEmpty(ServerTitle) || string.IsNullOrEmpty(ServerIP))
            {
                return;
            }

            var result = this.boarsengerManager.CreateServer(new BoarsengerManager.Models.ServerData()
            {
                Title = ServerTitle,
                ServerIP = ServerIP
            }).GetAwaiter().GetResult();

            if (result == null)
            {
                return;
            }

            ServerIP = string.Empty;
            ServerTitle = string.Empty;

            OnPropertyChanged(nameof(ServerTitle));

            OnPropertyChanged(nameof(ServerIP));

            Update(null);
        }

        private void Update(object obj)
        {
            var result = this.boarsengerManager.GetServerPage().GetAwaiter().GetResult();

            if (result != null)
            {
                this.ServerData = result.ServerList;
                OnPropertyChanged(nameof(ServerData));
            }
        }
    }
}
