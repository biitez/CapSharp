using CapSharp.Enums;
using System;

namespace CapSharp.Models
{
    public class ProxySettings
    {
        private ProxyProtocol _proxyProtocol = ProxyProtocol.HTTP;

        public TimeSpan Timeout = TimeSpan.FromSeconds(1);

        public bool BackConnect = false;

        public Credentials ProxyCredentials = null;

        public ProxySettings(ProxyProtocol proxyProtocol)
        {
            _proxyProtocol = proxyProtocol;
        }
    }
}
