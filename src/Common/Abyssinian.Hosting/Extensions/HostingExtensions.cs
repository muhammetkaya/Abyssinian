using Abyssinian.Hosting.Settings;
using Abyssinian.Messaging.Interaces;
using Abyssinian.Messaging.Kafka;
using Abyssinian.Messaging.Kafka.Settings;
using Abyssinian.Messaging.RabbitMQ;
using Abyssinian.Messaging.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Abyssinian.Hosting.Extensions
{
    public static class HostingExtensions
    {
        public const string InterServiceSettingsContant = "InterServiceCommunication";

        public static void UseInterServiceCommunication(this IServiceCollection services, InterServiceCommunicationSettings settings)
        {
            IMessageClient messageClient = null;
            MessageClientSettings producerSettings = null;

            switch (settings.Type)
            {
                case Enumerations.InterServiceCommunicationType.RabbitMQ:
                    messageClient = new RabbitMQMessageClient();
                    break;
                case Enumerations.InterServiceCommunicationType.Kafka:
                default:
                    messageClient = new KafkaMessageClient();
                    producerSettings = new KafkaMessageSettings()
                    {
                        BrokerList = settings.Address,
                        Topics = settings.Receivers,
                        ServiceName = settings.ServiceName
                    };
                    break;
            }

            messageClient.InitializeMessageClient(producerSettings);

            services.AddSingleton(messageClient);
        }
    }
}
