# CapSharp
A .NET Standard library to interact with multiple captcha solution providers. 

### Example:
```cs

CapSharp capSharp = new CapSharp(useProxy: true)
{
    ThrowExceptions = true, // the function will throw an exception instead of returning false.
    Proxy = new Proxy("1.1.1.1", 1234, new ProxySettings(ProxyProtocol.HTTP)
    {
        BackConnect = true,
        Timeout = TimeSpan.FromSeconds(5),
        ProxyCredentials = new Credentials("Username", "Password")
    })
};

TwoCaptcha twoCaptcha = new TwoCaptcha(apiKey: "AccountApiKey", capSharp);

twoCaptcha.SetCaptchaSettings(
    TwoCaptchaTypes.reCaptchaV2, siteKey: "SITE_KEY", "SITE_URL", captchaInvisible: false);

bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out var accessToken);   
```
