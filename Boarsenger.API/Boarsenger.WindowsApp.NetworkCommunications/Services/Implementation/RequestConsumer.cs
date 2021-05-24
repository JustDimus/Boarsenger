using Boarsenger.WindowsApp.NetworkCommunications.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
            var task = new Task<IRequestResult>(() => SendMessage(sendRequest), requestCancelToken);
            task.Start();
            return task;
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

            Uri currentUri = new Uri(string.Concat(this.baseUri, sendRequest.URL));
            var request = (HttpWebRequest)WebRequest.Create(currentUri);

            request.Method = restMethod;
            request.ContentType = "application/json";

            byte[] array = Encoding.UTF8.GetBytes(sendRequest.PayLoad);

            request.ContentLength = array.Length;

            var req = request.GetRequestStream();

            req.Write(array, 0, array.Length);

            req.Close();

            HttpWebResponse response = null;

            string error = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }

            RequestResult result = new RequestResult();

            result.StatusCode = (int)response.StatusCode;

            var responseMessage = response.GetResponseStream();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result.Message =  reader.ReadToEnd();
            }

            return result;
        }
    }
}
