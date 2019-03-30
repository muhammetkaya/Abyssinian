using Abyssinian.Messaging.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Messaging.Kafka.Settings
{
    public class KafkaConsumerSettings : ConsumeSettings
    {
        public List<string> Topics { get; set; } = new List<string>();
        public string BrokerList { get; set; }
        public string GroupId { get; set; }
    }
}
