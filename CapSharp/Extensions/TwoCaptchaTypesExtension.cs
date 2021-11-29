using CapSharp.Services.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapSharp.Extensions
{
    internal static class TwoCaptchaTypesExtension
    {
        internal static string ToStringType(this TwoCaptchaTypes CaptchaType)
        {
            var types = new Dictionary<TwoCaptchaTypes, string>
            {
                { TwoCaptchaTypes.reCaptchaV2 | TwoCaptchaTypes.reCaptchaV2, "userrecaptcha" },
            };

            return types[CaptchaType];
        }
    }
}
