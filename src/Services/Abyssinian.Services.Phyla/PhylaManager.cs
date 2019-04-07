using Abyssinian.Messaging;
using Abyssinian.Messaging.Interaces;
using Abyssinian.Services.Phyla.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abyssinian.Services.Phyla
{
    public class PhylaManager : IPhylaManager
    {
        private readonly Guid _currentId;
        private readonly ConcurrentBag<Message> _messages;
        private readonly IMessageClient _messageClient;

        public PhylaManager(IMessageClient messageClient)
        {
            _currentId = Guid.NewGuid();
            _messages = new ConcurrentBag<Message>();
            _messageClient = messageClient;
            _messageClient.ReceiveMessage = (message, cancellationToken) =>
            {
                Console.WriteLine("Message received.");
                Console.WriteLine("Message Details: " + Environment.NewLine + message.ToString());
                _messages.Add(message);
                return true;
            };
        }

        public List<Message> GetMessages()
        {
            return _messages.ToList();
        }

        public void SendMessage(string message, string to)
        {
            _messageClient.SendMessage(new Message(_currentId)
            {
                Content = message,
                To = to
            });
            Console.WriteLine($"Message sent to {to}.");
        }

    }
}
