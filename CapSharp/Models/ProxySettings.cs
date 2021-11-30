using CapSharp.Enums;
using System;

namespace CapSharp.Models
{
    public class ProxySettings
    {
        public ProxyProtocol _proxyProtocol { get; } = ProxyProtocol.HTTP;

        public TimeSpan Timeout = TimeSpan.FromSeconds(1);

        public bool BackConnect = false;

        /// <summary>
        /// Your proxy has credentials?, configure them here
        /// </summary>
        public Credentials ProxyCredentials = null;

        /// <summary>
        /// Proxy configuration
        /// </summary>
        /// <param name="proxyProtocol"><see cref="ProxyProtocol"/></param>
        public ProxySettings(ProxyProtocol proxyProtocol)
        {
            _proxyProtocol = proxyProtocol;
        }
    }
}
