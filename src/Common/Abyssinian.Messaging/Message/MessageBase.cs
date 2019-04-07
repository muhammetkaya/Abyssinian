using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Messaging
{
    public class MessageBase
    {
        private readonly Guid _producerId;
        public Guid ProducerId
        {
            get
            {
                return _producerId;
            }
        }

        public string To { get; set; }

        public MessageBase(Guid producerId)
        {
            _producerId = producerId;
        }
    }
}
