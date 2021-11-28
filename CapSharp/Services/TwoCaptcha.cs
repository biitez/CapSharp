using CapSharp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CapSharp.Services
{
    public class TwoCaptcha : ICapSharp
    {
        public CapSharp capSharp { get; set; }
        public string ApiKey { get; }

        public TwoCaptcha(string ApiKey, CapSharp capSharp)
        {
            this.ApiKey = ApiKey;
            this.capSharp = capSharp;
        }

        public bool TrySolveCaptcha(out string AccessToken)
        {
            AccessToken = null;

            return false;
        }

        public bool TryGetUserBalance(out double Balance)
        {
            throw new NotImplementedException();
        }
    }
}
