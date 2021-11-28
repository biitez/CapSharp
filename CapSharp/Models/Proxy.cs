using System;

namespace CapSharp.Models
{
    public class Proxy
    {
        public Proxy(string host, int port, ProxySettings proxySettings) : this(proxySettings)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));

            _port = port > 65535 
                ? throw new ArgumentOutOfRangeException(nameof(port), port, "Port not supported")
                : port;
        }

        private Proxy(ProxySettings proxySettings)
        {
            _proxySettings = proxySettings;
        }

        private readonly ProxySettings _proxySettings;
        private readonly string _host;
        private readonly int _port;
    }
}
