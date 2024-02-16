using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.NetworkCommunications.Models
{
    public class RequestResult : IRequestResult
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
