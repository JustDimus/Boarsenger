using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.NetworkCommunications.Models
{
    public interface IRequestResult
    {
        int StatusCode { get; }

        string Message { get; }
    }
}
