using Boarsenger.WindowsApp.NetworkCommunications.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boarsenger.WindowsApp.NetworkCommunications.Services
{
    public interface IRequestConsumer
    {
        Task<IRequestResult> AddRequestAsync(ISendRequest sendRequest, CancellationToken requestCancelToken);

    }
}
