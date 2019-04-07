using Abyssinian.Messaging.Interaces;
using Abyssinian.Messaging.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abyssinian.Messaging.RabbitMQ
{
    public class RabbitMQMessageClient : IMessageClient
    {
        public Func<Message, CancellationToken, bool> ReceiveMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void InitializeMessageClient(MessageClientSettings producerSettings)
        {
            throw new NotImplementedException();
        }

        public Task SendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
