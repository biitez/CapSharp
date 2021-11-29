using System;
using System.Collections.Generic;
using System.Text;

namespace CapSharp.Exceptions
{
    internal class TwoCaptchaException : Exception
    {
        public TwoCaptchaException(string ErrorCode) : base(ErrorCode) { }
    }
}
