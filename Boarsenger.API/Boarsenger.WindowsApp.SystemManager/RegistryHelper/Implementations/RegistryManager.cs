using Boarsenger.WindowsApp.SystemManager.Constants;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.SystemManager.RegistryHelper.Implementations
{
    public class RegistryManager : IRegistryManager
    {
        public void GetValue(string hive, string cell, out int result)
        {
            this.GetValueBase(hive, cell, out object obj);

            if (obj == null)
            {
                result = -1;
                return;
            }

            int.TryParse(obj.ToString(), out result);
        }

        public void GetValue(string hive, string cell, out string result)
        {
            this.GetValueBase(hive, cell, out object obj);

            if (obj == null)
            {
                result = null;
            }

            result = obj.ToString();
        }

        public void GetValue(string hive, string cell, out bool result)
        {
            this.GetValueBase(hive, cell, out object obj);

            if (obj == null)
            {
                result = false;
            }

            result = obj.ToString().ToString().ToLower() == "true";
        }

        public void SetValue(string hive, string cell, int result)
        {
            this.SetValueBase(hive, cell, result.ToString());
        }

        public void SetValue(string hive, string cell, string result)
        {
            this.SetValueBase(hive, cell, result);
        }

        public void SetValue(string hive, string cell, bool result)
        {
            this.SetValueBase(hive, cell, result.ToString());
        }

        private void SetValueBase(string hive, string cell, string value)
        {
            RegistryKey mainKey = Registry.CurrentUser.OpenSubKey(RegistryConstants.MAIN_DIRECTORY, true);

            if (mainKey == null)
            {
                mainKey = Registry.CurrentUser.CreateSubKey(cell);
            }

            RegistryKey subKey = mainKey.OpenSubKey(hive, true);

            if (subKey == null)
            {
                subKey = mainKey.CreateSubKey(hive);
            }

            try
            {
                subKey.SetValue(cell, value);
            }
            finally
            {
                subKey.Close();
                mainKey.Close();
            }
        }

        private void GetValueBase(string hive, string cell, out object result)
        {
            RegistryKey mainKey = Registry.CurrentUser.OpenSubKey(RegistryConstants.MAIN_DIRECTORY);

            if (mainKey == null)
            {
                result = null;
                return;
            }

            RegistryKey subKey = mainKey.OpenSubKey(hive);

            if (subKey == null)
            {
                result = null;
                return;
            }

            try
            {
                result = subKey.GetValue(cell);
                return;
            }
            finally
            {
                subKey.Close();
                mainKey.Close();
            }
        }
    }
}
