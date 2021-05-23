using Boarsenger.WindowsApp.UI.Navigation;
using Boarsenger.WindowsApp.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private List<Route> resourceDictionary = new List<Route>()
        {
            new Route(Page.Login, "Pages/LoginPage.xaml"),
            new Route(Page.Register, "Pages/RegisterPage.xaml"),
            new Route(Page.Server, "Pages/ServerPage.xaml"),
            new Route(Page.Home, "Pages/HomePage.xaml")
        };

        public MainWindowViewModel()
        {

        }

        public string PathToView { get; private set; }
    }
}
