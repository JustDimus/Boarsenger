using Boarsenger.WindowsApp.UI.Navigation;
using Boarsenger.WindowsApp.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private INavigationService navigationService;

        public MainWindowViewModel(
            INavigationService navigationService)
        {
            this.navigationService = navigationService;

            this.navigationService.CurrentPageKey.Subscribe((page) =>
            {
                if (this.resourceDictionary.Any(c => c.Page == page))
                {
                    string newPathToView = this.resourceDictionary.FirstOrDefault(c => c.Page == page)?.PathToPage;
                    if (newPathToView != this.PathToView)
                    {
                        PathToView = newPathToView;

                        OnPropertyChanged(nameof(PathToView));
                    }
                }
            });
        }

        public string PathToView { get; private set; }
    }
}
