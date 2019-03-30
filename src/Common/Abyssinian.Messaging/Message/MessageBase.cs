using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Messaging
{
    public class MessageBase
    {
        public Guid ProducerId { get; set; }
        public Guid ConsumerId { get; set; }

        public MessageBase(Guid producerId)
        {
            ProducerId = producerId;
        }
    }
}
