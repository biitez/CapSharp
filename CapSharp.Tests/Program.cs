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

            CapSharp capSharp = new CapSharp(useProxy: true)
            {
                ThrowExceptions = false,
                Proxy = new Proxy("1.1.1.1", 1234, new ProxySettings(ProxyProtocol.HTTP)
                {
                    BackConnect = true,
                    Timeout = TimeSpan.FromSeconds(5),
                    ProxyCredentials = new Credentials("Username", "Password")
                })
            };

            TwoCaptcha twoCaptcha = new TwoCaptcha(apiKey: "AccountApiKey", capSharp);

            twoCaptcha.SetCaptchaSettings(
                TwoCaptchaTypes.reCaptchaV2, siteKey: "SITE_KEY", siteUrl: "SITE_URL", invisibleCaptcha: false);

            bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out var accessToken);

            Console.WriteLine(CaptchaIsSuccess);
            Console.WriteLine(accessToken);

            Console.ReadLine();

        }
    }
}