using System;

namespace CapSharp.Models
{
    public class Proxy
    {
        /// <summary>
        /// Configure your proxy
        /// </summary>
        /// <param name="host">Proxy Host / Ip Address</param>
        /// <param name="port">Proxy Port</param>
        /// <param name="proxySettings"><see cref="ProxySettings"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        public ProxySettings _proxySettings { get; } = null;
        public string _host { get; } = null;
        public int? _port { get; } = null;
    }
}
