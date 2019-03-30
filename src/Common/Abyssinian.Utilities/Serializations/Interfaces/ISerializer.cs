using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Utilities.Serializations.Interfaces
{
    public interface ISerializer<TSerialized>
    {
        TSerialized Serialize(object content);
        T Deserialize<T>(TSerialized content);
        object Deserialize(TSerialized content);
    }
}
