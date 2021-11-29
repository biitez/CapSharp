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

### Contributions, reports or suggestions
If you find a problem or have a suggestion inside this library, please let me know by [clicking here](https://github.com/biitez/CapSharp/issues), if you want to improve the code, make it cleaner or even more expensive, create a [pull request](https://github.com/biitez/CapSharp/pulls). 

In case you will contribute in the code, please follow the same code base.

### Credits

- `Telegram: https://t.me/biitez`
- `Bitcoin Addy: bc1qzz4rghmt6zg0wl6shzaekd59af5znqhr3nxmms`
