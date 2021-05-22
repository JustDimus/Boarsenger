using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.Libraries.Telemetry.Parser
{
    public static class JsonParser
    {
        public static T ParseToObject<T>(string value)
        {
            return (T)JsonConvert.DeserializeObject(value);
        }

        public static string ParseToString<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity);
        }
    }
}
