using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace Boarsenger.WindowsApp.UI.Navigation
{
    public class NavigationService : INavigationService
    {
        private Page prevPage;
        private Page currentPage;

        private ReplaySubject<Page> currentPageKey = new ReplaySubject<Page>();

        public NavigationService()
        {
            this.currentPage = Page.Login;
            this.currentPageKey.OnNext(Page.Login);
        }

        public IObservable<Page> CurrentPageKey => this.currentPageKey;

        public void GoBack()
        {
            this.currentPageKey.OnNext(prevPage);
        }

        public void NavigateTo(Page page)
        {
            this.prevPage = currentPage;
            this.currentPage = page;
            this.currentPageKey.OnNext(page);
        }
    }
}
