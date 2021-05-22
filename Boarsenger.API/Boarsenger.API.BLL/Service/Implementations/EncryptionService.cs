using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Boarsenger.API.BLL.Service.Implementations
{
    public class EncryptionService : IEncryptionService
    {
        public string Encrypt(string message)
        {
            var sha256 = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(message);

            var hash = sha256.ComputeHash(bytes);

            var hashedMessage = Convert.ToBase64String(hash);

            return hashedMessage;
        }
    }
}
