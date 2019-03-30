using Abyssinian.Messaging.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abyssinian.Messaging.Interaces
{
    public interface IMessageClient
    {
        void InitializeProducer(ProducerSettings producerSettings);
        Task Produce(Message message);
        void Consume(ConsumeSettings consumeSettings);
    }
}
