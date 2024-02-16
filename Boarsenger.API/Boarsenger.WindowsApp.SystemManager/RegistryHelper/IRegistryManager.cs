using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.SystemManager.RegistryHelper
{
    public interface IRegistryManager
    {
        void GetValue(string hive, string cell, out int result);

        void GetValue(string hive, string cell, out string result);

        void GetValue(string hive, string cell, out bool result);

        void SetValue(string hive, string cell, int result);

        void SetValue(string hive, string cell, string result);

        void SetValue(string hive, string cell, bool result);
    }
}
