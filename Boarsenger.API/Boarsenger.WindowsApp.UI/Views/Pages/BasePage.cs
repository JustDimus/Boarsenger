using Boarsenger.WindowsApp.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Boarsenger.WindowsApp.UI.Views.Pages
{
    [ComVisible(false)]
    public class BasePage: Page
    {
        public BasePage()
        {
            Loaded += BasePage_Loaded;
            Unloaded += BasePage_Unloaded;
        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IPageViewModel pageViewModel)
            {
                pageViewModel.OnPageLoaded();
            }
        }

        private void BasePage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IPageViewModel pageViewModel)
            {
                pageViewModel.OnPageUnloaded();
            }
        }
    }
}
