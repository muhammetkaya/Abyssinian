using Abyssinian.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abyssinian.Services.Phyla.Contracts
{
    public interface IPhylaManager
    {
        List<Message> GetMessages();
        void SendMessage(string message, string to);
    }
}
