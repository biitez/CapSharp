using CapSharp.Services.TwoCaptchaService.Enums;
using System.Collections.Generic;

namespace CapSharp.Extensions
{
    // I just got a little creative trying to simplify the code
    internal static class TwoCaptchaTypesExtension
    {
        internal static string ToStringType(this TwoCaptchaTypes CaptchaType)
        {
            string reCaptcha = "userrecaptcha";

            var types = new Dictionary<TwoCaptchaTypes, string>
            {
                { TwoCaptchaTypes.reCaptchaV2, reCaptcha },
                { TwoCaptchaTypes.reCaptchaV3, reCaptcha },
                { TwoCaptchaTypes.FunCaptcha, "funcaptcha" },
                { TwoCaptchaTypes.KeyCaptcha, "keycaptcha" },
                { TwoCaptchaTypes.GeeTest, "geetest" },
                { TwoCaptchaTypes.hCaptcha, "hcaptcha" }
            };

            return types[CaptchaType];
        }
    }
}
