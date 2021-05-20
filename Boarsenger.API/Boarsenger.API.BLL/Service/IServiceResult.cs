using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.BLL.Service
{
    public interface IServiceResult
    {
        bool IsSuccesful { get; }

        string Message { get; }
    }

    public interface IServiceResult<TResult> : IServiceResult
    {
        TResult Result { get; }
    }
}
