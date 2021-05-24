using Boarsenger.WindowsApp.NetworkCommunications.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boarsenger.WindowsApp.NetworkCommunications.Services.Implementation
{
    public class RequestConsumer : IRequestConsumer
    {
        private string baseUri;

        public RequestConsumer()
        {
            this.baseUri = ConfigurationManager.AppSettings["api.uri"];
        }

        public Task<IRequestResult> AddRequestAsync(
            ISendRequest sendRequest, 
            CancellationToken requestCancelToken)
        {
            return new Task<IRequestResult>(() => SendMessage(sendRequest), requestCancelToken);
        }

        private IRequestResult SendMessage(ISendRequest sendRequest)
        {
            if (sendRequest == null)
            {
                throw new ArgumentNullException();
            }

            string restMethod = string.Empty;

            switch (sendRequest.Restmethod)
            {
                case RESTMETHOD.GET:
                    restMethod = "GET";
                    break;
                case RESTMETHOD.POST:
                    restMethod = "POST";
                    break;
                case RESTMETHOD.PUT:
                    restMethod = "PUT";
                    break;
            }

            Uri currentUri = new Uri(string.Concat(this.baseUri, sendRequest.URL.AbsoluteUri));

            var request = (HttpWebRequest)WebRequest.Create(currentUri);

            request.Method = restMethod;
            request.ContentType = "application/json";

            byte[] array = Encoding.Unicode.GetBytes(sendRequest.PayLoad);

            request.ContentLength = array.Length;

            request.GetRequestStream().Write(array);

            var response = (HttpWebResponse)request.GetResponse();

            RequestResult result = new RequestResult();

            result.StatusCode = (int)response.StatusCode;

            var responseMessage = response.GetResponseStream();

            byte[] responseResult = new byte[responseMessage.Length];

            responseMessage.Seek(0, System.IO.SeekOrigin.Begin);

            responseMessage.Read(responseResult, 0, (int)responseMessage.Length);

            result.Message = Encoding.Unicode.GetString(responseResult);

            return result;
        }
    }
}
