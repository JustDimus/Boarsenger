using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.UI.Navigation
{
    public interface INavigationService
    {
        void NavigateTo(Page page);

        void GoBack();

        IObservable<Page> CurrentPageKey { get; }
    }
}
