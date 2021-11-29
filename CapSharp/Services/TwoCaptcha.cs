using CapSharp.Exceptions;
using CapSharp.Extensions;
using CapSharp.Models;
using CapSharp.Services.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CapSharp.Services
{
    public class TwoCaptcha : ICapSharp
    {
        public string SiteKey, SiteUrl;

        public string UserAgent = null;
        public string PingBack = null;

        public bool Enterprise = false;

        public string ApiUrl = "http://2captcha.com/in.php";

        public Dictionary<string, string> Params = new Dictionary<string, string>
        {
            { "json", 1.ToString() }
        };

        public TwoCaptcha(string apiKey, CapSharp capSharp)
        {
            Params.Add("key", apiKey);

            CapSharp = capSharp;

            httpClientHandler = new Handlers.HttpClientHandler(new Uri(ApiUrl));
        }

        public void SetCaptchaSettings(TwoCaptchaTypes captchaType, 
            string siteKey, string siteUrl, bool invisibleCaptcha = false)
        {
            Params.Add("method", captchaType.ToStringType());
            Params.Add("invisible", invisibleCaptcha ? "1" : "0");

            // Se utiliza para diferenciar los metodos de TrySolveCaptcha()
            CaptchaType = captchaType;

            SiteKey = siteKey;
            SiteUrl = siteUrl;            
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

            // Desde este metodo, comprobar el tipo de captcha que es
            // (recaptcha, funcaptcha, hcaptcha, etc)
            // y enviar la solicitud al metodo SolveCaptchaRequest con
            // la configuracion necesaria.

            var (isSuccess, requestId) = SolveCaptchaRequest();

            // Se debe hacer una solicitud enviando el RequestId
            // cada 5 segundos hasta tener el access Token

            string accessToken = string.Empty;

            AccessToken = accessToken;

            return isSuccess;
        }

        protected (bool, string) SolveCaptchaRequest()
        {
            Params.Add("googlekey", SiteKey);
            Params.Add("pageurl", SiteUrl);

            if (CapSharp.UseProxy)
            {
                Params.Add("proxytype", CapSharp.Proxy._proxySettings._proxyProtocol.ToString());

                if (CapSharp.Proxy._host == null &&
                    CapSharp.Proxy._port == null)
                {
                    throw new ArgumentNullException(nameof(CapSharp.Proxy._host));
                }

                string username = CapSharp.Proxy._proxySettings.ProxyCredentials.Username ?? string.Empty;
                string password = CapSharp.Proxy._proxySettings.ProxyCredentials.Password ?? string.Empty;

                Params.Add("proxy",
                    $"{(username != string.Empty && password != string.Empty ? $"{username}:{password}@" : string.Empty)}{CapSharp.Proxy._host}:{CapSharp.Proxy._port}");
            }

            if (!string.IsNullOrWhiteSpace(PingBack))
            {
                Params.Add("pingback", PingBack);
            }

            if (!string.IsNullOrWhiteSpace(UserAgent))
            {
                Params.Add("userAgent", UserAgent);
            }

            JObject CaptchaSolverJsonResponse = 
                httpClientHandler.GetResponseJsonAsync<JObject>(HttpMethod.Get, GetQueries: Params).Result;

            string Status = (string?)CaptchaSolverJsonResponse.SelectToken("status");
            string RequestText = (string?)CaptchaSolverJsonResponse.SelectToken("request");

            return (Status != 1.ToString()
                ? CapSharp.ThrowExceptions
                    ? throw new TwoCaptchaException(RequestText ?? nameof(TwoCaptcha))
                    : false
                : true, RequestText);
        }

        public bool TryGetUserBalance(out double balance)
        {

            throw new NotImplementedException();
        }

        private CapSharp CapSharp { get; set; }
        private Handlers.HttpClientHandler httpClientHandler { get; set; }
        private TwoCaptchaTypes? CaptchaType { get; set; } = null;
    }
}
