using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.BLL.Service.Implementations
{
    public class ServiceResult : IServiceResult
    {
        public bool IsSuccesful { get; set; }

        public string Message { get; set; }
    }

    public class ServiceResult<TResult> : ServiceResult, IServiceResult<TResult>
    {
        public TResult Result { get; set; }
    }
}
