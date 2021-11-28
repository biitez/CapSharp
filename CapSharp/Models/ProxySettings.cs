using CapSharp.Enums;
using System;

namespace CapSharp.Models
{
    public class ProxySettings
    {
        public ProxyProtocol ProxyProtocol { get; private set; } = ProxyProtocol.HTTP;

        public TimeSpan Timeout = TimeSpan.FromSeconds(1);

        public bool BackConnect = false;

        public Credentials ProxyCredentials { get; set; }

        public ProxySettings(ProxyProtocol proxyProtocol)
        {
            ProxyProtocol = proxyProtocol;
        }
    }
}
