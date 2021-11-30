using System;

namespace CapSharp.Exceptions
{
    internal class TwoCaptchaException : Exception
    {
        public TwoCaptchaException(string ErrorCode) : base(ErrorCode) { }
    }
}
