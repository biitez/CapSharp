using CapSharp.Enums;
using CapSharp.Models;
using CapSharp.Services;
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

            TwoCaptcha twoCaptcha = new TwoCaptcha(ApiKey: "ApiKey", capSharp);

            bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out var accessToken);
            bool BalanceIsSuccess = twoCaptcha.TryGetUserBalance(out var Balance);
        }
    }
}