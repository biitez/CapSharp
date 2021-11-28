using CapSharp.Models;
using System;
using System.Net.Http;

namespace CapSharp
{
    public class CapSharp
    {
        public Proxy Proxy { get; set; }
        private HttpClient HttpClient { get; }

        public bool ThrowExceptions = false;
        public bool UseProxy = false;

        public CapSharp (bool useProxy = false)
        {
            UseProxy = useProxy;
            HttpClient = new HttpClient();
        }
    }
}
