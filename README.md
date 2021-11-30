# CapSharp (BETA)
A .NET Standard library to interact with multiple captcha solution providers. 

### Under development!
- For now it only contains 2Captcha provider and a code-base of how the multiple Captcha solver providers will work, you want to improve it or add captcha systems?, create a [pull request](https://github.com/biitez/CapSharp/pulls) !

I will dedicate myself to update it if it gets community support

### How to use? [Example class](https://github.com/biitez/CapSharp/blob/master/CapSharp.Tests/Program.cs)

#### Add the library to your project reference and initialize it using:

```cs

CapSharp capSharpLibrary = new CapSharp(useProxy: false) // Here you can assign the use of proxies
{
    // If you have this option enabled, the 'TrySolveCaptcha'
    // method will never return as false and will throw an exception with the error code
    ThrowExceptions = true,

    // Configure this method if you are activating the 'useProxy' in the method call
    Proxy = new Proxy(Host: "1.1.1.1", Port: 1234, new ProxySettings(ProxyProtocol.HTTP)
    {
        BackConnect = false, // If your proxies are backconnect
        Timeout = TimeSpan.FromSeconds(5),
        ProxyCredentials = new Credentials("Username", "Password") // If your proxies use credentials, assign them
    })
};

```

### Current captcha solvers

- [2Captcha.com](#2captcha)


### Menus:

- [Creates the captcha instance](#create-a-instance)
- [Configure the captcha instance](#configure-the-instance)
- [Solve the captcha](#solve-the-captcha)
- [Get user balance](#get-user-balance)

# 2Captcha

### Create a instance:
```cs
// CapSharp capSharpLibrary = new CapSharp(us... ()

TwoCaptcha twoCaptcha = new TwoCaptcha(apiKey: "YOUR_API_KEY", capSharp: capSharpLibrary);
```

### Configure the instance:

#### Two Captcha Types:

- [ReCaptcha V2, reCaptcha V3, reCaptcha Invisible and reCaptcha Enterprise](#google-captcha)
- [hCaptcha](#hcaptcha)
- [Arkose Labs - FunCaptcha](#funcaptcha)
- [KeyCaptcha](#keycaptcha)
- [GeeTest](#geetest)

#### Google Captcha
```cs
/* Google reCaptcha */

twoCaptcha.SetCaptchaSettings(
        TwoCaptchaTypes.reCaptchaV2, PageUrl: "PAGE_URL", ReCaptchaSiteKey: "RECAPTCHA_SITE");
        
// Is the captcha invisible?, add the parameter to the method:

twoCaptcha.SetCaptchaSettings(
        TwoCaptchaTypes.reCaptchaV2, PageUrl: "PAGE_URL", ReCaptchaSiteKey: "RECAPTCHA_SITE", invisibleCaptcha: true);
        
// twoCaptcha.MinScore = 0.3; (Optional): For captcha invisibles
// twoCaptcha.Enterprise = true; (Default: false) // if the captcha is enterprise just enable this
        
/* Google reCaptcha */
```

#### FunCaptcha
```cs
twoCaptcha.SetCaptchaSettings(
    TwoCaptchaTypes.FunCaptcha, PageUrl: "PAGE_URL");

twoCaptcha.PublicKey = "PUBLIC_KEY";
twoCaptcha.ServiceUrl = "SERVICE_URL";
```

#### KeyCaptcha
```cs
twoCaptcha.SetCaptchaSettings(
    TwoCaptchaTypes.KeyCaptcha, PageUrl: "PAGE_URL");

twoCaptcha.KeyCaptcha.s_s_c_user_id = "USER_ID";
twoCaptcha.KeyCaptcha.s_s_c_session_id = "SESSION_ID";
twoCaptcha.KeyCaptcha.s_s_c_web_server_sign = "WEB_SERVER_SIGN";
twoCaptcha.KeyCaptcha.s_s_c_web_server_sign2 = "WEB_SERVER_SIGN2";
```

#### GeeTest
```cs
twoCaptcha.SetCaptchaSettings(
    TwoCaptchaTypes.GeeTest, PageUrl: "PAGE_URL");

twoCaptcha.GeeTest.gt = "GT";
twoCaptcha.GeeTest.challenge = "CHALLENGE";
twoCaptcha.GeeTest.api_server = "API_SERVER"; // (Optional)
```

#### hCaptcha
```cs
twoCaptcha.SetCaptchaSettings(
    TwoCaptchaTypes.hCaptcha, PageUrl: "PAGE_URL");

twoCaptcha.hCaptchaSiteKey = "SITE_KEY";
```

### Solve the captcha:

```cs
bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out string accessToken);
```

#### In the case of [GeeTest](#geetest), you must call:

```cs
bool CaptchaIsSuccess = twoCaptcha.TrySolveCaptcha(out GeeTest.Response GeeTestResponse);

// Response:
// GeeTestResponse.challenge (string)
// GeeTestResponse.validate (string)
// GeeTestResponse.seccode (string)
```

# Get user balance
```cs
bool Success = CaptchaInstance.TryGetUserBalance(out string MyBalance);
```

# General solve captcha
```cs
bool Success = CaptchaInstance.TrySolveCaptcha(out string CaptchaAccessToken);
```

### Contributions, reports or suggestions
If you find a problem or have a suggestion inside this library, please let me know by [clicking here](https://github.com/biitez/CapSharp/issues), if you want to improve the code, make it cleaner or even more expensive, create a [pull request](https://github.com/biitez/CapSharp/pulls). 

In case you will contribute in the code, please follow the same code base.

### Credits

- `Telegram: https://t.me/biitez`
- `Bitcoin Addy: bc1qzz4rghmt6zg0wl6shzaekd59af5znqhr3nxmms`

