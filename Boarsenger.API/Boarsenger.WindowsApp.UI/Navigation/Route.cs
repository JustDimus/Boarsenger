using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.UI.Navigation
{
    public class Route
    {
        public Route() { }

        public Route(Page page, string pathToPage)
        {
            this.Page = page;
            this.PathToPage = pathToPage;
        }

        public Page Page { get; set; }

        public string PathToPage { get; set; }
    }
}
