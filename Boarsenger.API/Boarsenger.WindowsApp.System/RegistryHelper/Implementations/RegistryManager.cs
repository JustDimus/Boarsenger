using Microsoft.VisualStudio.Services.Common.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.WindowsApp.System.RegistryHelper.Implementations
{
    public class RegistryManager : IRegistryManager
    {
        public void GetValue(string hive, string cell, out int result)
        {
            result = -1;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(hive);

            if (key == null)
            {
                throw new ArgumentException();
            }

            try
            {
                object obj = key.GetValue(cell);

                if (!int.TryParse(obj.ToString(), out result))
                {
                    throw new FormatException();
                }
            }
            finally
            {
                key.Close();
            }
        }

        public void GetValue(string hive, string cell, out string result)
        {
            result = null;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(hive);

            if (key == null)
            {
                throw new ArgumentException();
            }

            try
            {
                result = key.GetValue(cell).ToString() ?? throw new FormatException();
            }
            finally
            {
                key.Close();
            }
        }

        public void GetValue(string hive, string cell, out bool result)
        {
            result = false;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(hive);

            if (key == null)
            {
                throw new ArgumentException();
            }

            try
            {
                object obj = key.GetValue(cell);

                if (obj == null)
                {
                    throw new ArgumentException();
                }

                result = obj.ToString().ToString().ToLower() == "true";
            }
            finally
            {
                key.Close();
            }
        }

        public void SetValue(string hive, string cell, int result)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(hive);

            try
            {
                key.SetValue(cell, result);
            }
            finally
            {
                key.Close();
            }
        }

        public void SetValue(string hive, string cell, string result)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(hive);

            try
            {
                key.SetValue(cell, result);
            }
            finally
            {
                key.Close();
            }
        }

        public void SetValue(string hive, string cell, bool result)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(hive);

            try
            {
                key.SetValue(cell, result);
            }
            finally
            {
                key.Close();
            }
        }
    }
}
