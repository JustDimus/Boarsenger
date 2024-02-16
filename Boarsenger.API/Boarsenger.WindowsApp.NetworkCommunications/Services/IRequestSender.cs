using Boarsenger.Libraries.Telemetry.Models;
using Boarsenger.WindowsApp.NetworkCommunications.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boarsenger.WindowsApp.NetworkCommunications.Services
{
    public interface IRequestSender
    {
        Task<IRequestResult> SendRequestAsync(ISendRequest request);
    }
}
