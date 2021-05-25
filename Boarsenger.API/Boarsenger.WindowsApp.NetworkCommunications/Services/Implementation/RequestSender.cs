using Boarsenger.Libraries.Telemetry.Models;
using Boarsenger.Libraries.Telemetry.Parser;
using Boarsenger.WindowsApp.NetworkCommunications.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boarsenger.WindowsApp.NetworkCommunications.Services.Implementation
{
    public class RequestSender : IRequestSender
    {
        private IRequestConsumer requestConsumer;

        public RequestSender(
            IRequestConsumer requestConsumer)
        {
            this.requestConsumer = requestConsumer;
        }

        public async Task<IRequestResult> SendRequestAsync(ISendRequest request)
        {
            using (CancellationTokenSource source = new CancellationTokenSource(30000))
            {
                IRequestResult requestResult = null;

                try
                {
                    requestResult = await this.requestConsumer.AddRequestAsync(request, source.Token);
                }
                catch (Exception ex)
                {
                    return null;
                }

                if (requestResult == null)
                {
                    return null;
                }

                ServerResult serverResult = JsonParser.ParseToObject<ServerResult>(requestResult.Message);

                return new RequestResult()
                {
                    StatusCode = serverResult.StatusCode,
                    Message = serverResult.Result
                };
            }
        }
    }
}
