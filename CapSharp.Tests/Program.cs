using CapSharp.Enums;
using CapSharp.Models;
using CapSharp.Services.TwoCaptchaService;

using System;

namespace CapSharp.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {

            CapSharp capSharp = new CapSharp(useProxy: false)
            {

                // If you have this option enabled, the 'TrySolveCaptcha'
                // method will never return as false and will throw an exception with the error code
                ThrowExceptions = true,

                // Configure this method if you are activating the 'useProxy' in the method call
                Proxy = new Proxy("1.1.1.1", 1234, new ProxySettings(ProxyProtocol.HTTP)
                {
                    BackConnect = true,
                    Timeout = TimeSpan.FromSeconds(5),
                    ProxyCredentials = new Credentials("Username", "Password")
                })
            };

            // Multiple captcha solvers that are integrated into the library will be initialized as TwoCaptcha does
            TwoCaptcha twoCaptcha = new TwoCaptcha(apiKey: "YOUR_API_KEY", capSharp);


            /* Additional */

            //twoCaptcha.PingBack = ""; (Optional): https://2captcha.com/2captcha-api#pingback
            //twoCaptcha.UserAgent = ""; (Optional): You can set the useragent for the 2Captcha request

            /* Additional */

            /* -------------- Get User Balance -------------- */

            //bool Success = twoCaptcha.TryGetUserBalance(out string MyBalance);

            /* -------------- Get User Balance -------------- */


            /* -------------- Google reCaptchaV2 -------------- */

            //twoCaptcha.SetCaptchaSettings(
            //    TwoCaptchaTypes.reCaptchaV2, PageUrl: "https://www.google.com/recaptcha/api2/demo", ReCaptchaSiteKey: "6Le-wvkSAAAAAPBMRTvw0Q4Muexq9bi0DJwx_mJ-", invisibleCaptcha: false);

            //bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out string accessToken);

            /* -------------- Google reCaptchaV2 -------------- */



            /* -------------- Google reCaptchaV3 -------------- */

            //twoCaptcha.SetCaptchaSettings(
            //    TwoCaptchaTypes.reCaptchaV3, PageUrl: "PAGE_URL", ReCaptchaSiteKey: "", invisibleCaptcha: false);

            //bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out var accessToken);

            /* -------------- Google reCaptchaV3 -------------- */



            /* -------------- Google reCaptcha Invisible -------------- */

            // Simply set 'invisibleCaptcha' to true in the 'SetCaptchaSettings' method

            // twoCaptcha.MinScore = 0.3; (Optional)            

            /* -------------- Google reCaptcha Invisible -------------- */



            /* -------------- Google reCaptcha Enterprise -------------- */

            // twoCaptcha.Enterprise = true;

            /* -------------- Google reCaptcha Enterprise -------------- */



            /* -------------- ArkoseLabs: FunCaptcha -------------- */

            //twoCaptcha.SetCaptchaSettings(
            //    TwoCaptchaTypes.FunCaptcha, PageUrl: "https://api.funcaptcha.com/fc/api/nojs/?pkey=69A21A01-CC7B-B9C6-0F9A-E7FA06677FFC");

            //twoCaptcha.PublicKey = "69A21A01-CC7B-B9C6-0F9A-E7FA06677FFC";
            //twoCaptcha.ServiceUrl = "https%3A%2F%2Fapi.funcaptcha.com";

            //bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out var accessToken);

            /* -------------- ArkoseLabs: FunCaptcha -------------- */



            /* -------------- KeyCaptcha -------------- */

            //twoCaptcha.SetCaptchaSettings(
            //    TwoCaptchaTypes.KeyCaptcha, PageUrl: "https://www.keycaptcha.ru/demo-magnetic/");

            //twoCaptcha.KeyCaptcha.s_s_c_user_id = "10";
            //twoCaptcha.KeyCaptcha.s_s_c_session_id = "493e52c37c10c2bcdf4a00cbc9ccd1e8";
            //twoCaptcha.KeyCaptcha.s_s_c_web_server_sign = "9006dc725760858e4c0715b835472f22-pz-";
            //twoCaptcha.KeyCaptcha.s_s_c_web_server_sign2 = "2ca3abe86d90c6142d5571db98af6714";

            //bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out var accessToken);

            /* -------------- KeyCaptcha -------------- */



            /* -------------- GeeTest -------------- */

            //twoCaptcha.SetCaptchaSettings(
            //    TwoCaptchaTypes.GeeTest, PageUrl: "https://2captcha.com/demo/geetest");

            //twoCaptcha.GeeTest.gt = "81388ea1fc187e0c335c0a8907ff2625";
            //twoCaptcha.GeeTest.challenge = "e02b3442c6bafa48bab2306fe6e9c455";
            //twoCaptcha.GeeTest.api_server = "api.geetest.com";

            //bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out GeeTest.Response accessToken);

            /* -------------- GeeTest -------------- */



            /* -------------- hCaptcha -------------- */

            //twoCaptcha.SetCaptchaSettings(
            //    TwoCaptchaTypes.hCaptcha, PageUrl: "http://democaptcha.com/demo-form-eng/hcaptcha.html");

            //twoCaptcha.hCaptchaSiteKey = "51829642-2cda-4b09-896c-594f89d700cc";

            //bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out string accessToken);

            /* -------------- hCaptcha -------------- */


            //Console.WriteLine(CaptchaIsSuccess);

            //Console.WriteLine(accessToken);

            Console.ReadKey();
        }
    }
}