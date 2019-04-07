using Abyssinian.Messaging.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abyssinian.Messaging.Interaces
{
    public interface IMessageClient
    {
        Func<Message, CancellationToken, bool> ReceiveMessage { get; set; }
        Task SendMessage(Message message);
        void InitializeMessageClient(MessageClientSettings producerSettings);
    }
}
