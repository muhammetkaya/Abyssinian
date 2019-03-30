using Abyssinian.Utilities.Serializations.Interfaces;
using Abyssinian.Utilities.Serializations.Serializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Utilities.Serializations
{
    public static class Serializer
    {
        public static ISerializer<string> JSON { get; set; } = new JsonSerializer();
        
    }
}
