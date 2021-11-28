using CapSharp.Models;
using CapSharp.Services.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CapSharp.Services
{
    public class TwoCaptcha : ICapSharp
    {
        public string ApiKey, SiteKey, SiteUrl;
        public string ApiUrl = "http://2captcha.com/";

        public TwoCaptcha(string apiKey, CapSharp capSharp)
        {
            ApiKey = apiKey;
            CapSharp = capSharp;

            HttpClient = new HttpClient();
        }

        public TwoCaptcha SetCaptchaSettings(TwoCaptchaTypes captchaType, 
            string siteKey, string siteUrl, bool captchaInvisible = false)
        {
            this.SiteKey = siteKey;
            this.SiteUrl = siteUrl;

            CaptchaType = captchaType;
            InvisibleCaptcha = captchaInvisible;

            return this;
        }

        public bool TrySolveCaptcha(out string AccessToken)
        {
            AccessToken = null;

            if (!CaptchaType.HasValue)
            {
                return CapSharp.ThrowExceptions
                    ? throw new ArgumentException(nameof(TwoCaptchaTypes))
                    : false;
            }

            var captchaFuncs = new Dictionary<TwoCaptchaTypes, Func<(bool, string)>>
            {
                { TwoCaptchaTypes.reCaptchaV2, ReCaptchaV2 }
            };

            var (isSuccess, accessToken) = captchaFuncs[CaptchaType.Value].Invoke();

            AccessToken = accessToken;

            return isSuccess;
        }

        protected (bool, string) ReCaptchaV2()
        {
            return (true, null);
        }

        public bool TryGetUserBalance(out double balance)
        {

            throw new NotImplementedException();
        }

        private CapSharp CapSharp { get; set; }
        private HttpClient HttpClient { get; set; }
        private TwoCaptchaTypes? CaptchaType { get; set; } = null;
        private bool InvisibleCaptcha { get; set; } = false;
    }
}
