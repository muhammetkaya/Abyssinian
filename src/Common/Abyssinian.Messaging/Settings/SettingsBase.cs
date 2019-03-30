using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Abyssinian.Messaging.Settings
{
    public class SettingsBase
    {
        public CancellationToken CancellationToken { get; set; }
    }
}
