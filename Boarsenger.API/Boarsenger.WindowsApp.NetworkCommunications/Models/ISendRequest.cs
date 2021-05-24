using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.NetworkCommunications.Models
{
    public interface ISendRequest
    {
        public Uri URL { get; }

        public RESTMETHOD Restmethod { get; }

        public string PayLoad { get; }
    }
}
