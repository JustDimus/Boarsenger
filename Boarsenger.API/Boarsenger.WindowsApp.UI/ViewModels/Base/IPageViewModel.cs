using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.UI.ViewModels.Base
{
    public interface IPageViewModel
    {
        void OnPageLoaded();
        void OnPageUnloaded();
    }
}
