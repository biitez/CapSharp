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

            httpClient = new HttpClient();
        }

        public TwoCaptcha SetCaptchaSettings(TwoCaptchaTypes CaptchaType, 
            string SiteKey, string SiteUrl, bool CaptchaInvisible = false)
        {
            this.SiteKey = SiteKey;
            this.SiteUrl = SiteUrl;

            _CaptchaType = CaptchaType;
            InvisibleCaptcha = CaptchaInvisible;

            return this;
        }

        public bool TrySolveCaptcha(out string AccessToken)
        {
            AccessToken = null;

            if (!_CaptchaType.HasValue)
            {
                return CapSharp.ThrowExceptions
                    ? throw new ArgumentException(nameof(TwoCaptchaTypes))
                    : false;
            }

            var CaptchaFuncs = new Dictionary<TwoCaptchaTypes, Func<(bool, string)>>
            {
                { TwoCaptchaTypes.reCaptchaV2, ReCaptchaV2 }
            };

            (bool isSuccess, string accessToken) = CaptchaFuncs[_CaptchaType.Value].Invoke();

            AccessToken = accessToken;

            return isSuccess;
        }

        private (bool, string) ReCaptchaV2()
        {
            return (true, null);
        }

        public bool TryGetUserBalance(out double Balance)
        {

            throw new NotImplementedException();
        }

        public CapSharp CapSharp { get; set; }
        private HttpClient httpClient { get; set; }
        private TwoCaptchaTypes? _CaptchaType { get; set; } = null;
        private bool InvisibleCaptcha { get; set; } = false;
    }
}
