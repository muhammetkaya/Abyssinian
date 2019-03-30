using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abyssinian.Messaging.Extensions;
using Abyssinian.Messaging.Interaces;
using Abyssinian.Messaging.Kafka.Settings;
using Abyssinian.Messaging.Settings;
using Confluent.Kafka;

namespace Abyssinian.Messaging.Kafka
{
    public class KafkaMessageClient : IMessageClient
    {
        #region ..Fields..

        ProducerConfig fProducerConfig;

        #endregion

        #region ..Privates..

        private void Internal_Consume(KafkaConsumerSettings consumeSettings)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = consumeSettings.BrokerList,
                GroupId = consumeSettings.GroupId, //"csharp-consumer",
                EnableAutoCommit = false,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true
            };

            const int commitPeriod = 5;

            // Note: If a key or value deserializer is not set (as is the case below), the 
            // deserializer corresponding to the appropriate type from Confluent.Kafka.Deserializers
            // will be used automatically (where available). The default deserializer for string
            // is UTF8. The default deserializer for Ignore returns null for all input data
            // (including non-null data).
            using (var consumer = new ConsumerBuilder<Ignore, string>(config)
                // Note: All handlers are called on the main .Consume thread.
                .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                .SetStatisticsHandler((_, json) => Console.WriteLine($"Statistics: {json}"))
                .SetPartitionsAssignedHandler((c, partitions) =>
                {
                    Console.WriteLine($"Assigned partitions: [{string.Join(", ", partitions)}]");
                    // possibly manually specify start offsets or override the partition assignment provided by
                    // the consumer group by returning a list of topic/partition/offsets to assign to, e.g.:
                    // 
                    // return partitions.Select(tp => new TopicPartitionOffset(tp, externalOffsets[tp]));
                })
                .SetPartitionsRevokedHandler((c, partitions) =>
                {
                    Console.WriteLine($"Revoking assignment: [{string.Join(", ", partitions)}]");
                })
                .Build())
            {
                consumer.Subscribe(consumeSettings.Topics);

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(consumeSettings.CancellationToken);

                            if (consumeResult.IsPartitionEOF)
                            {
                                Console.WriteLine(
                                    $"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");

                                continue;
                            }
                            Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Value}");
                            var consumed = consumeSettings.Consume(consumeResult.Value.ConvertToMessage(), consumeSettings.CancellationToken);
                            if (consumed && consumeResult.Offset % commitPeriod == 0)
                            {
                                // The Commit method sends a "commit offsets" request to the Kafka
                                // cluster and synchronously waits for the response. This is very
                                // slow compared to the rate at which the consumer is capable of
                                // consuming messages. A high performance application will typically
                                // commit offsets relatively infrequently and be designed handle
                                // duplicate messages in the event of failure.
                                try
                                {
                                    consumer.Commit(consumeResult);
                                }
                                catch (KafkaException e)
                                {
                                    Console.WriteLine($"Commit error: {e.Error.Reason}");
                                }
                            }
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Consume error: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Closing consumer.");
                    consumer.Close();
                }
            }
        }

        private async Task Internal_Produce(KafkaMessage message)
        {
            if (fProducerConfig == null) InitializeProducer(null);
            using (var producer = new ProducerBuilder<string, string>(fProducerConfig).Build())
            {
                Console.WriteLine("\n-----------------------------------------------------------------------");
                Console.WriteLine($"Producer {producer.Name} producing on topic {message.Topic}.");
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("To create a kafka message with UTF-8 encoded key and value:");
                Console.WriteLine("> key value<Enter>");
                Console.WriteLine("To create a kafka message with a null key and UTF-8 encoded value:");
                Console.WriteLine("> value<enter>");
                Console.WriteLine("Ctrl-C to quit.\n");

                var cancelled = false;
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cancelled = true;
                };

                while (!cancelled)
                {
                    Console.Write("> ");

                    string text;
                    try
                    {
                        text = Console.ReadLine();
                    }
                    catch (IOException)
                    {
                        // IO exception is thrown when ConsoleCancelEventArgs.Cancel == true.
                        break;
                    }
                    if (text == null)
                    {
                        // Console returned null before 
                        // the CancelKeyPress was treated
                        break;
                    }

                    string key = null;
                    string val = text;

                    // split line if both key and value specified.
                    int index = text.IndexOf(" ");
                    if (index != -1)
                    {
                        key = text.Substring(0, index);
                        val = text.Substring(index + 1);
                    }

                    try
                    {
                        // Note: Awaiting the asynchronous produce request below prevents flow of execution
                        // from proceeding until the acknowledgement from the broker is received (at the 
                        // expense of low throughput).
                        var deliveryReport = await producer.ProduceAsync(
                            message.Topic, new Message<string, string> { Key = key, Value = val });

                        Console.WriteLine($"delivered to: {deliveryReport.TopicPartitionOffset}");
                    }
                    catch (ProduceException<string, string> e)
                    {
                        Console.WriteLine($"failed to deliver message: {e.Message} [{e.Error.Code}]");
                    }
                }

                // Since we are producing synchronously, at this point there will be no messages
                // in-flight and no delivery reports waiting to be acknowledged, so there is no
                // need to call producer.Flush before disposing the producer.
            }
        }

        private void Internal_InitializeProducer(KafkaProducerSettings producerSettings)
        {
            var config = new ProducerConfig { BootstrapServers = producerSettings.BrokerList };
        }

        private KafkaProducerSettings GetDefaultSettings()
        {
            return null;
        }

        #endregion

        #region ..IMessageClient Implementations..

        public void Consume(ConsumeSettings consumeSettings)
        {
            if (!(consumeSettings is KafkaConsumerSettings))
                throw new Exception("Settings should be KafkaSettings");

            Internal_Consume(consumeSettings as KafkaConsumerSettings);
        }

        public async Task Produce(Message message)
        {
            if (!(message is KafkaMessage))
                throw new Exception("Settings should be KafkaSettings");

            await Internal_Produce(message as KafkaMessage);
        }

        public void InitializeProducer(ProducerSettings producerSettings)
        {
            if (producerSettings == null)
                producerSettings = GetDefaultSettings();
            else if (!(producerSettings is KafkaProducerSettings))
                throw new Exception("Settings should be KafkaSettings");

            Internal_InitializeProducer(producerSettings as KafkaProducerSettings);

        }

        #endregion
    }
}
