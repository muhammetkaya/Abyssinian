using Abyssinian.Hosting.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyssinian.Hosting.Settings
{
    public class InterServiceCommunicationSettings
    {
        public InterServiceCommunicationType Type { get; set; }
        public string Address { get; set; }
        public List<string> Receivers { get; set; }
        public string ServiceName { get; set; }
    }
}
