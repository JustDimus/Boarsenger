using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.BLL.Service
{
    public interface IServiceResult
    {
        public bool IsSuccessful { get; set; }

        public string ResultMessage { get; set; }
    }

    public interface IServiceResult<T> : IServiceResult
    {
        public T Result { get; set; }
    }
}
