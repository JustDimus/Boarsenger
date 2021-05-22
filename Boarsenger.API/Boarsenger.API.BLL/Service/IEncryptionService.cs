using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.BLL.Service
{
    public interface IEncryptionService
    {
        string Encrypt(string message);
    }
}
