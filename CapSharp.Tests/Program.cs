using CapSharp.Enums;
using CapSharp.Models;
using CapSharp.Services;
using CapSharp.Services.Enums;
using System;

namespace CapSharp.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Esperado:
            //
            // ICapSharp capSharp = new TwoCaptcha(ApiKey: "ApiKey", UseProxy: true)
            // {
            //    ThrowExceptions = false,
            //    UseProxy = true
            // };
            //
            // bool IsSolver = capSharp.TrySolve(out string AccessToken);

            CapSharp capSharp = new CapSharp(UseProxy: true)
            {
                ThrowExceptions = true,
                Proxy = new Proxy("1.1.1.1", 1234, new ProxySettings(ProxyProtocol.HTTP)
                {
                    BackConnect = true,
                    Timeout = TimeSpan.FromSeconds(5),
                    ProxyCredentials = new Credentials("Username", "Password")
                })
            };

            TwoCaptcha twoCaptcha = new TwoCaptcha(apiKey: "AccountApiKey", capSharp);

            twoCaptcha.SetCaptchaSettings(
                TwoCaptchaTypes.reCaptchaV2, SiteKey: "SITE_KEY", "SITE_URL", CaptchaInvisible: false);


            bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out var accessToken);
            bool BalanceIsSuccess = twoCaptcha.TryGetUserBalance(out var Balance);            

            twoCaptcha.TrySolveCaptcha(out var lol);
        }
    }
}