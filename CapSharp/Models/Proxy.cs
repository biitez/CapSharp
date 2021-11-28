using System;

namespace CapSharp.Models
{
    public class Proxy
    {
        public Proxy(string Host, int Port, ProxySettings proxySettings) : this(proxySettings)
        {
            _host = Host ?? throw new ArgumentNullException(nameof(Host));

            _port = Port > 65535 
                ? throw new ArgumentOutOfRangeException(nameof(Port), Port, "Port not supported")
                : Port;
        }

        private Proxy(ProxySettings proxySettings)
        {
            _ProxySettings = proxySettings;
        }

        private ProxySettings _ProxySettings { get; }

        private string _host { get; }
        private int _port { get; }
    }
}
