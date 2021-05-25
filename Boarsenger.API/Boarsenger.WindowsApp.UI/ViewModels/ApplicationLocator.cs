using Boarsenger.WindowsApp;
using Boarsenger.WindowsApp.UI.Navigation;
using Boarsenger.WindowsApp.UI.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.UI.ViewModels
{
    public class ApplicationLocator
    {
        public ApplicationLocator()
        {
            IServiceCollection collection = new ServiceCollection();

            collection.AddSingleton<INavigationService, NavigationService>();
            collection.AddTransient<BoarsengerManager.BoarsengerManager.IBoarsengerManager,
                BoarsengerManager.BoarsengerManager.Implementation.BoarsengerManager>();
            collection.AddTransient<MainWindowViewModel>();
            collection.AddTransient<LoginViewModel>();
            collection.AddTransient<RegisterViewModel>();
            collection.AddTransient<ServerViewModel>();
            collection.AddTransient<HomeViewModel>();

            IServiceProvider provider = collection.BuildServiceProvider();

            MainWindowViewModel = provider.GetRequiredService<MainWindowViewModel>();
            LoginViewModel = provider.GetRequiredService<LoginViewModel>();
            RegisterViewModel = provider.GetRequiredService<RegisterViewModel>();
            ServerViewModel = provider.GetRequiredService<ServerViewModel>();
            HomeViewModel = provider.GetRequiredService<HomeViewModel>();
        }

        public MainWindowViewModel MainWindowViewModel { get; private set; }

        public LoginViewModel LoginViewModel { get; private set; }

        public RegisterViewModel RegisterViewModel { get; private set; }

        public ServerViewModel ServerViewModel { get; private set; }

        public HomeViewModel HomeViewModel { get; private set; }
    }
}
