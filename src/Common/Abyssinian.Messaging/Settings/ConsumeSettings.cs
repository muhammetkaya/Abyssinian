using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Abyssinian.Messaging.Settings
{
    public class ConsumeSettings
    {
        public Func<Message, CancellationToken, bool> Consume { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
