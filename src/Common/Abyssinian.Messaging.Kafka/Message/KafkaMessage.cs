using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Messaging.Kafka
{
    public class KafkaMessage : Message
    {
        public string Topic { get; set; }

        public KafkaMessage(Guid producerId) : base(producerId)
        {

        }
    }
}
