using System;
using Abyssinian.Messaging.Settings;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Messaging.Kafka.Settings
{
    public class KafkaMessageSettings : MessageClientSettings
    {
        public string BrokerList { get; set; }
        public string GroupId { get; internal set; }
        public List<string> Topics { get; set; }
        public string ServiceName { get; set; }
    }
}
