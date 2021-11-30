using CapSharp.Exceptions;
using CapSharp.Extensions;
using CapSharp.Handlers;
using CapSharp.Services.TwoCaptchaService.Enums;
using CapSharp.Services.TwoCaptchaService.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CapSharp.Services.TwoCaptchaService
{
    public class TwoCaptcha : ICapSharp
    {
        private Uri ApiUrl = new Uri("http://2captcha.com/in.php");
        private Uri ApiUrlGetResponse = new Uri("http://2captcha.com/res.php");

        public string UserAgent, PingBack, PublicKey, hCaptchaSiteKey;

        /// <summary>
        /// <see cref="KeyCaptcha"/> Settings
        /// </summary>
        public KeyCaptcha KeyCaptcha = new KeyCaptcha();

        /// <summary>
        /// <see cref="GeeTest"/> Settings
        /// </summary>
        public GeeTest GeeTest = new GeeTest();

        /// <summary>
        /// In order not to assign new parameters all the time, everything is configured from the same dictionary
        /// </summary>
        private Dictionary<string, string> Params = new Dictionary<string, string>
        {
            { "json", 1.ToString() }
        };

        /// <summary>
        /// The captcha is enterprise?, change this option to <see cref="true"/>, default is <see cref="false"/>
        /// </summary>
        public bool Enterprise = false;

        /// <summary>
        /// Service Url is optional parameter and if you don't 
        /// provide it we use a default value that is valid for most cases, 
        /// but we recommend you to provide it.
        /// 
        /// (ArkoseLabs)
        /// Can be found on the div with the name = fc-token
        /// </summary>
        public string ServiceUrl = string.Empty;

        /// <summary>
        /// The score needed for resolution. 
        /// Currently it's almost impossible to get token with score higher than 0.3
        /// 
        /// Default: 0.4, Not Required
        /// </summary>
        public double MinScore = 0.4;        

        /// <summary>
        /// Initialize an instance of <see cref="TwoCaptcha"/>
        /// </summary>
        /// <param name="apiKey">Your 2Captcha.com API Key</param>
        /// <param name="capSharp">CapSharp Library Builder</param>
        public TwoCaptcha(string apiKey, CapSharp capSharp)
        {
            Params.Add("key", apiKey);

            CapSharp = capSharp;

            HttpClientHandler = new Handlers.HttpClientHandler(ApiUrl, 
                new HttpClient(new System.Net.Http.HttpClientHandler
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
                })
                {
                    Timeout = capSharp.Proxy?._proxySettings?.Timeout ?? TimeSpan.FromSeconds(5),
                });
        }

        /// <summary>
        /// Configure the captcha solver
        /// </summary>
        /// <param name="captchaType"><see cref="TwoCaptchaTypes"/></param>
        /// <param name="PageUrl">The page Url</param>
        /// <param name="ReCaptchaSiteKey">The key to the google recaptcha site</param>
        /// <param name="invisibleCaptcha">Your captcha is Invisible?, set this option to true</param>
        /// <returns><see cref="TwoCaptcha"/></returns>
        public TwoCaptcha SetCaptchaSettings(TwoCaptchaTypes captchaType, 
            string PageUrl, string ReCaptchaSiteKey = null, bool invisibleCaptcha = false)
        {
            Params.Add("method", captchaType.ToStringType());

            if (!string.IsNullOrWhiteSpace(PageUrl)) Params.Add("pageurl", PageUrl);

            if (invisibleCaptcha)
            {
                Params.Add("invisible", "1");
                Params.Add("min_score", MinScore.ToString() ?? "0.4");
            }

            CaptchaType = captchaType;

            Params.Add("googlekey", ReCaptchaSiteKey);            

            return this;
        }

        /// <summary>
        /// Try to solve the captcha
        /// </summary>
        /// <param name="AccessToken">The valid captcha code</param>
        /// <returns><see cref="bool"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public bool TrySolveCaptcha(out string AccessToken)
        {
            AccessToken = null;

            Params.Add("enterprise", !Enterprise ? "0" : "1");

            if (CaptchaType.Value.Equals(TwoCaptchaTypes.reCaptchaV3))
            {
                Params.Add("version", "v3");
            }

            if (CaptchaType.Equals(TwoCaptchaTypes.hCaptcha))
            {
                Params.Add("sitekey", hCaptchaSiteKey ?? throw new ArgumentNullException(nameof(hCaptchaSiteKey)));
            }

            if (CaptchaType.Equals(TwoCaptchaTypes.FunCaptcha))
            {
                Params.Add("publickey", PublicKey ?? throw new ArgumentNullException(nameof(PublicKey)));

                if (string.IsNullOrWhiteSpace(ServiceUrl)) Params.Add("surl", ServiceUrl);
            }

            if (CaptchaType.Value.Equals(TwoCaptchaTypes.KeyCaptcha))
            {
                if (!KeyCaptcha.ParametersFilled())
                {
                    return CapSharp.ThrowExceptions
                        ? throw new ArgumentException("Unfilled KeyCaptcha parameters")
                        : false;
                }

                Params.Add("s_s_c_user_id", KeyCaptcha.s_s_c_user_id);
                Params.Add("s_s_c_session_id", KeyCaptcha.s_s_c_session_id);
                Params.Add("s_s_c_web_server_sign", KeyCaptcha.s_s_c_web_server_sign);
                Params.Add("s_s_c_web_server_sign2", KeyCaptcha.s_s_c_web_server_sign2);
            }

            if (CaptchaType.Value.Equals(TwoCaptchaTypes.GeeTest))
            {
                if (!GeeTest.ParametersFilled())
                {
                    return CapSharp.ThrowExceptions
                        ? throw new ArgumentException("Unfilled GeeTest parameters")
                        : false;
                }

                Params.Add("gt", GeeTest.gt);
                Params.Add("challenge", GeeTest.challenge);

                if (!string.IsNullOrWhiteSpace(GeeTest.api_server)) 
                    Params.Add("api_server", GeeTest.api_server);
            }

            var (isSuccess, requestId) = SolveCaptchaRequest();

            if (!isSuccess)
            {
                AccessToken = requestId;

                return false;
            }

            var (SuccessResponse, CaptchaAccessToken) = CaptchaRequestSolvePings(requestId).Result;

            if (!SuccessResponse)
            {
                AccessToken = CaptchaAccessToken;

                return false;
            }

            AccessToken = CaptchaAccessToken;

            return true;
        }


        /// <summary>
        /// Try to solve the captcha (I thought this captcha was dead, lol)
        /// </summary>
        /// <param name="geeTestResponse">A model response to GeeTest</param>
        /// <returns><see cref="bool"/></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool TrySolveCaptcha(out GeeTest.Response geeTestResponse)
        {
            geeTestResponse = null;

            if (!GeeTest.ParametersFilled())
            {
                return CapSharp.ThrowExceptions
                    ? throw new ArgumentException("Unfilled GeeTest parameters")
                    : false;
            }

            Params.Add("gt", GeeTest.gt);
            Params.Add("challenge", GeeTest.challenge);

            if (!string.IsNullOrWhiteSpace(GeeTest.api_server))
                Params.Add("api_server", GeeTest.api_server);

            var (isSuccess, requestId) = SolveCaptchaRequest();

            if (!isSuccess) return false;

            var (SuccessResponse, CaptchaAccessToken) = CaptchaRequestSolvePings(requestId).Result;

            if (!SuccessResponse || CaptchaAccessToken != null) return false;

            geeTestResponse = GeeTestApiResponse;

            return true;
        }

        protected (bool, string) SolveCaptchaRequest()
        {
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
                HttpClientHandler.GetResponseJsonAsync<JObject>(HttpMethod.Get, GetQueries: Params).Result;

            string Status = (string?)CaptchaSolverJsonResponse.SelectToken("status");
            string RequestText = (string?)CaptchaSolverJsonResponse.SelectToken("request");

            return (Status != 1.ToString()
                ? CapSharp.ThrowExceptions
                    ? throw new TwoCaptchaException(RequestText ?? nameof(TwoCaptcha))
                    : false
                : true, RequestText);
        }

        protected async Task<(bool, string)> CaptchaRequestSolvePings(string RequestId)
        {
            (bool IsSuccess, string CaptchaAccessToken) Response = (false, null);

            HttpClientHandler.SetNewUri(ApiUrlGetResponse);

            await Task.Delay(TimeSpan.FromSeconds(10)).ConfigureAwait(false);

            await Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                    JObject CaptchaResponseJson = await
                        HttpClientHandler.GetResponseJsonAsync<JObject>(HttpMethod.Get, new Dictionary<string, string>
                        {
                        { "action", "get" },
                        { "key", Params["key"] },
                        { "id", RequestId },
                        { "json", 1.ToString() }
                        })
                        .ConfigureAwait(false);

                    string ResponseInfo = (string)CaptchaResponseJson.SelectToken("request");

                    if (ResponseInfo.Equals("CAPCHA_NOT_READY"))
                    {
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace((string)CaptchaResponseJson.SelectToken("challenge")))
                    {
                        Response.IsSuccess = true;

                        GeeTestApiResponse = new GeeTest.Response
                        {
                            challenge = (string)CaptchaResponseJson.SelectToken("challenge"),
                            seccode = (string)CaptchaResponseJson.SelectToken("seccode"),
                            validate = (string)CaptchaResponseJson.SelectToken("validate"),
                        };

                        break;
                    }

                    int Status = (int)CaptchaResponseJson.SelectToken("status");

                    if (Status.Equals(1)) 
                    {
                        Response.CaptchaAccessToken = ResponseInfo;
                        Response.IsSuccess = true;

                        break;
                    }

                    if (CapSharp.ThrowExceptions) throw new TwoCaptchaException(ResponseInfo);

                    Response.CaptchaAccessToken = ResponseInfo;

                    break;
                }
            }).ConfigureAwait(false);

            return (Response.IsSuccess, Response.CaptchaAccessToken);
        }

        public bool TryGetUserBalance(out string balance)
        {
            balance = null;

            HttpClientHandler.SetNewUri(ApiUrlGetResponse);

            JObject UserBalanceJson =
                HttpClientHandler.GetResponseJsonAsync<JObject>(HttpMethod.Get, new Dictionary<string, string>
                {
                    { "action", "getbalance" },
                    { "key", Params["key"] },
                    { "json", 1.ToString() }
                }).Result;

            if (!((int)UserBalanceJson.SelectToken("status")).Equals(1))
            {
                if (CapSharp.ThrowExceptions) 
                    throw new TwoCaptchaException(
                        (string)UserBalanceJson.SelectToken("request") ?? "Invalid Request");

                return false;
            }

            balance = (string)UserBalanceJson.SelectToken("request");
            return true;
        }

        private CapSharp CapSharp { get; set; }
        private Handlers.HttpClientHandler HttpClientHandler { get; set; }
        private GeeTest.Response GeeTestApiResponse { get; set; } = null;

        private TwoCaptchaTypes? CaptchaType
        {
            // Not having this assigned is a big problem,
            // the exception occurs even if it has been configured not to be throwed.

            get { return _CaptchaType ?? throw new ArgumentException(nameof(TwoCaptchaTypes)); }
            set { _CaptchaType = value; }
        }

        private TwoCaptchaTypes? _CaptchaType;
    }
}
