using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Messaging
{
    public class Message : MessageBase
    {
        public string Content { get; set; }

        public Message(Guid producerId): base(producerId)
        {
            
        }
    }
}
