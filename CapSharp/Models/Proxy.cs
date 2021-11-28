using System;
using System.Collections.Generic;
using System.Text;

namespace CapSharp.Models
{
    internal class Proxy
    {
        public string IpAddress { get; set; }

        public int Port { get { return _Port; } 
            set 
            { 
                if (value > 65535) throw new ArgumentOutOfRangeException(nameof(Port));

                _Port = value;
            }
        }

        public int _Port;
    }
}
