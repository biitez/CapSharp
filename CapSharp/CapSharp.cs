using CapSharp.Models;
using System;
using System.Net.Http;

namespace CapSharp
{
    public class CapSharp
    {
        public Proxy Proxy { get; set; }

        public bool ThrowExceptions = false;

        private HttpClient httpClient { get; }
        public bool UseProxy { get; private set; }

        public CapSharp( bool UseProxy = false)
        {
            this.UseProxy = UseProxy;

            httpClient = new HttpClient();
        }
    }
}
