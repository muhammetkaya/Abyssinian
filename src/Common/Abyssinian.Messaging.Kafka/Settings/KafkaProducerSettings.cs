using System;
using Abyssinian.Messaging.Settings;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Messaging.Kafka.Settings
{
    public class KafkaProducerSettings : ProducerSettings
    {
        public string BrokerList { get; set; }
    }
}
