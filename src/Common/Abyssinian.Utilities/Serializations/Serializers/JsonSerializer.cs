using Abyssinian.Utilities.Serializations.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Utilities.Serializations.Serializers
{
    public class JsonSerializer : ISerializer<string>
    {
        public T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public object Deserialize(string content)
        {
            return JsonConvert.DeserializeObject(content);
        }

        public string Serialize(object content)
        {
            return JsonConvert.SerializeObject(content);
        }
    }
}
