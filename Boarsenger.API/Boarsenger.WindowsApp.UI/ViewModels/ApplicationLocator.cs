using Boarsenger.WindowsApp.UI.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.UI.ViewModels
{
    public class ApplicationLocator
    {


        public MainWindowViewModel MainWindowViewModel { get; private set; }

        public LoginViewModel LoginViewModel { get; private set; }

        public RegisterViewModel RegisterViewModel { get; private set; }

        public ServerViewModel ServerViewModel { get; private set; }

        public HomeViewModel HomeViewModel { get; private set; }
    }
}
